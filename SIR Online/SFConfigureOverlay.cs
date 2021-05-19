using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeckLinkAPI;
using System.Diagnostics;

namespace SIR_Online
{
    public partial class SFConfigureOverlay : Form
    {
        public OverlayConfigurationHelper ovlycfg;

        ContextMenuStrip ctxMenuStrip;

        int[,] Tiles;
        public Action<Control> cbSelectedControlChanged;

        Control SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                Control old = _selectedControl;
                Control newLabel = value;
                if (newLabel != old)
                {
                    _selectedControl = newLabel;
                    cbSelectedControlChanged(newLabel);
                }
            }
        }

        ScreenPanel NTSCPanel;
        ScreenPanel PALPanel;
        ScreenPanel HDPanel;

        ScreenPanel NTSCPanelIntro;
        ScreenPanel PALPanelIntro;
        ScreenPanel HDPanelIntro;



        ScreenPanel CurrenPanel;
        private Point MouseDownlocation;
        Control _selectedControl;

        FontDialog fd;


        public SFConfigureOverlay()
        {
            InitializeComponent();
            ovlycfg = new OverlayConfigurationHelper();

            ctxMenuStrip = new ContextMenuStrip();
        }

        private void SFConfigureOverlay_Load(object sender, EventArgs e)
        {
            InitializeCtxMenuStrip();
            InitializeSurveyKeyCB();
            InitializeDisplayDurationCB();

            //
            NTSCPanel = new ScreenPanel(900, 608, ScreenRatio.NTSC, 20);
            //PALPanel = new ScreenPanel(720, 576, ScreenRatio.PAL, 20);
            PALPanel = new ScreenPanel(900, 720, ScreenRatio.PAL, 20);
            //HDPanel = new ScreenPanel(912, 513, ScreenRatio.HD, 20);
            HDPanel = new ScreenPanel(1121, 633, ScreenRatio.HD, 20);
            NTSCPanel.Hide();
            PALPanel.Hide();
            HDPanel.Hide();
            NTSCPanel.Parent = OverlayTabGB;
            PALPanel.Parent = OverlayTabGB;
            HDPanel.Parent = OverlayTabGB;
            NTSCPanel.Location = NTSCPanel.GetLocationReferringParent();
            PALPanel.Location = PALPanel.GetLocationReferringParent();
            HDPanel.Location = HDPanel.GetLocationReferringParent();

            //NTSCPanelIntro = new ScreenPanel(720, 486, ScreenRatio.NTSC, 20);
            //PALPanelIntro = new ScreenPanel(720, 576, ScreenRatio.PAL, 20);
            //HDPanelIntro = new ScreenPanel(912, 513, ScreenRatio.HD, 20);
            NTSCPanelIntro = new ScreenPanel(900, 608, ScreenRatio.NTSC, 20);
            PALPanelIntro = new ScreenPanel(900, 720, ScreenRatio.PAL, 20);
            HDPanelIntro = new ScreenPanel(1121, 633, ScreenRatio.HD, 20);
            NTSCPanelIntro.Hide();
            PALPanelIntro.Hide();
            HDPanelIntro.Hide();
            NTSCPanelIntro.Parent = IntroPageTabControlGB;
            PALPanelIntro.Parent = IntroPageTabControlGB;
            HDPanelIntro.Parent = IntroPageTabControlGB;
            NTSCPanelIntro.Location = NTSCPanel.GetLocationReferringParent();
            PALPanelIntro.Location = PALPanel.GetLocationReferringParent();
            HDPanelIntro.Location = HDPanel.GetLocationReferringParent();

            InitializeScreenRatioCB();
            InitializeNumericUpDowns();
            TextLabelPropertyPanel.Hide();
            ImagePropertyPanel.Hide();
            fd = new FontDialog();
            colorDialog = new ColorDialog();
            cbSelectedControlChanged += OnSelectedControlChanged;
            SurveyData.RegisterNavConfigChanged(OnSurveyDataNavconfigModelChange);

        }

        private void SFConfigureOverlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        public void InitializeScreenRatioCB()
        {
            ScreenRatioCB.Items.Add(new StringObjectPair<ScreenRatio>("NTSC", ScreenRatio.NTSC));
            ScreenRatioCB.Items.Add(new StringObjectPair<ScreenRatio>("PAL", ScreenRatio.PAL));
            ScreenRatioCB.Items.Add(new StringObjectPair<ScreenRatio>("HD", ScreenRatio.HD));
            ScreenRatioCB.SelectedIndex = 0;
        }
        void InitializeCtxMenuStrip()
        {
            ctxMenuStrip.Items.Add("Del").Name = "Delete";
            ctxMenuStrip.Items.Add("Edit").Name = "Edit";
        }
        public void SetPanelSizeAccordingtoScreenRatio()
        {
            ScreenRatio key = ((StringObjectPair<ScreenRatio>)ScreenRatioCB.SelectedItem).value;

            switch (key)
            {
                case ScreenRatio.NTSC:
                    NTSCPanel.Show();
                    NTSCPanelIntro.Show();
                    PALPanel.Hide();
                    PALPanelIntro.Hide();
                    HDPanel.Hide();
                    HDPanelIntro.Hide();
                    if (tabControl1.SelectedTab.Name == "OverlayTabPage")
                    {
                        CurrenPanel = NTSCPanel;
                    }
                    else if (tabControl1.SelectedTab.Name == "IntroTabPage")
                    {
                        CurrenPanel = NTSCPanelIntro;
                    }
                    Console.WriteLine(" tab control name :" + tabControl1.SelectedTab.Name);
                    Console.WriteLine(" current panel :" + CurrenPanel.Name);
                    break;

                case ScreenRatio.PAL:
                    PALPanel.Show();
                    PALPanelIntro.Show();
                    NTSCPanel.Hide();
                    NTSCPanelIntro.Hide();
                    HDPanel.Hide();
                    HDPanelIntro.Hide();

                    if (tabControl1.SelectedTab.Name == "OverlayTabPage")
                    {
                        CurrenPanel = PALPanel;
                    }
                    else if (tabControl1.SelectedTab.Name == "IntroTabPage")
                    {
                        CurrenPanel = PALPanelIntro;
                    }


                    break;

                case ScreenRatio.HD:
                    HDPanel.Show();
                    HDPanelIntro.Show();

                    NTSCPanel.Hide();
                    NTSCPanelIntro.Hide();
                    PALPanel.Hide();
                    PALPanelIntro.Hide();

                    if (tabControl1.SelectedTab.Name == "OverlayTabPage")
                    {
                        CurrenPanel = HDPanel;
                    }
                    else if (tabControl1.SelectedTab.Name == "IntroTabPage")
                    {
                        CurrenPanel = HDPanelIntro;
                    }

                    break;

                default:

                    break;
            }
        }

        ColorDialog colorDialog;

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = fd.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Font fcx = fd.Font;
                SelectedControl.Font = fcx;

            }
        }

        public void InitializeSurveyKeyCB()
        {
            SurveyKeyCB.Items.Clear();
            try
            {
                if (SurveyData.NavConfigModel.CommaFieldIndexes != null)
                {
                    foreach (var item in SurveyData.NavConfigModel.CommaFieldIndexes)
                    {
                        if (item.Value != null)
                        {
                            SurveyKeyCB.Items.Add(item.Key);
                        }
                    }
                    SurveyKeyCB.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.Source);
            }
        }


        public void OnSelectedControlChanged(Control _lbl)
        {
            Type controlType = _lbl.GetType();


            if (controlType == typeof(TextLabel))
            {
                TextLabelPropertyPanel.Show();
                ImagePropertyPanel.Hide();
                if (!((TextLabel)_lbl).IsSurveyData)
                {
                    TextTB.Show();
                    SurveyKeyCB.Hide();
                    Labeler.Text = "Text";
                }
                else
                {
                    TextTB.Hide();
                    SurveyKeyCB.Show();
                    Labeler.Text = " Survey Key";
                }

            }
            else if (controlType == typeof(ImageControl))
            {
                ImagePropertyPanel.Show();
                TextLabelPropertyPanel.Hide();
                ImageControl imageControl = (ImageControl)_lbl;
                HeightNumericUpD.Value = imageControl.Size.Height;
                WidthNumericUPD.Value = imageControl.Size.Width;

            }

            //if (!_lbl.IsSurveyData)
            //{
            //    TextTB.Show();
            //    SurveyKeyCB.Hide();
            //    Labeler.Text = "Text";
            //}
            //else
            //{
            //    TextTB.Hide();
            //    SurveyKeyCB.Show();
            //    Labeler.Text = " Survey Key";
            //}
            InitializeNumericUpDowns();
        }
        void OnSurveyDataNavconfigModelChange()
        {
            InitializeSurveyKeyCB();
        }

        private void AddTextNotSurveyBtn_Click(object sender, EventArgs e)
        {
            var CurrentControl = new TextLabel(false);
            CurrentControl.Parent = CurrenPanel;
            CurrentControl.ForeColor = Color.White;
            CurrentControl.Text = " Text";
            //CurrentControl.Paint += TextLabel_OnPaint;
            //  new PaintEventHandler((objectpaint, paint) =>
            //{

            //    paint.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //    paint.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    paint.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            //    FontFamily fontFamily = new FontFamily("Arial Black");

            //    StringFormat strformat = new StringFormat();
            //    strformat.Alignment=StringAlignment.Near;
            //    string szbuf = CurrentControl.Text;
            //    SolidBrush semiTransBrush = new SolidBrush(Color.WhiteSmoke);

            //    //graphics.DrawString(SurveyData.Time, font, new SolidBrush(Color.Red), f);
            //    System.Drawing.Font font = new System.Drawing.Font(FontFamily.GenericSerif, CurrentControl.Font.Size);
            //    Point point = new Point(0, 0);
            //    paint.Graphics.DrawString(szbuf, font, semiTransBrush, point);


            //});
            Font f = new Font(FontFamily.GenericMonospace, 5);

            CurrentControl.BorderStyle = BorderStyle.FixedSingle;
            CurrentControl.AutoSize = true;

            CurrentControl.Size = TextRenderer.MeasureText("Text Render", new System.Drawing.Font(FontFamily.GenericSerif, CurrentControl.Font.Size));

            CurrentControl.Anchor = AnchorStyles.None;
            CurrentControl.Dock = DockStyle.None;
            CurrentControl.BackColor = Color.Transparent;
            CurrentControl.Margin = new Padding(3, 0, 3, 0);
            CurrentControl.Padding = new Padding(0, 0, 0, 0);
            //CurrentControl.TextAlign = ContentAlignment.MiddleCenter;

            Point p = new Point();
            p.X = 40;
            p.Y = 50;
            CurrentControl.Location = p;
            CurrentControl.MouseDown += new MouseEventHandler((o, x) =>
            {
                SelectedControl = CurrentControl;
                TextTB.Text = SelectedControl.Text;


                if (x.Button == MouseButtons.Left)
                {
                    MouseDownlocation = x.Location;
                }
                else if (x.Button == MouseButtons.Right)
                {

                    ctxMenuStrip.Show(CurrentControl, x.Location);
                    ctxMenuStrip.ItemClicked += CtxMenuStrip_ItemClicked; ;

                }
            });
            CurrentControl.MouseMove += new MouseEventHandler((m, xo) =>
            {

                if (xo.Button == MouseButtons.Left)
                {
                    // CurrentControl.Left = Math.Min(Math.Max(0, xo.X + CurrentControl.Left - MouseDownlocation.X), CurrenPanel.Width - CurrentControl.Width);
                    // CurrentControl.Top = Math.Min(Math.Max(0, xo.Y + CurrentControl.Top - MouseDownlocation.Y), CurrenPanel.Height - CurrentControl.Height);

                    if (xo.X > 0 && xo.Y > 0 && xo.X < CurrenPanel.Width - CurrenPanel.tileSize && xo.Y < CurrenPanel.Height - CurrenPanel.tileSize)
                    {
                        //CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).X + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).X), CurrenPanel.Width - CurrentControl.Width);
                        //CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).Y + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).Y), CurrenPanel.Height - CurrentControl.Height);
                        CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X + CurrentControl.Left - MouseDownlocation.X, xo.Y).X), CurrenPanel.Width - CurrentControl.Width);
                        CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y + CurrentControl.Top - MouseDownlocation.Y).Y), CurrenPanel.Height - CurrentControl.Height);
                    }
                }

            });
            CurrentControl.MouseUp += new MouseEventHandler((bd, bb) =>
            {
                if (bb.Button == MouseButtons.Left)
                {
                    MouseDownlocation.X = 0;
                    MouseDownlocation.Y = 0;
                }
                //SelectedLabel = null;
            });
            //CurrentControl.DoubleClick += new EventHandler((v, sa) =>
            //{

            //    FontDialog vf = new FontDialog();
            //    DialogResult dialogResult = vf.ShowDialog();
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        Font fcx = vf.Font;
            //        CurrentControl.Font = fcx;
            //        Console.WriteLine(" font size :" + fcx.Size);
            //    }


            //});

            CurrentControl.TextChanged += (nn, md) =>
            {
                Size s = TextRenderer.MeasureText(CurrentControl.Text, CurrentControl.Font);
                CurrentControl.Width = s.Width;
                CurrentControl.Height = s.Height;
            };

        }

        private void CtxMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                //case "Edit":
                //    ctxMenuStrip.Hide();
                //    FontDialog vf = new FontDialog();
                //    DialogResult dialogResult = vf.ShowDialog();

                //    if (dialogResult == DialogResult.OK)
                //    {
                //        Font fcx = vf.Font;
                //        SelectedLabel.Font = fcx;
                //        Console.WriteLine(" font size :" + fcx.Size);
                //        vf.Dispose();
                //        vf = null;

                //    }
                //    break;
                case "Delete":
                    //textLabels.Remove(SelectedLabel);
                    SelectedControl.Dispose();

                    ctxMenuStrip.Hide();
                    break;
            };
        }

        private void AddTextISsurveyBtn_Click(object sender, EventArgs e)
        {
            var CurrentControl = new TextLabel(true, "");
            CurrentControl.Parent = CurrenPanel;
            CurrentControl.ForeColor = Color.White;
            CurrentControl.Text = SurveyKeyCB.SelectedItem.ToString();
            CurrentControl.Paint += new PaintEventHandler((objectpaint, paint) =>
            {

                //paint.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //paint.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //paint.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                //FontFamily fontFamily = new FontFamily("Arial Black");

                //StringFormat strformat = new StringFormat();
                //strformat.Alignment = StringAlignment.Near;
                //string szbuf = CurrentControl.Text;
                //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(125, 0, 0, 255));
                ////graphics.DrawString(SurveyData.Time, font, new SolidBrush(Color.Red), f);
                //System.Drawing.Font font = new System.Drawing.Font(FontFamily.GenericSerif, CurrentControl.Font.Size);
                //Point point = new Point(0, 0);
                //paint.Graphics.DrawString(szbuf, font, semiTransBrush, point);


            });
            Font f = new Font(FontFamily.GenericMonospace, 5);

            CurrentControl.BorderStyle = BorderStyle.FixedSingle;
            CurrentControl.AutoSize = true;

            CurrentControl.Size = TextRenderer.MeasureText("Text Render", new System.Drawing.Font(FontFamily.GenericSerif, CurrentControl.Font.Size));

            CurrentControl.Anchor = AnchorStyles.None;
            CurrentControl.Dock = DockStyle.None;
            CurrentControl.BackColor = Color.Transparent;
            CurrentControl.Margin = new Padding(3, 0, 3, 0);
            CurrentControl.Padding = new Padding(0, 0, 0, 0);
            //CurrentControl.TextAlign = ContentAlignment.MiddleCenter;

            Point p = new Point();
            p.X = 40;
            p.Y = 50;
            CurrentControl.Location = p;
            CurrentControl.MouseDown += new MouseEventHandler((o, x) =>
            {
                SelectedControl = CurrentControl;
                TextTB.Text = SelectedControl.Text;

                TextLabelPropertyPanel.Show();
                ImagePropertyPanel.Hide();
                //if (!SelectedControl.IsSurveyData)
                if (!((TextLabel)SelectedControl).IsSurveyData)
                {
                    TextTB.Show();
                    SurveyKeyCB.Hide();
                    Labeler.Text = "Text";
                }
                else
                {
                    TextTB.Hide();
                    SurveyKeyCB.Show();
                    Labeler.Text = " Survey Key";
                }

                if (x.Button == MouseButtons.Left)
                {
                    MouseDownlocation = x.Location;
                }
                else if (x.Button == MouseButtons.Right)
                {

                    ctxMenuStrip.Show(CurrentControl, x.Location);
                    ctxMenuStrip.ItemClicked += CtxMenuStrip_ItemClicked;

                }
            });
            CurrentControl.MouseMove += new MouseEventHandler((m, xo) =>
            {

                if (xo.Button == MouseButtons.Left)
                {
                    if (xo.X > 0 && xo.Y > 0 && xo.X < CurrenPanel.Width - CurrenPanel.tileSize && xo.Y < CurrenPanel.Height - CurrenPanel.tileSize)
                    {
                        //CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).X + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).X), CurrenPanel.Width - CurrentControl.Width);
                        //CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).Y + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).Y), CurrenPanel.Height - CurrentControl.Height);
                        CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X + CurrentControl.Left - MouseDownlocation.X, xo.Y).X), CurrenPanel.Width - CurrentControl.Width);
                        CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y + CurrentControl.Top - MouseDownlocation.Y).Y), CurrenPanel.Height - CurrentControl.Height);
                    }
                    //CurrentControl.Left = Math.Min(Math.Max(0, xo.X + CurrentControl.Left - MouseDownlocation.X), CurrenPanel.Width - CurrentControl.Width);
                    //CurrentControl.Top = Math.Min(Math.Max(0, xo.Y + CurrentControl.Top - MouseDownlocation.Y), CurrenPanel.Height - CurrentControl.Height);
                    //CurrentControl.Left = Math.Min(Math.Max(0, (xo.X + CurrentControl.Left - MouseDownlocation.X)/CurrenPanel.tileSize), CurrenPanel.Width - CurrentControl.Width);
                    // CurrentControl.Top = Math.Min(Math.Max(0,( xo.Y + CurrentControl.Top - MouseDownlocation.Y)/CurrenPanel.tileSize), CurrenPanel.Height - CurrentControl.Height);
                }
            });
            CurrentControl.MouseUp += new MouseEventHandler((bd, bb) =>
            {
                if (bb.Button == MouseButtons.Left)
                {
                    MouseDownlocation.X = 0;
                    MouseDownlocation.Y = 0;
                }
                //SelectedLabel = null;
            });
            //CurrentControl.DoubleClick += new EventHandler((v, sa) =>
            //{

            //    FontDialog vf = new FontDialog();
            //    DialogResult dialogResult = vf.ShowDialog();
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        Font fcx = vf.Font;
            //        CurrentControl.Font = fcx;
            //        Console.WriteLine(" font size :" + fcx.Size);
            //    }


            //});

            CurrentControl.TextChanged += (nn, md) =>
            {
                Size s = TextRenderer.MeasureText(CurrentControl.Text, CurrentControl.Font);
                CurrentControl.Width = s.Width;
                CurrentControl.Height = s.Height;
            };
            CurrentControl.ForeColorChanged += CurrentControl_ForeColorChanged; ;

            //textLabels.Add(CurrentControl);
        }

        private void CurrentControl_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void SurveyKeyCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedControl.GetType() == typeof(TextLabel))
            {
                TextLabel textLabel = (TextLabel)SelectedControl;

                if (textLabel.IsSurveyData)
                {
                    textLabel.key = (string)SurveyKeyCB.SelectedItem;
                    textLabel.Text = (string)SurveyKeyCB.SelectedItem;
                }
            }
        }


        private void InitializeDisplayDurationCB()
        {
            DisplayDurationCB.Update();
            DisplayDurationCB.Items.Add(new StringObjectPair<int>("10 Second", 10));
            DisplayDurationCB.Items.Add(new StringObjectPair<int>("20 Second", 20));
            DisplayDurationCB.Items.Add(new StringObjectPair<int>("30 Second", 30));
            DisplayDurationCB.Items.Add(new StringObjectPair<int>("1 minute", 60));
            //DisplayDurationCB.EndUpdate();
            DisplayDurationCB.SelectedIndex = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrenPanel = null;
            SetPanelSizeAccordingtoScreenRatio();
        }

        private void TextTB_TextChanged(object sender, EventArgs e)
        {
            SelectedControl.Text = TextTB.Text;
        }

        private void ApplyConfigBtn_Click(object sender, EventArgs e)
        {
            //TextOverlay[] textOverlays = new TextOverlay[textLabels.Count];
            //Overlay.textOverlays = null;
            //for (int i = 0; i < textOverlays.Length; i++)
            //{
            //    TextOverlay txt;
            //    if (textLabels[i].IsSurveyData == false)
            //    {
            //         txt = new TextOverlay(textLabels[i].Text, textLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font, textLabels[i].ForeColor);                   
            //    }
            //    else
            //    {
            //         txt = new TextOverlay(textLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor);                 
            //    }
            //    textOverlays[i] = txt;
            //}
            //Overlay.textOverlays = textOverlays;

            TextOverlay[] textOverlayNTSC = new TextOverlay[NTSCPanel.GetTextLabels.Length];
            for (int i = 0; i < textOverlayNTSC.Length; i++)
            {
                TextOverlay txt;
                if (NTSCPanel.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Text, NTSCPanel.GetTextLabels[i].Font.Size, NTSCPanel.GetTextLabels[i].Location, NTSCPanel.Width, NTSCPanel.GetTextLabels[i].Font, NTSCPanel.GetTextLabels[i].ForeColor, NTSCPanel.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, NTSCPanel.GetTextLabels[i].Location, NTSCPanel.Width, NTSCPanel.GetTextLabels[i].Font, NTSCPanel.GetTextLabels[i].key, NTSCPanel.GetTextLabels[i].ForeColor, NTSCPanel.ratio);
                }
                textOverlayNTSC[i] = txt;
            }

            Overlay.NTSCScreen = new OverlayScreen(NTSCPanel.ratio, textOverlayNTSC);

            TextOverlay[] textOverlayPAL = new TextOverlay[PALPanel.GetTextLabels.Length];
            for (int i = 0; i < textOverlayPAL.Length; i++)
            {
                TextOverlay txt;
                if (PALPanel.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(PALPanel.GetTextLabels[i].Text, PALPanel.GetTextLabels[i].Font.Size, PALPanel.GetTextLabels[i].Location, PALPanel.Width, PALPanel.GetTextLabels[i].Font, PALPanel.GetTextLabels[i].ForeColor, PALPanel.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(PALPanel.GetTextLabels[i].Font.Size, PALPanel.GetTextLabels[i].Location, PALPanel.Width, PALPanel.GetTextLabels[i].Font, PALPanel.GetTextLabels[i].key, PALPanel.GetTextLabels[i].ForeColor, PALPanel.ratio);
                }
                textOverlayPAL[i] = txt;
            }
            Overlay.PALScreen = new OverlayScreen(PALPanel.ratio, textOverlayPAL);

            TextOverlay[] textoverlayHD = new TextOverlay[HDPanel.GetTextLabels.Length];
            for (int i = 0; i < textoverlayHD.Length; i++)
            {
                TextOverlay txt;
                if (HDPanel.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(HDPanel.GetTextLabels[i].Text, HDPanel.GetTextLabels[i].Font.Size, HDPanel.GetTextLabels[i].Location, HDPanel.Width, HDPanel.GetTextLabels[i].Font, HDPanel.GetTextLabels[i].ForeColor, HDPanel.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(HDPanel.GetTextLabels[i].Font.Size, HDPanel.GetTextLabels[i].Location, HDPanel.Width, HDPanel.GetTextLabels[i].Font, HDPanel.GetTextLabels[i].key, HDPanel.GetTextLabels[i].ForeColor, HDPanel.ratio);
                }
                textoverlayHD[i] = txt;
            }
            Overlay.HDScreen = new OverlayScreen(HDPanel.ratio, textoverlayHD);

            //  Intro Page
            //ScreenRatio key = ((StringObjectPair<ScreenRatio>)ScreenRatioCB.SelectedItem).value;
            int secDisplayDur = ((StringObjectPair<int>)DisplayDurationCB.SelectedItem).value;

            TextOverlay[] textOverlayNTSCIntro = new TextOverlay[NTSCPanelIntro.GetTextLabels.Length];
            for (int i = 0; i < textOverlayNTSCIntro.Length; i++)
            {
                TextOverlay txt;
                if (NTSCPanelIntro.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(NTSCPanelIntro.GetTextLabels[i].Text, NTSCPanelIntro.GetTextLabels[i].Font.Size, NTSCPanelIntro.GetTextLabels[i].Location, NTSCPanelIntro.Width, NTSCPanelIntro.GetTextLabels[i].Font, NTSCPanelIntro.GetTextLabels[i].ForeColor, NTSCPanelIntro.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(NTSCPanelIntro.GetTextLabels[i].Font.Size, NTSCPanelIntro.GetTextLabels[i].Location, NTSCPanelIntro.Width, NTSCPanelIntro.GetTextLabels[i].Font, NTSCPanelIntro.GetTextLabels[i].key, NTSCPanelIntro.GetTextLabels[i].ForeColor, NTSCPanelIntro.ratio);
                }
                textOverlayNTSCIntro[i] = txt;
            }

            Overlay.NTSCIntroPage = new OverlayScreen(NTSCPanelIntro.ratio, textOverlayNTSCIntro, true, secDisplayDur);

            TextOverlay[] textOverlayPALIntro = new TextOverlay[PALPanelIntro.GetTextLabels.Length];
            for (int i = 0; i < textOverlayPALIntro.Length; i++)
            {
                TextOverlay txt;
                if (PALPanelIntro.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(PALPanelIntro.GetTextLabels[i].Text, PALPanelIntro.GetTextLabels[i].Font.Size, PALPanelIntro.GetTextLabels[i].Location, PALPanelIntro.Width, PALPanelIntro.GetTextLabels[i].Font, PALPanelIntro.GetTextLabels[i].ForeColor, PALPanelIntro.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(PALPanelIntro.GetTextLabels[i].Font.Size, PALPanelIntro.GetTextLabels[i].Location, PALPanelIntro.Width, PALPanelIntro.GetTextLabels[i].Font, PALPanelIntro.GetTextLabels[i].key, PALPanelIntro.GetTextLabels[i].ForeColor, PALPanelIntro.ratio);
                }
                textOverlayPALIntro[i] = txt;
            }
            Overlay.PALIntro = new OverlayScreen(PALPanelIntro.ratio, textOverlayPALIntro, true, secDisplayDur);

            TextOverlay[] textoverlayHDIntro = new TextOverlay[HDPanelIntro.GetTextLabels.Length];
            for (int i = 0; i < textoverlayHDIntro.Length; i++)
            {
                TextOverlay txt;
                if (HDPanelIntro.GetTextLabels[i].IsSurveyData == false)
                {
                    txt = new TextOverlay(HDPanelIntro.GetTextLabels[i].Text, HDPanelIntro.GetTextLabels[i].Font.Size, HDPanelIntro.GetTextLabels[i].Location, HDPanelIntro.Width, HDPanelIntro.GetTextLabels[i].Font, HDPanelIntro.GetTextLabels[i].ForeColor, HDPanelIntro.ratio);
                }
                else
                {
                    // txt = new TextOverlay(NTSCPanel.GetTextLabels[i].Font.Size, textLabels[i].Location, panel1.Width, (float)panel1.Width / (float)panel1.Height, textLabels[i].Font,textLabels[i].key, textLabels[i].ForeColor); 
                    txt = new TextOverlay(HDPanelIntro.GetTextLabels[i].Font.Size, HDPanelIntro.GetTextLabels[i].Location, HDPanelIntro.Width, HDPanelIntro.GetTextLabels[i].Font, HDPanelIntro.GetTextLabels[i].key, HDPanelIntro.GetTextLabels[i].ForeColor, HDPanelIntro.ratio);
                }
                textoverlayHDIntro[i] = txt;
            }
            Overlay.HDIntro = new OverlayScreen(HDPanelIntro.ratio, textoverlayHDIntro, true, secDisplayDur);


        }

        private void ScreenRatioCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPanelSizeAccordingtoScreenRatio();
        }

        private void ColorPickerPanel_Click(object sender, EventArgs e)
        {
            DialogResult fd = colorDialog.ShowDialog();
            if (fd == DialogResult.OK)
            {
                SelectedControl.ForeColor = colorDialog.Color;
                ColorPickerPanel.BackColor = colorDialog.Color;

            }

        }

        private void SurveyKeyCB_Click(object sender, EventArgs e)
        {

        }

        private void btn_images_Click(object sender, EventArgs e)
        {
            var CurrentControl = new ImageControl();
            CurrentControl.Parent = CurrenPanel;
            CurrentControl.ForeColor = Color.White;
            CurrentControl.BackColor = Color.Green;
            CurrentControl.BorderStyle = BorderStyle.FixedSingle;
            CurrentControl.AutoSize = true;
            // CurrentControl.Size = TextRenderer.MeasureText("Text Render", new System.Drawing.Font(FontFamily.GenericSerif, CurrentControl.Font.Size));
            CurrentControl.Anchor = AnchorStyles.None;
            CurrentControl.Dock = DockStyle.None;
            //CurrentControl.BackColor = Color.Transparent;
            CurrentControl.Margin = new Padding(3, 0, 3, 0);
            CurrentControl.Padding = new Padding(0, 0, 0, 0);

            Point p = new Point();
            p.X = 40;
            p.Y = 50;
            CurrentControl.Location = p;
            CurrentControl.MouseDown += new MouseEventHandler((o, x) =>
            {
                SelectedControl = CurrentControl;
                //TextTB.Text = SelectedControl.Text;

                TextLabelPropertyPanel.Hide();
                ImagePropertyPanel.Show();
                //if (!SelectedControl.IsSurveyData)


                if (x.Button == MouseButtons.Left)
                {
                    MouseDownlocation = x.Location;
                }
                else if (x.Button == MouseButtons.Right)
                {

                    ctxMenuStrip.Show(CurrentControl, x.Location);
                    ctxMenuStrip.ItemClicked += CtxMenuStrip_ItemClicked;

                }
            });
            CurrentControl.MouseMove += new MouseEventHandler((m, xo) =>
            {

                if (xo.Button == MouseButtons.Left)
                {
                    if (xo.X > 0 && xo.Y > 0 && xo.X < CurrenPanel.Width - CurrenPanel.tileSize && xo.Y < CurrenPanel.Height - CurrenPanel.tileSize)
                    {
                        //CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).X + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).X), CurrenPanel.Width - CurrentControl.Width);
                        //CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y).Y + CurrenPanel.GetPointAt(CurrentControl.Left, CurrentControl.Top).Y), CurrenPanel.Height - CurrentControl.Height);
                        CurrentControl.Left = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X + CurrentControl.Left - MouseDownlocation.X, xo.Y).X), CurrenPanel.Width - CurrentControl.Width);
                        CurrentControl.Top = Math.Min(Math.Max(0, CurrenPanel.GetPointAt(xo.X, xo.Y + CurrentControl.Top - MouseDownlocation.Y).Y), CurrenPanel.Height - CurrentControl.Height);
                    }
                    //CurrentControl.Left = Math.Min(Math.Max(0, xo.X + CurrentControl.Left - MouseDownlocation.X), CurrenPanel.Width - CurrentControl.Width);
                    //CurrentControl.Top = Math.Min(Math.Max(0, xo.Y + CurrentControl.Top - MouseDownlocation.Y), CurrenPanel.Height - CurrentControl.Height);
                    //CurrentControl.Left = Math.Min(Math.Max(0, (xo.X + CurrentControl.Left - MouseDownlocation.X)/CurrenPanel.tileSize), CurrenPanel.Width - CurrentControl.Width);
                    // CurrentControl.Top = Math.Min(Math.Max(0,( xo.Y + CurrentControl.Top - MouseDownlocation.Y)/CurrenPanel.tileSize), CurrenPanel.Height - CurrentControl.Height);
                }
            });
            CurrentControl.MouseUp += new MouseEventHandler((bd, bb) =>
            {
                if (bb.Button == MouseButtons.Left)
                {
                    MouseDownlocation.X = 0;
                    MouseDownlocation.Y = 0;
                }
                //SelectedLabel = null;
            });
            //CurrentControl.DoubleClick += new EventHandler((v, sa) =>
            //{

            //    FontDialog vf = new FontDialog();
            //    DialogResult dialogResult = vf.ShowDialog();
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        Font fcx = vf.Font;
            //        CurrentControl.Font = fcx;
            //        Console.WriteLine(" font size :" + fcx.Size);
            //    }

            //});
        }



        // TODO :The iamge size output to the screen must be handled if the width or height of original image bigger than screenPanel
        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Open Image";
                dialog.Filter = "bmp files (*.BMP,*.JPG,*.png)|*.BMP;*.JPG;*.PNG";

                DialogResult ret = STAShowDialog(dialog);
                if (ret == DialogResult.OK)
                {
                    if (SelectedControl.GetType() == typeof(ImageControl))
                    {
                        var currImageOntrol = (ImageControl)SelectedControl;
                        //currImage.Image=new Bitmap(dialog.FileName,new Size((int)(WidthNumericUPD.Value),(int)HeightNumericUpD.Value));

                        int widthNUD = (int)(WidthNumericUPD.Value);
                        int heightNUD = (int)(HeightNumericUpD.Value);
                        Size sizeNUD = new Size(widthNUD, heightNUD);
                        Image img = new Bitmap(dialog.FileName);
                        currImageOntrol.Image = img;
                        currImageOntrol.ImageLocation = dialog.FileName;
                        currImageOntrol.Size = currImageOntrol.Image.Size;

                        IsBothAlreadySet = true;
                        WidthNumericUPD.Value = currImageOntrol.Size.Width;
                        HeightNumericUpD.Value = currImageOntrol.Size.Height;
                        IsBothAlreadySet = false;


                        //if (MaintanARChkBox.Checked)
                        //{
                        //    int WidthMaintainedAR=widthNUD;
                        //    int HeightMaintainedAr=heightNUD;
                        //    if (img.Width > img.Height)
                        //    {
                        //        WidthMaintainedAR = widthNUD;
                        //        HeightMaintainedAr = (int)(heightNUD / ((float)img.Width / (float)img.Height));

                        //    }else if (img.Height > img.Width)
                        //    {
                        //        HeightMaintainedAr = heightNUD;
                        //        WidthMaintainedAR = (int)(widthNUD / ((float)img.Width / (float)img.Height));
                        //    }
                        //    currImage.Image = new Bitmap(img, new Size(WidthMaintainedAR,HeightMaintainedAr));

                        //}
                        //else
                        //{
                        //    //currImage.Image = new Bitmap(img, sizeNUD);
                        //}


                    }
                }
            }
        }

        public void InitializeNumericUpDowns()
        {
            WidthNumericUPD.Maximum = 900;
            HeightNumericUpD.Maximum = 600;
            WidthNumericUPD.Minimum = 50;
            HeightNumericUpD.Minimum = 50;

            WidthNumericUPD.Value = 100;
            HeightNumericUpD.Value = 100;

        }

        private DialogResult STAShowDialog(FileDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }

        bool IsWidthDetermined;
        bool IsheightDetermined;
        bool IsBothAlreadySet
        {
            get
            {
                if (IsWidthDetermined && IsheightDetermined)
                {
                    return true;
                }
                else
                    return false;
            }
            set
            {
                if (value == false)
                {
                    IsWidthDetermined = false;
                    IsheightDetermined = false;
                }
                else
                {
                    return;
                }
            }
        }

        bool IsDetermined;

        //public enum HoldActionNumeric { None,Width,Height  }
        //HoldActionNumeric _holdNUmeric;
        private void WidthNumericUPD_ValueChanged(object sender, EventArgs e)
        {

            if (SelectedControl != null && SelectedControl.GetType() == typeof(ImageControl))
            {

                ImageControl imageControl = (ImageControl)SelectedControl;
                if (imageControl.Image != null && !string.IsNullOrWhiteSpace(imageControl.ImageLocation))
                {
                    using (Image sourceImage = new Bitmap(imageControl.ImageLocation))
                    {
                        if (MaintanARChkBox.Checked)
                        {

                            if (!IsDetermined)
                            {
                               // int width = (int)WidthNumericUPD.Value;



                            }


                        }
                        else
                        {
                            imageControl.Image = new Bitmap(sourceImage, (int)WidthNumericUPD.Value, (int)HeightNumericUpD.Value);
                            
                        }
                        imageControl.Size = imageControl.Image.Size;
                    }
                }
            }
        }

        private void HeightNumericUpD_ValueChanged(object sender, EventArgs e)
        {          
            if (SelectedControl != null && SelectedControl.GetType() == typeof(ImageControl))
            {

                ImageControl imageControl = (ImageControl)SelectedControl;
                if (imageControl.Image != null && !string.IsNullOrWhiteSpace(imageControl.ImageLocation))
                {
                    using (Image sourceImage = new Bitmap(imageControl.ImageLocation))
                    {
                        if (MaintanARChkBox.Checked)
                        {
                        
                        }
                        else
                        {
                            imageControl.Image = new Bitmap(sourceImage, (int)WidthNumericUPD.Value, (int)HeightNumericUpD.Value);
                            
                        }
                        imageControl.Size = imageControl.Image.Size;
                    }

                }

            }

        }

        void CalCulateWidthHeight(int _initialWidth,int _initialHeight)
        {
            int width;
            int height;

            //if()

        }
        //private void HeightNumericUpD_ValueChanged(object sender, EventArgs e)
        //{
        //    if (SelectedControl != null && SelectedControl.GetType() == typeof(ImageControl))
        //    {

        //        ImageControl imageControl = (ImageControl)SelectedControl;
        //        if (imageControl.Image != null && !string.IsNullOrWhiteSpace(imageControl.ImageLocation))
        //        {
        //            using (Image sourceImage = new Bitmap(imageControl.ImageLocation))
        //            {
        //                if (MaintanARChkBox.Checked)
        //                {
        //                    if (!IsBothAlreadySet)
        //                    {
        //                        if (!IsheightDetermined)
        //                        {
        //                            if (!IsWidthDetermined)
        //                            {

        //                                int widthOld = (int)WidthNumericUPD.Value;
        //                                int heightOld = (int)HeightNumericUpD.Value;

        //                                int widthNew = (int)(widthOld * ((float)sourceImage.Width / (float)sourceImage.Height));
        //                                int heightNew = heightOld;
        //                                int appliedWidth = widthNew > WidthNumericUPD.Maximum ? (int)WidthNumericUPD.Maximum : widthNew;
        //                                int heightappliedAR = (int)((int)WidthNumericUPD.Maximum / ((float)sourceImage.Width / (float)sourceImage.Height));
        //                                int appliedHeight = appliedWidth == (int)WidthNumericUPD.Maximum ? heightappliedAR : heightNew;
        //                                imageControl.Image = new Bitmap(sourceImage, appliedWidth, appliedHeight);
        //                                IsheightDetermined = true;
        //                                WidthNumericUPD.Value = appliedWidth;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {


        //                    }



        //                    // WidthNumericUPD.Value = Width;

        //                    // WidthNumericUPD.Value = Width;
        //                }
        //                else
        //                {
        //                    imageControl.Image = new Bitmap(sourceImage, (int)WidthNumericUPD.Value, (int)HeightNumericUpD.Value);

        //                }
        //                imageControl.Size = imageControl.Image.Size;
        //            }

        //        }

        //    }

        //}

        private void WidthNumericUPD_Click(object sender, EventArgs e)
        {
            IsBothAlreadySet = false;
        }

        private void HeightNumericUpD_Click(object sender, EventArgs e)
        {
            IsBothAlreadySet = false;
        }

        private void AutoLabel5_Click(object sender, EventArgs e)
        {

        }
    }

    #region HelperClass
    public class TextLabel : Label
    {
        public bool IsSurveyData;
        public string key;

        public TextLabel(bool _issurveyData, string _key)
        {
            IsSurveyData = _issurveyData;
            key = _key;
        }

        public TextLabel(bool _issurveyData)
        {
            IsSurveyData = _issurveyData;

        }
    }

    public class ImageControl : PictureBox
    {
        //public Image ImageSource { get; set; }

        public string SourceLocation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageLocation { get; set; }
        public ImageControl()
        {
         
        }
        
    }
    
    public class ScreenPanel : Panel
    {
        public ScreenRatio ratio;
        Point[,] tiles;
        public int tileSize;
        public ScreenPanel(int _width, int _height, ScreenRatio _ratio, int _tileSize)
        {

            this.Size = new Size(_width, _height);
            this.AutoSize = false;
            this.BackColor = Color.Black;
            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.ratio = _ratio;
            tileSize = _tileSize;
            InitializeTiles();
            this.Paint += ScreenPanel_Paint;

        }


        private void ScreenPanel_Paint(object sender, PaintEventArgs e)
        {

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 255));
            Pen p = new Pen(semiTransBrush, 0.0000005f);
            
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                e.Graphics.DrawLine(p, tiles[i, 0].X, 0, tiles[i, 0].X, Size.Height);
               
            }
            for (int k = 0; k < tiles.GetLength(1); k++)
            {
                e.Graphics.DrawLine(p, 0, tiles[0, k].Y, Size.Width, tiles[0, k].Y);
            }

        }
        void InitializeTiles()
        {
            tiles = new Point[(int)Size.Width / tileSize, (int)Size.Height / tileSize];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int k = 0; k < tiles.GetLength(1); k++)
                {
                    tiles[i, k].X = i * tileSize;
                    tiles[i, k].Y = k * tileSize;
                }
            }
        }

        public Point GetPointAt(int x, int y)
        {
            //int X=Math.Min()
            //int X = Math.Min(Math.Max(0, x), tiles.GetLength(0));
            // int Y = Math.Min(Math.Max(0, y), tiles.GetLength(1));

            Point location = tiles[Math.Abs(x / tileSize), Math.Abs(y / tileSize)];
            //Console.WriteLine("x :" + x + " x/tilesize : " + x / tileSize);
            //Console.WriteLine("length of x :" + tiles.GetLength(0));
            //Console.WriteLine("y :" + y + " y/tilesize : " + y / tileSize);
            //Console.WriteLine("length of y:" + tiles.GetLength(1));
            return location;
        }

        public TextLabel[] GetTextLabels
        {
            get
            {
                IEnumerable<TextLabel> controlCollection = this.Controls.OfType<TextLabel>();
                TextLabel[] labels = controlCollection.ToArray<TextLabel>();

                return labels;
            }
        }

        public Point GetLocationReferringParent()
        {
            Control parent = this.Parent;
            int x = ((int)parent.Width / 2) - ((int)this.Width / 2);
            int y = ((int)parent.Height / 2) - ((int)this.Height / 2);
            Point f = new Point(x, y);
            return f;
        }
    }
    [Serializable]
    public class TextOverlay
    {

        string text;
        string DataBlockKey;
        float textSize;
        Point textLocation;
        double ScreenRatioDble;
        int PanelControlWidth;
        int RealWidth;
        int RealHeight;
        Font panelFont;
        public Font RealFont;
        Color textColor;
        ScreenRatio Ratio;
        public bool isDataString = false;

        public TextOverlay(string _text, float _textSize, Point point, int _screenSizeWdith, Font _font, Color _textColor, ScreenRatio _ratio)
        {
            text = _text;
            textSize = _textSize;
            textLocation = point;
            PanelControlWidth = _screenSizeWdith;
            panelFont = _font;
            textColor = _textColor;
            Ratio = _ratio;
            SetRatioDouble();

            MakeFont();
        }
        public TextOverlay(float _textSize, Point point, int _screenSizeWdith, Font _font, string _DatablockKey, Color _textColor, ScreenRatio _ratio)
        {
            isDataString = true;
            textSize = _textSize;
            textLocation = point;
            PanelControlWidth = _screenSizeWdith;
            panelFont = _font;
            DataBlockKey = _DatablockKey;
            textColor = _textColor;
            Ratio = _ratio;
            SetRatioDouble();

            if (SurveyData.NavConfigModel != null)
            {
                text = SurveyData.GetStringFromDictKey(_DatablockKey);
            }

            MakeFont();
        }
        public string GetTextFromSurveyData()
        {
            string txt = SurveyData.GetStringFromDictKey(DataBlockKey);
            return txt;
        }

        void SetRatioDouble()
        {
            switch (Ratio)
            {
                case ScreenRatio.NTSC:
                    ScreenRatioDble = (double)720 / 486;
                    RealWidth = 720;
                    RealHeight = (int)(RealWidth / ScreenRatioDble);
                    break;
                case ScreenRatio.PAL:
                    ScreenRatioDble = (double)720 / 576;
                    RealWidth = 720;
                    RealHeight = (int)(RealWidth / ScreenRatioDble);
                    break;
                case ScreenRatio.HD:
                    ScreenRatioDble = (double)1920 / 1080;
                    RealWidth = 1920;
                    RealHeight = (int)(RealWidth / ScreenRatioDble);
                    break;
                default:
                    break;
            }
        }

        private void MakeFont()
        {
            System.Drawing.Font font = new System.Drawing.Font(panelFont.FontFamily, TextSize, panelFont.Style);
            RealFont = font;
        }
        public Color Textcolor
        {
            get
            {
                return textColor;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
        }
        public float TextSize
        {
            get
            {
                float realSize = textSize * ((float)RealWidth / (float)PanelControlWidth);
                //Console.WriteLine(" real size : " + realSize);
               // float realSize = (int)(RealWidth / PanelControlWidth) > 0 ? textSize * (int)(RealWidth / PanelControlWidth) : (textSize / (PanelControlWidth / RealWidth));
                return realSize;
            }
        }

        public Point Location
        {
            get
            {
                int x = (int)(textLocation.X * ((float)RealWidth / (float)PanelControlWidth));
                //int x = (int)RealWidth / PanelControlWidth > 0 ? textLocation.X * ((int)RealWidth / PanelControlWidth) : (int)(textLocation.X / (PanelControlWidth / RealWidth));
                int heightPanel = (int)(PanelControlWidth / ScreenRatioDble);
                //int y = (int)(1080 / (heightPanel / textLocation.Y));  
                 int y = (int)(textLocation.Y * ((float)RealHeight / (float)heightPanel));
                //int y = (int)(RealHeight / heightPanel) > 0 ? textLocation.Y * (int)(RealHeight / heightPanel) : (int)(textLocation.Y / (heightPanel/ RealHeight));

                Point point = new Point(x, y);
                return point;
            }
        }
    }
    [Serializable]
    public class ImageOverlay
    {
        public int width;
        public int Height;
        public ImageOverlay()
        {

        }

    }
    [Serializable]
    public enum ScreenRatio
    {
        NTSC,
        PAL,
        HD
    }

    [Serializable]
    public class OverlayScreen
    {
        public bool IsIntroPage = false;
        public int Seconds;
        public ScreenRatio ScreenRatio;
        public TextOverlay[] TextOverlays;
        public ImageOverlay[] ImageOverlays;
        public OverlayScreen(ScreenRatio _screenRatio, TextOverlay[] _textOverlays)
        {
            ScreenRatio = _screenRatio;
            TextOverlays = _textOverlays;
        }
        public OverlayScreen(ScreenRatio _screenRatio, TextOverlay[] _textOverlays, bool _isIntroPage, int _sec)
        {
            ScreenRatio = _screenRatio;
            TextOverlays = _textOverlays;
            IsIntroPage = _isIntroPage;
            Seconds = _sec;
        }
    }

    public static class Overlay
    {
        public static OverlayScreen NTSCScreen;
        public static OverlayScreen PALScreen;
        public static OverlayScreen HDScreen;
        public static OverlayScreen NTSCIntroPage;
        public static OverlayScreen PALIntro;
        public static OverlayScreen HDIntro;
    }

    #endregion
}
