using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using DeckLinkAPI;
using System.IO.Ports;

namespace SIR_Online

{
    
    public partial class ConfigureForm : Form
    {
        delegate void ControlEnableDelegate(Control control, bool enable);

        SIROnlineSession _SIROS;
        SurveyHelper SurveyHelper;
        RecordingSettings recordingSetting;
        
        public DeckLinkDeviceDiscovery m_deckLinkDiscovery;
        public Bgra32FrameConverter m_frameConverter;
        public DeckLinkInputDevice m_selectedCaptureDevice;
        public DeckLinkOutputDevice m_selectedPlaybackDevice;

        public FullDuplexDevice m_SelectedDuplexDevice;

        _BMDAudioSampleRate m_audioSampleRate = _BMDAudioSampleRate.bmdAudioSampleRate48kHz;
        _BMDAudioSampleType m_audioSampleDepth = _BMDAudioSampleType.bmdAudioSampleType16bitInteger;
        uint m_channelCount = 2;
        //
        public _BMDAudioSampleRate AudioSampleRate
        {
            get
            {
                return m_audioSampleRate;
            }
        }
        public _BMDAudioSampleType AudioSampleType
        {
            get
            {
                return m_audioSampleDepth;
            }
        }
        public uint ChannelCount
        {
            get
            {
                return m_channelCount;
            }
        }

        public DisplayModeEntry CurrentDisplayMode
        {
            get
            {
                return (DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem;
            }
        }
        

        public int ComboboxVideoModeFormatCurrIdx
        {
            get
            {
                return comboBoxCaptureVideoMode.SelectedIndex;
            }
        }
        public bool videoFormatDetection
        {
            get { return checkBoxEnableFormatDetection.Checked; }
        }


        public ConfigureForm()
        {
            
            InitializeComponent();
            _SIROS = SIROnlineSession.Instance;
            m_deckLinkDiscovery = new DeckLinkDeviceDiscovery();         
            m_frameConverter = new Bgra32FrameConverter();
            m_deckLinkDiscovery. DeviceArrived +=new EventHandler<DeckLinkDiscoveryEventArgs>((s, e) => this.Invoke((Action)(() => AddDuplexDevice(s, e))));
            m_deckLinkDiscovery.DeviceRemoved +=new EventHandler<DeckLinkDiscoveryEventArgs>((s, e) => this.Invoke((Action)(() => RemoveDuplexDevice(s, e))));
            checkBoxEnableFormatDetection.Checked = true;
            listBox1.SelectedIndex = 0;

            SurveyHelper = new SurveyHelper(serialPort1,this);
            recordingSetting = new RecordingSettings();
            
            InitializeSerialPortComboboxes();
            InitializeRecordingSetting();
            InitializeDataStringGB();


            //InitializeDataStringGB();
            // 


        }

        private IReadOnlyList<StringObjectPair<_BMDPixelFormat>> kPixelFormatList = new List<StringObjectPair<_BMDPixelFormat>>
        {
            new StringObjectPair<_BMDPixelFormat>("8-Bit YUV", _BMDPixelFormat.bmdFormat8BitYUV),
            new StringObjectPair<_BMDPixelFormat>("10-Bit YUV", _BMDPixelFormat.bmdFormat10BitYUV),
            new StringObjectPair<_BMDPixelFormat>("8-Bit ARGB", _BMDPixelFormat.bmdFormat8BitARGB),
            new StringObjectPair<_BMDPixelFormat>("8-Bit BGRA", _BMDPixelFormat.bmdFormat8BitBGRA),
            new StringObjectPair<_BMDPixelFormat>("10-Bit RGB", _BMDPixelFormat.bmdFormat10BitRGB),
            new StringObjectPair<_BMDPixelFormat>("12-Bit RGB", _BMDPixelFormat.bmdFormat12BitRGB),
            new StringObjectPair<_BMDPixelFormat>("12-Bit RGB LE", _BMDPixelFormat.bmdFormat12BitRGBLE),
            new StringObjectPair<_BMDPixelFormat>("10-Bit RGBX", _BMDPixelFormat.bmdFormat10BitRGBX),
            new StringObjectPair<_BMDPixelFormat>("10-Bit RGBX LE", _BMDPixelFormat.bmdFormat10BitRGBXLE)
        };


        //void AddDevice(object sender, DeckLinkDiscoveryEventArgs e)
        //{
        //    try
        //    {
        //        Console.WriteLine(" is device captureable ? :" + e.CaptureDevice + "is device Outputable :" + e.PlaybackDevice);
        //        DeckLinkInputDevice deckLink = new DeckLinkInputDevice(e.deckLink);
               
        //        comboBoxCaptureDevice.BeginUpdate();
        //        comboBoxCaptureDevice.Items.Add(new StringObjectPair<DeckLinkInputDevice>(deckLink.DeviceName, deckLink));
        //        comboBoxCaptureDevice.EndUpdate();
                

        //        // If first device, then enable capture interface
        //        if (comboBoxCaptureDevice.Items.Count == 1)
        //        {
        //            comboBoxCaptureDevice.SelectedIndex = 0;
        //            EnableComponent(CapturePanelConfig, true);
        //        }
        //    }
        //    catch (DecklinkInputInvalidException)
        //    {
        //        // Device likely playback only, eg DeckLink Mini Monitor
        //    }

        //    try
        //    {
        //        DeckLinkOutputDevice deckLink = new DeckLinkOutputDevice(e.deckLink);
                
                
               
        //        comboBoxPlaybackDevice.BeginUpdate();
        //        comboBoxPlaybackDevice.Items.Add(new StringObjectPair<DeckLinkOutputDevice>(deckLink.DeviceName, deckLink));
        //        comboBoxPlaybackDevice.EndUpdate();

        //        if (comboBoxPlaybackDevice.Items.Count == 1)
        //        {
        //            comboBoxPlaybackDevice.SelectedIndex = 0;
        //            EnableComponent(CapturePanelConfig, true);
        //        }
        //    }
        //    catch (DeckLinkOutputInvalidException)
        //    {
        //        // Device likely capture only, eg DeckLink Mini Recorder
        //    }
        //   // RefreshCaptureVideoModeList();
        //    //RefreshCapturePixelFormatList();
        //}

        void AddDuplexDevice(object sender,DeckLinkDiscoveryEventArgs e)
        {
            try
            {
                if(e.CaptureDevice && e.PlaybackDevice)
                {
                    FullDuplexDevice device = new FullDuplexDevice(e.deckLink, this);
                    ComboBoxFullDuplexDevice.BeginUpdate();
                    ComboBoxFullDuplexDevice.Items.Add(new StringObjectPair<FullDuplexDevice>(device.DeviceName, device));
                    ComboBoxFullDuplexDevice.EndUpdate();

                    

                }
                if(ComboBoxFullDuplexDevice.Items.Count>=1)
                {
                    ComboBoxFullDuplexDevice.SelectedIndex = 0;
                    EnableComponent(CaptureOptionGroupbox, true);
                }
            }
            catch 
            {

            }
            RefreshCaptureVideoModeList();
            RefreshCapturePixelFormatList();
        }

        void RemoveDuplexDevice(object sender,DeckLinkDiscoveryEventArgs e)
        {
            if(m_SelectedDuplexDevice !=null && m_SelectedDuplexDevice.DeckLink == e.deckLink)
            {
                if (m_SelectedDuplexDevice.inputDevice.isCapturing)
                {
                    m_SelectedDuplexDevice.inputDevice.StopCapture();
                    m_SelectedDuplexDevice.inputDevice.isRecording = false;
                    
                }
                m_SelectedDuplexDevice.outputDevice.DisableVideoOutput();
                m_SelectedDuplexDevice.outputDevice.DisableAudioOutput();

                ComboBoxFullDuplexDevice.BeginUpdate();
                foreach(StringObjectPair<FullDuplexDevice> item in ComboBoxFullDuplexDevice.Items)
                {
                    if (item.value.DeckLink == e.deckLink)
                    {
                        ComboBoxFullDuplexDevice.Items.Remove(item);
                        break;
                    }
                }
                ComboBoxFullDuplexDevice.EndUpdate();
                if (ComboBoxFullDuplexDevice.Items.Count == 0)
                {
                    EnableComponent(ConfigPanel, false);
                    m_SelectedDuplexDevice = null;
                }
            }

        }

        //void RemoveDevice(object sender, DeckLinkDiscoveryEventArgs e)
        //{
        //    // Stop capture thread if the selected capture device was removed
        //    if (m_selectedCaptureDevice != null && m_selectedCaptureDevice.DeckLink == e.deckLink )
        //    {
        //        if (m_selectedCaptureDevice.isCapturing)
        //            m_selectedCaptureDevice.StopCapture();
        //    }

        //    // Stop playback thread if the selected playback device was removed
        //    if (m_selectedPlaybackDevice != null && m_selectedPlaybackDevice.DeckLink == e.deckLink )
        //    {
               
        //        m_selectedPlaybackDevice.DisableVideoOutput();
        //    }

        //    // Remove the device from the capture dropdown
        //    comboBoxCaptureDevice.BeginUpdate();
        //    foreach (StringObjectPair<DeckLinkInputDevice> item in comboBoxCaptureDevice.Items)
        //    {
        //        if (item.value.DeckLink == e.deckLink)
        //        {
        //            comboBoxCaptureDevice.Items.Remove(item);
        //            break;
        //        }
        //    }
        //    comboBoxCaptureDevice.EndUpdate();

        //    if (comboBoxCaptureDevice.Items.Count == 0)
        //    {
        //        EnableComponent(CapturePanelConfig, false);
        //        m_selectedCaptureDevice = null;
        //    }
        //    else if (m_selectedCaptureDevice.DeckLink == e.deckLink)
        //    {
        //        comboBoxCaptureDevice.SelectedIndex = 0;
        //    }

        //    // Remove the device from the playback dropdown
        //    comboBoxPlaybackDevice.BeginUpdate();
        //    foreach (StringObjectPair<DeckLinkOutputDevice> item in comboBoxPlaybackDevice.Items)
        //    {
        //        if (item.value.DeckLink == e.deckLink)
        //        {
        //            comboBoxPlaybackDevice.Items.Remove(item);
        //            break;
        //        }
        //    }
        //    comboBoxPlaybackDevice.EndUpdate();

        //    if (comboBoxPlaybackDevice.Items.Count == 0)
        //    {
        //        //EnableComponent(groupBoxPlayback, false);
        //        m_selectedPlaybackDevice = null;
        //    }
        //    else if (m_selectedPlaybackDevice.DeckLink == e.deckLink)
        //    {
        //        comboBoxPlaybackDevice.SelectedIndex = 0;
        //    }
        //}

        private void EnableGroupInterface(GroupBox group, bool enable)
        {
            // Get list of groupbox child controls, filter out button
            var groupControls = group.Controls.Cast<Control>();
            foreach (Control control in groupControls.Where((ctrl, ret) => { return (ctrl.GetType() != typeof(Button)); }))
                EnableComponent(control, enable);
        }


        private void EnableComponent(Control control, bool enable)
        {
            if (control.InvokeRequired)
                this.Invoke(new ControlEnableDelegate(EnableComponent), new object[] { control, enable });
            else
                control.Enabled = enable;
        }


        public void CreateVideoFrame()
        {
            
        }


        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        struct StringObjectPair<T>
        {
            public string name;
            public T value;

            public StringObjectPair(string name, T value)
            {
                this.name = name;
                this.value = value;
            }

            public override string ToString()
            {
                return name;
            }
        }

        //private void comboBoxCaptureDevice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    m_selectedCaptureDevice = null;

        //    if (comboBoxCaptureDevice.SelectedIndex < 0)
        //        return;

        //    m_selectedCaptureDevice = ((StringObjectPair<DeckLinkInputDevice>)comboBoxCaptureDevice.SelectedItem).value;
           

        //    RefreshCaptureVideoModeList();
        //}

        private void ComboBoxFullDuplexDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectedDuplexDevice = null;
            if (ComboBoxFullDuplexDevice.SelectedIndex < 0)
            {
                return;
            }
            m_SelectedDuplexDevice = ((StringObjectPair<FullDuplexDevice>)ComboBoxFullDuplexDevice.SelectedItem).value;
            RefreshCaptureVideoModeList();

        }

        private void RefreshCaptureVideoModeList()
        {
            if (m_SelectedDuplexDevice != null)
            {
                comboBoxCaptureVideoMode.BeginUpdate();
                comboBoxCaptureVideoMode.Items.Clear();

                foreach (IDeckLinkDisplayMode displayMode in m_SelectedDuplexDevice.inputDevice)
                    comboBoxCaptureVideoMode.Items.Add(new DisplayModeEntry(displayMode));

                comboBoxCaptureVideoMode.SelectedIndex = 0;
                comboBoxCaptureVideoMode.EndUpdate();

                // Need to refresh pixel format list
                RefreshCapturePixelFormatList();
            }
        }
        private void RefreshCapturePixelFormatList()
        {
            comboBoxCapturePixelFormat.BeginUpdate();
            comboBoxCapturePixelFormat.Items.Clear();
            
            if(m_SelectedDuplexDevice == null)
            {
                Console.WriteLine(" device is null");
            }
            if (comboBoxCaptureVideoMode.SelectedItem!= null){
                //Console.WriteLine((DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem);
                foreach (StringObjectPair<_BMDPixelFormat> pixelFormat in kPixelFormatList.Where((pf, ret) => { return (m_SelectedDuplexDevice.inputDevice.IsvideoModeSupported(((DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem).displayMode, pf.value)); }))
                {
                    comboBoxCapturePixelFormat.Items.Add(pixelFormat);
                    // Console.WriteLine(" pixel mode supported : " + pixelFormat.name);
                }

                if (comboBoxCapturePixelFormat.Items.Count > 0)
                {
                    comboBoxCapturePixelFormat.SelectedIndex = 0;
                }
                else
                {
                    comboBoxCapturePixelFormat.Text = null;

                    comboBoxCapturePixelFormat.EndUpdate();
                    return;
                }
                comboBoxCapturePixelFormat.EndUpdate();
            }       
        }

        private void comboBoxCaptureVideoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCapturePixelFormatList();
        }

        public void DisplayModeChanged(object sender,DeckLinkInputFormatChangedEventArgs e)
        {
            if (e.notificationEvents.HasFlag(_BMDVideoInputFormatChangedEvents.bmdVideoInputDisplayModeChanged))
            {
                // Video input mode has changed, update combo-box
                foreach (DisplayModeEntry item in comboBoxCaptureVideoMode.Items)
                {
                    if (item.displayMode.GetDisplayMode() == e.displayMode)
                        this.Invoke(new MethodInvoker(() => { comboBoxCaptureVideoMode.SelectedItem = item; }));
                }
            }
            if (e.notificationEvents.HasFlag(_BMDVideoInputFormatChangedEvents.bmdVideoInputColorspaceChanged))
            {
                // Input pixel format has changed, update combo-box
                foreach (StringObjectPair<_BMDPixelFormat> item in comboBoxCapturePixelFormat.Items)
                {
                    if (item.value == e.pixelFormat)
                        comboBoxCaptureVideoMode.SelectedItem = item;
                }
            }
        }

        //private void comboBoxPlaybackDevice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    m_selectedPlaybackDevice = null;

        //    if (comboBoxPlaybackDevice.SelectedIndex < 0)
        //        return;

        //    m_selectedPlaybackDevice = ((StringObjectPair<DeckLinkOutputDevice>)comboBoxPlaybackDevice.SelectedItem).value;
            
        //   // m_Keyer = (IDeckLinkKeyer)m_selectedPlaybackDevice;
        //    //RefreshPlaybackVideoModeList();
        //    RefreshCaptureVideoModeList();
        //}

        private void checkBoxEnableFormatDetection_CheckedChanged(object sender, EventArgs e)
        {
            EnableComponent(comboBoxCaptureVideoMode, !checkBoxEnableFormatDetection.Checked);
            EnableComponent(comboBoxCapturePixelFormat, !checkBoxEnableFormatDetection.Checked);
            RefreshCaptureVideoModeList();
            RefreshCapturePixelFormatList();
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            m_deckLinkDiscovery.Enable();
            ConfigPanel.Show();
            

        }

        private void comboBoxCapturePixelFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                // capture and playback 
                case 0:
                    
                    CaptureOptionGroupbox.Parent = ConfigPanel;
                    CaptureOptionGroupbox.Dock = DockStyle.Fill;
                    CaptureOptionGroupbox.Show();

                    DataBlockGB.Hide();
                    DataStringGB.Hide();
                    RecordingGB.Hide();

                    break;
          
                // datastring GB
                case 1:
                    DataStringGB.Parent = ConfigPanel;
                    DataStringGB.Dock = DockStyle.Fill;
                    DataStringGB.Show();

                    DataBlockGB.Hide();
                    CaptureOptionGroupbox.Hide();
                    RecordingGB.Hide();
                    break;

                // Recording GB
                case 3:
                    RecordingGB.Parent = ConfigPanel;
                    RecordingGB.Dock = DockStyle.Fill;
                    RecordingGB.Show();

                    DataBlockGB.Hide();
                    CaptureOptionGroupbox.Hide();
                    DataStringGB.Hide();

                    break;
                // Data Block GB
                case 4:
                    DataBlockGB.Parent = ConfigPanel;
                    DataBlockGB.Dock = DockStyle.Fill;
                    DataBlockGB.Show();

                    CaptureOptionGroupbox.Hide();
                    DataStringGB.Hide();
                    RecordingGB.Hide();

                    break;
         
                default:
                    break;

            }
        }
        #region Serial Port

        public void InitializeSerialPortComboboxes()
        {
            if(SurveyHelper != null)
            {
                COMportCB.BeginUpdate();
                foreach(string ports in SurveyHelper.comPorts)
                {
                    COMportCB.Items.Add(new StringObjectPair<string>(ports.ToString(),ports));
                }
                COMportCB.EndUpdate();
                COMportCB.SelectedIndex = 0;

                BaudRateCB.BeginUpdate();
                
                foreach (int item in SurveyHelper.baudRateList)
                {
                    BaudRateCB.Items.Add(new StringObjectPair<int>(item.ToString(), item));
                }
                BaudRateCB.EndUpdate();
                Console.WriteLine(" baud rate counts :" + BaudRateCB.Items.Count);
                BaudRateCB.SelectedIndex = 0;

                ParityCB.BeginUpdate();
                foreach (Parity item in SurveyHelper.parities)
                {
                    ParityCB.Items.Add(new StringObjectPair<Parity>(item.ToString(), item));
                }
                ParityCB.EndUpdate();
                Console.WriteLine(" parity counts :" + ParityCB.Items.Count);
                ParityCB.SelectedIndex = 0;

                DataBitsCB.BeginUpdate();
                foreach (var item in SurveyHelper.databitList)
                {
                    DataBitsCB.Items.Add(new StringObjectPair<int>(item.ToString(),item));
                }
                DataBitsCB.EndUpdate();
                DataBitsCB.SelectedIndex = 0;

                StopBitsCB.BeginUpdate();
                foreach (var item in SurveyHelper.StopBitList)
                {
                    StopBitsCB.Items.Add(new StringObjectPair<StopBits>(item.ToString(),item));
                }
                StopBitsCB.EndUpdate();
                StopBitsCB.SelectedIndex = 0;



            }

          }

        private void COMportCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(COMportCB.Items != null)
            {
                
                SurveyHelper.cOMPort = ((StringObjectPair<string>)COMportCB.SelectedItem).value;
                Console.WriteLine("which comport :" + SurveyHelper.cOMPort);
            }
        }

        private void OpenPortButton_Click(object sender, EventArgs e)
        {
            if (SurveyHelper.SerialPort.IsOpen == false)
                try
                {
                    SurveyHelper.OpenPort();
                    if (serialPort1.IsOpen)
                    {
                        IsPortOpenLbl.Text = "port is open";
                        OpenPortButton.Text = "Close Port";
                    }
                    else
                    {
                        IsPortOpenLbl.Text = "port is NOT open";
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            else
            {
                serialPort1.Close();
                OpenPortButton.Text = "Open Port";
                IsPortOpenLbl.Text = "port is NOT open";
            }
        }


        //private void COMportCB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SurveyHelper.cOMPort = (string)COMportCB.SelectedValue;

        //}

        //private void OpenPortButton_Click(object sender, EventArgs e)
        //{
        //    if(serialPort1.IsOpen==false)
        //       serialPort1.Open();
        //}
        private void ParityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParityCB.Items != null)
            {             
                //((StringObjectPair<FullDuplexDevice>)ComboBoxFullDuplexDevice.SelectedItem).value;
                SurveyHelper.parity = ((StringObjectPair<Parity>)ParityCB.SelectedItem).value;
                Console.WriteLine(" parity :" + SurveyHelper.parity.ToString());
            }      
        }
        private void DataBitsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DataBitsCB.Items != null)
            {
                SurveyHelper.dataBits = ((StringObjectPair<int>)DataBitsCB.SelectedItem).value;
            }
                
        }

        private void StopBitsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(StopBitsCB.Items != null)
            {
                SurveyHelper.stopBits = ((StringObjectPair<StopBits>)StopBitsCB.SelectedItem).value;
            }
                
        }
        public void UpdateDataStringLabel(string lbltime, float kp, float easting, float northing)
        {
    
                this.Invoke(new MethodInvoker(() =>
                {
                    TimeSurveyLbl.Text = lbltime;
                    kpsurveyLbl.Text = kp.ToString();
                    eastingLbl.Text = easting.ToString();
                    northingLbl.Text = northing.ToString();
                }));
        }

        #endregion

        #region Recording
        public void InitializeRecordingSetting()
        {
            if (recordingSetting != null)
            {
                CutOffCB.BeginUpdate();
                foreach (var item in RecordingSettings.cutOffOptions)
                {
                    CutOffCB.Items.Add(new StringObjectPair<int>(item.ToString(), item));

                }
                CutOffCB.EndUpdate();
                CutOffCB.SelectedIndex = 0;
            }     
        }

        private void CutOffCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CutOffCB.Items != null)
            {
                recordingSetting.CuttOffDuration = ((StringObjectPair<int>)CutOffCB.SelectedItem).value;               
               
            }
        }

        #endregion

        #region DataString
        int BlockNumber;
        
        public void InitializeDataStringGB()
        {
            BlockNumber = 0;
            numericUpDown1.Value = 5;
            BlockNumber = (int)numericUpDown1.Value;

            numericUpDown1.Maximum = 7;

            BuildDataBlockCOntrols();
            ApplyDataBlock();

        }
        void BuildDataBlockCOntrols()
        {
            for (int i = 0; i < BlockNumber; i++)
            {
                DataBlockPanel dbp = new DataBlockPanel(BlockNumber, i);
                TextBox Name = (TextBox)(dbp.Controls.Find("DataBlockTB", true)[0]);
                ComboBox Order = (ComboBox)(dbp.Controls.Find("DataBlockOrderCB", true)[0]);
                Name.Text = ("DataBlock" + i);
                flowLayoutPanel1.Controls.Add(dbp);
            }
        }


        private void ApplyDataBlockBtn_Click(object sender, EventArgs e)
        {
            ApplyDataBlock();
          
        }
        public void ApplyDataBlock()
        {
            NavConfigModel navConfigModel = new NavConfigModel();
            if (IsCommaChkBox.Checked)
            {
                try
                {
                    navConfigModel.CommaFieldIndexes = new Dictionary<string, int>();
                    foreach (DataBlockPanel item in flowLayoutPanel1.Controls)
                    {
                        TextBox Name = (TextBox)(item.Controls.Find("DataBlockTB", true)[0]);
                        ComboBox Order = (ComboBox)(item.Controls.Find("DataBlockOrderCB", true)[0]);

                        navConfigModel.CommaFieldIndexes.Add(Name.Text, (int)Order.SelectedItem);

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
            SurveyData.NavConfigModel = navConfigModel;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int oldBlockNumber = BlockNumber;
            BlockNumber = (int)numericUpDown1.Value;

            //if (BlockNumber - oldBlockNumber > 0)
            //{
            //    flowLayoutPanel1.Controls.Add(new DataBlockPanel());
            //}
            //else
            //{
            //    flowLayoutPanel1.Controls[BlockNumber].Dispose();
            //}
            foreach (Control item in flowLayoutPanel1.Controls)
            {
               
               
            }
            flowLayoutPanel1.Controls.Clear();

            BuildDataBlockCOntrols();
            //Console.WriteLine(" block number :" + BlockNumber);
        }


        #endregion


    }
}
