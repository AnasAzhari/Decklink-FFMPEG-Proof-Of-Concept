namespace SIR_Online
{
    partial class Overlay_Configuration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddTextNotSurveyBtn = new System.Windows.Forms.Button();
            this.AddTextISsurveyBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ColorPickerPanel = new System.Windows.Forms.Panel();
            this.SurveyKeyCB = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TextTB = new System.Windows.Forms.TextBox();
            this.Labeler = new System.Windows.Forms.Label();
            this.ApplyConfigBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.OverlayTabPage = new System.Windows.Forms.TabPage();
            this.OverlayTabGB = new System.Windows.Forms.GroupBox();
            this.IntroTabPage = new System.Windows.Forms.TabPage();
            this.DisplayDurationCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IntroPageTabControlGB = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ScreenRatioCB = new System.Windows.Forms.ComboBox();
            this.AddImageButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.OverlayTabPage.SuspendLayout();
            this.IntroTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddTextNotSurveyBtn
            // 
            this.AddTextNotSurveyBtn.Location = new System.Drawing.Point(12, 69);
            this.AddTextNotSurveyBtn.Name = "AddTextNotSurveyBtn";
            this.AddTextNotSurveyBtn.Size = new System.Drawing.Size(75, 40);
            this.AddTextNotSurveyBtn.TabIndex = 3;
            this.AddTextNotSurveyBtn.Text = "Add Text";
            this.AddTextNotSurveyBtn.UseVisualStyleBackColor = true;
            this.AddTextNotSurveyBtn.Click += new System.EventHandler(this.AddTextBtn_Click);
            // 
            // AddTextISsurveyBtn
            // 
            this.AddTextISsurveyBtn.Location = new System.Drawing.Point(12, 115);
            this.AddTextISsurveyBtn.Name = "AddTextISsurveyBtn";
            this.AddTextISsurveyBtn.Size = new System.Drawing.Size(75, 35);
            this.AddTextISsurveyBtn.TabIndex = 5;
            this.AddTextISsurveyBtn.Text = "Add Survey Data String";
            this.AddTextISsurveyBtn.UseVisualStyleBackColor = true;
            this.AddTextISsurveyBtn.Click += new System.EventHandler(this.AddTextISsurveyBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.ColorPickerPanel);
            this.groupBox2.Controls.Add(this.SurveyKeyCB);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TextTB);
            this.groupBox2.Controls.Add(this.Labeler);
            this.groupBox2.Location = new System.Drawing.Point(1070, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 185);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Opacity";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(54, 126);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 9;
            // 
            // ColorPickerPanel
            // 
            this.ColorPickerPanel.BackColor = System.Drawing.Color.White;
            this.ColorPickerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPickerPanel.Location = new System.Drawing.Point(140, 87);
            this.ColorPickerPanel.Name = "ColorPickerPanel";
            this.ColorPickerPanel.Size = new System.Drawing.Size(50, 23);
            this.ColorPickerPanel.TabIndex = 8;
            this.ColorPickerPanel.Click += new System.EventHandler(this.ColorPickerPanel_Click);
            // 
            // SurveyKeyCB
            // 
            this.SurveyKeyCB.FormattingEnabled = true;
            this.SurveyKeyCB.Location = new System.Drawing.Point(77, 32);
            this.SurveyKeyCB.Name = "SurveyKeyCB";
            this.SurveyKeyCB.Size = new System.Drawing.Size(128, 21);
            this.SurveyKeyCB.TabIndex = 7;
            this.SurveyKeyCB.SelectedIndexChanged += new System.EventHandler(this.SurveyKeyCB_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Set Font";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Font";
            // 
            // TextTB
            // 
            this.TextTB.Location = new System.Drawing.Point(40, 32);
            this.TextTB.Name = "TextTB";
            this.TextTB.Size = new System.Drawing.Size(128, 20);
            this.TextTB.TabIndex = 1;
            this.TextTB.Visible = false;
            this.TextTB.TextChanged += new System.EventHandler(this.TextTB_TextChanged);
            // 
            // Labeler
            // 
            this.Labeler.AutoSize = true;
            this.Labeler.Location = new System.Drawing.Point(6, 35);
            this.Labeler.Name = "Labeler";
            this.Labeler.Size = new System.Drawing.Size(28, 13);
            this.Labeler.TabIndex = 0;
            this.Labeler.Text = "Text";
            // 
            // ApplyConfigBtn
            // 
            this.ApplyConfigBtn.Location = new System.Drawing.Point(1248, 236);
            this.ApplyConfigBtn.Name = "ApplyConfigBtn";
            this.ApplyConfigBtn.Size = new System.Drawing.Size(79, 42);
            this.ApplyConfigBtn.TabIndex = 8;
            this.ApplyConfigBtn.Text = "Apply Configuration";
            this.ApplyConfigBtn.UseVisualStyleBackColor = true;
            this.ApplyConfigBtn.Click += new System.EventHandler(this.ApplyConfigBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.OverlayTabPage);
            this.tabControl1.Controls.Add(this.IntroTabPage);
            this.tabControl1.Location = new System.Drawing.Point(93, 44);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(971, 687);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // OverlayTabPage
            // 
            this.OverlayTabPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.OverlayTabPage.Controls.Add(this.OverlayTabGB);
            this.OverlayTabPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OverlayTabPage.Location = new System.Drawing.Point(4, 22);
            this.OverlayTabPage.Name = "OverlayTabPage";
            this.OverlayTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.OverlayTabPage.Size = new System.Drawing.Size(963, 661);
            this.OverlayTabPage.TabIndex = 0;
            this.OverlayTabPage.Text = "Overlay ";
            // 
            // OverlayTabGB
            // 
            this.OverlayTabGB.Location = new System.Drawing.Point(6, 35);
            this.OverlayTabGB.Name = "OverlayTabGB";
            this.OverlayTabGB.Size = new System.Drawing.Size(951, 653);
            this.OverlayTabGB.TabIndex = 3;
            this.OverlayTabGB.TabStop = false;
            // 
            // IntroTabPage
            // 
            this.IntroTabPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IntroTabPage.Controls.Add(this.DisplayDurationCB);
            this.IntroTabPage.Controls.Add(this.label4);
            this.IntroTabPage.Controls.Add(this.IntroPageTabControlGB);
            this.IntroTabPage.Location = new System.Drawing.Point(4, 22);
            this.IntroTabPage.Name = "IntroTabPage";
            this.IntroTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IntroTabPage.Size = new System.Drawing.Size(963, 661);
            this.IntroTabPage.TabIndex = 1;
            this.IntroTabPage.Text = "Intro Page";
            // 
            // DisplayDurationCB
            // 
            this.DisplayDurationCB.FormattingEnabled = true;
            this.DisplayDurationCB.Location = new System.Drawing.Point(254, 13);
            this.DisplayDurationCB.Name = "DisplayDurationCB";
            this.DisplayDurationCB.Size = new System.Drawing.Size(98, 21);
            this.DisplayDurationCB.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Display Duration";
            // 
            // IntroPageTabControlGB
            // 
            this.IntroPageTabControlGB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.IntroPageTabControlGB.Location = new System.Drawing.Point(6, 35);
            this.IntroPageTabControlGB.Name = "IntroPageTabControlGB";
            this.IntroPageTabControlGB.Size = new System.Drawing.Size(951, 623);
            this.IntroPageTabControlGB.TabIndex = 0;
            this.IntroPageTabControlGB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Display Type";
            // 
            // ScreenRatioCB
            // 
            this.ScreenRatioCB.FormattingEnabled = true;
            this.ScreenRatioCB.Location = new System.Drawing.Point(288, 20);
            this.ScreenRatioCB.Name = "ScreenRatioCB";
            this.ScreenRatioCB.Size = new System.Drawing.Size(121, 21);
            this.ScreenRatioCB.TabIndex = 1;
            this.ScreenRatioCB.SelectedIndexChanged += new System.EventHandler(this.ScreenRatioCB_SelectedIndexChanged);
            // 
            // AddImageButton
            // 
            this.AddImageButton.Location = new System.Drawing.Point(13, 157);
            this.AddImageButton.Name = "AddImageButton";
            this.AddImageButton.Size = new System.Drawing.Size(75, 32);
            this.AddImageButton.TabIndex = 10;
            this.AddImageButton.Text = "Add Image";
            this.AddImageButton.UseVisualStyleBackColor = true;
            this.AddImageButton.Click += new System.EventHandler(this.AddImageButton_Click);
            // 
            // Overlay_Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 758);
            this.Controls.Add(this.AddImageButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ScreenRatioCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApplyConfigBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.AddTextISsurveyBtn);
            this.Controls.Add(this.AddTextNotSurveyBtn);
            this.Name = "Overlay_Configuration";
            this.Text = "Overlay_Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Overlay_Configuration_FormClosing);
            this.Load += new System.EventHandler(this.Overlay_Configuration_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Overlay_Configuration_Paint);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.OverlayTabPage.ResumeLayout(false);
            this.IntroTabPage.ResumeLayout(false);
            this.IntroTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddTextNotSurveyBtn;
        private System.Windows.Forms.Button AddTextISsurveyBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TextTB;
        private System.Windows.Forms.Label Labeler;
        private System.Windows.Forms.Button ApplyConfigBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SurveyKeyCB;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage OverlayTabPage;
        private System.Windows.Forms.TabPage IntroTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ScreenRatioCB;
        private System.Windows.Forms.Panel ColorPickerPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox OverlayTabGB;
        private System.Windows.Forms.GroupBox IntroPageTabControlGB;
        private System.Windows.Forms.ComboBox DisplayDurationCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AddImageButton;
    }
}