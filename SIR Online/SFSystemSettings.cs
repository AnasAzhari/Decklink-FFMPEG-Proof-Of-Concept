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
using SIRModel;
//using Accord.Video.FFMPEG;

namespace SIR_Online
{
    public partial class SFSystemSettings : Form
    {
        delegate void ControlEnableDelegate(Control control, bool enable);

        SIROnlineSession _SIROS;
        SurveyHelper SurveyHelper;
        RecordingSettings recordingSetting;

        public DeckLinkDeviceDiscovery m_deckLinkDiscovery;
        public Bgra32FrameConverter m_frameConverter;
        public DeckLinkInputDevice m_selectedCaptureDevice;
        public DeckLinkOutputDevice m_selectedPlaybackDevice;

        public FullDuplexDevice M_selectedDecklinkDevice;
        private List<FullDuplexDevice> _fullDuplexDevice = new List<FullDuplexDevice>();

        // public List<DeckLinkInputDevice>
        int deviceCount;
        public List<FullDuplexDevice> GetFullDuplexDevices
        {
            get { return _fullDuplexDevice; }protected set { }
        }

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
        public VideoCodec SelectedVideoCOdec
        {
            get
            {
                //((StringObjectPair<Parity>)ParityCB.SelectedItem).value
                return ((StringObjectPair<VideoCodec>)RecordingCodecCB.SelectedItem).value;
            }
        }

        private static IReadOnlyList<StringObjectPair<_BMDPixelFormat>> kPixelFormatList = new List<StringObjectPair<_BMDPixelFormat>>
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
        public SFSystemSettings()
        {
            InitializeComponent();
            deviceCount = 0;
            m_deckLinkDiscovery = new DeckLinkDeviceDiscovery();
            m_frameConverter = new Bgra32FrameConverter();
           

            
            SurveyHelper = new SurveyHelper(serialPort1, this);
            recordingSetting = new RecordingSettings();

            InitializeSerialPortComboboxes();
            InitializeRecordingSetting();
            InitializeDataStringGB();
            InitializeAudioRateCB();
            InitializeAudioSampleDepth();
            _fullDuplexDevice = new List<FullDuplexDevice>();



        }
    
        private void SFSystemSettings_Load(object sender, EventArgs e)
        {
            _SIROS = SIROnlineSession.Instance;
            //m_deckLinkDiscovery.Enable();
            //m_deckLinkDiscovery.DeviceRemoved += new EventHandler<DeckLinkDiscoveryEventArgs>((s, e) => this.Invoke((Action)(() => RemoveDuplexDevice(s, e))));
            checkBoxEnableFormatDetection.Checked = true;
           
            Console.WriteLine(" discovered :" + m_deckLinkDiscovery);
            TurnOnDecklinkDsicovery();


        }
        public void TurnOnDecklinkDsicovery()
        {
            m_deckLinkDiscovery.Enable();
            m_deckLinkDiscovery.DeviceArrived += new EventHandler<DeckLinkDiscoveryEventArgs>((s, ex) => this.Invoke((Action)(() => AddDecklinkDevice(s, ex))));
            //m_deckLinkDiscovery.DeviceRemoved += new EventHandler<DeckLinkDiscoveryEventArgs>((s, e) => this.Invoke((Action)(() => RemoveDuplexDevice(s, e))));
        }

        #region Capture Settings
        void AddDecklinkDevice(object sender, DeckLinkDiscoveryEventArgs e)
        {
           // Console.WriteLine(" device arrived ");
            try
            {
                if (e.CaptureDevice && e.PlaybackDevice)
                {
                    string name;
                    e.deckLink.GetDisplayName(out name);

                    FullDuplexDevice device = new FullDuplexDevice(name,e.deckLink);
                    DecklinkDeviceCB.Update();
                    DecklinkDeviceCB.Items.Add(new StringObjectPair<FullDuplexDevice>(device.DeviceName, device));
                  
                }
                if (DecklinkDeviceCB.Items.Count >= 1)
                {
                    DecklinkDeviceCB.SelectedIndex = 0;
                    // EnableComponent(CaptureOptionGroupbox, true);
                }

                //if(e.CaptureDevice && e.PlaybackDevice)
                //{
                //    if (deviceCount == 0 || deviceCount % 2 == 0)
                //    {
                //        DeckLinkInputDevice deckLinkDevice = new DeckLinkInputDevice(e.deckLink);
                //        DecklinkDeviceCB.Update();
                //        DecklinkDeviceCB.Items.Add(new StringObjectPair<DeckLinkInputDevice>(deckLinkDevice.DeviceName, deckLinkDevice));
                //        Console.WriteLine(" deck link input Device : " + deckLinkDevice.DeviceName);
                //    }
                //    else
                //    {
                //        DeckLinkOutputDevice deckLinkDevice = new DeckLinkOutputDevice(e.deckLink);
                //        DecklinkDeviceCB.Update();
                //        DecklinkDeviceCB.Items.Add(new StringObjectPair<DeckLinkOutputDevice>(deckLinkDevice.DeviceName, deckLinkDevice));
                //        Console.WriteLine(" deck link output Device : " + deckLinkDevice.DeviceName);
                //    }
                //    deviceCount++;
                //   // DecklinkDeviceCB.

                //}

            }
            catch
            {

            }
            RefreshCaptureVideoModeList();
            RefreshCapturePixelFormatList();
        }
        //void RemoveDuplexDevice(object sender, DeckLinkDiscoveryEventArgs e)
        //{
        //    if (m_SelectedDuplexDevice != null && m_SelectedDuplexDevice.DeckLink == e.deckLink)
        //    {
        //        if (m_SelectedDuplexDevice.inputDevice.isCapturing)
        //        {
        //            m_SelectedDuplexDevice.inputDevice.StopCapture();
        //            m_SelectedDuplexDevice.inputDevice.isRecording = false;

        //        }
        //        m_SelectedDuplexDevice.outputDevice.DisableVideoOutput();
        //        m_SelectedDuplexDevice.outputDevice.DisableAudioOutput();

        //        ComboBoxFullDuplexDevice.Update();
        //        foreach (StringObjectPair<FullDuplexDevice> item in ComboBoxFullDuplexDevice.Items)
        //        {
        //            if (item.value.DeckLink == e.deckLink)
        //            {
        //                ComboBoxFullDuplexDevice.Items.Remove(item);
        //                break;
        //            }
        //        }
        //        //ComboBoxFullDuplexDevice.EndUpdate();
        //        if (ComboBoxFullDuplexDevice.DataBindings.Count == 0)
        //        {
        //            // EnableComponent(ConfigPanel, false);
        //            m_SelectedDuplexDevice = null;
        //        }
        //    }

        //}

        private void RefreshCaptureVideoModeList()
        {
            if (M_selectedDecklinkDevice != null)
            {
                comboBoxCaptureVideoMode.Update();
                comboBoxCaptureVideoMode.Items.Clear();

                foreach (IDeckLinkDisplayMode displayMode in M_selectedDecklinkDevice.inputDevice)
                    comboBoxCaptureVideoMode.Items.Add(new DisplayModeEntry(displayMode));

                comboBoxCaptureVideoMode.SelectedIndex = 0;
                //comboBoxCaptureVideoMode.EndUpdate();
                
                // Need to refresh pixel format list
                RefreshCapturePixelFormatList();
            }
        }
        private void RefreshCapturePixelFormatList()
        {
            comboBoxCapturePixelFormat.Update();
            comboBoxCapturePixelFormat.Items.Clear();

            if (M_selectedDecklinkDevice == null)
            {
                Console.WriteLine(" device is null");
            }
            if (comboBoxCaptureVideoMode.SelectedItem != null)
            {
                //Console.WriteLine((DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem);
                foreach (StringObjectPair<_BMDPixelFormat> pixelFormat in kPixelFormatList.Where((pf, ret) => { return (M_selectedDecklinkDevice.inputDevice.IsvideoModeSupported(((DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem).displayMode, pf.value)); }))
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

                   
                    return;
                }
                
            }
        }

        private void comboBoxCaptureVideoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (M_selectedDecklinkDevice != null && comboBoxCaptureVideoMode.SelectedItem != null)
            {
                DisplayModeEntry modeEntry = ((DisplayModeEntry)comboBoxCaptureVideoMode.SelectedItem);
                IDeckLinkDisplayMode displayMode = modeEntry.displayMode;
                _BMDDisplayMode dm = displayMode.GetDisplayMode();
                M_selectedDecklinkDevice.currentDisplay = dm;
                //M_selectedDecklinkDevice.currentDisplay = ((StringObjectPair<DisplayModeEntry>)comboBoxCaptureVideoMode.SelectedItem).value.displayMode.GetDisplayMode();
                RefreshCapturePixelFormatList();
            }
        }

        public void DisplayModeChanged(object sender, DeckLinkInputFormatChangedEventArgs e)
        {
            //Console.WriteLine("");
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

        private void checkBoxEnableFormatDetection_CheckedChanged(object sender, EventArgs e)
        {
            //  EnableComponent(comboBoxCaptureVideoMode, !checkBoxEnableFormatDetection.Checked);
            //EnableComponent(comboBoxCapturePixelFormat, !checkBoxEnableFormatDetection.Checked);
            if(M_selectedDecklinkDevice != null)
            {
                M_selectedDecklinkDevice.VideoFormatDetection = checkBoxEnableFormatDetection.Checked;
            }
            RefreshCaptureVideoModeList();
            RefreshCapturePixelFormatList();
        }

        private void SFSystemSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        //private void ComboBoxFullDuplexDevice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    M_selectedDecklinkInput = null;
        //    if (DecklinkDeviceCB.SelectedIndex < 0)
        //    {
        //        return;
        //    }
        //    //if(DecklinkDeviceCB.SelectedIndex==0|| DecklinkDeviceCB.SelectedIndex % 2 == 0)
        //    //{
        //    //    M_selectedDecklinkInput = ((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.SelectedItem).value;
        //    //    RefreshCaptureVideoModeList();
        //    //}
        //    M_selectedDecklinkInput = ((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.SelectedItem).value;
        //    RefreshCaptureVideoModeList();

        //}
        #endregion

        #region Serial Port
        public void InitializeSerialPortComboboxes()
        {
            if(SurveyHelper != null)
            {
                COMportCB.Update();
                foreach(string ports in SurveyHelper.comPorts)
                {
                    COMportCB.Items.Add(new StringObjectPair<string>(ports.ToString(),ports));
                }
                //COMportCB.EndUpdate();
                COMportCB.SelectedIndex = 0;

                BaudRateCB.Update();
                
                foreach (int item in SurveyHelper.baudRateList)
                {
                    BaudRateCB.Items.Add(new StringObjectPair<int>(item.ToString(), item));
                }
                //BaudRateCB.EndUpdate();
                Console.WriteLine(" baud rate counts :" + BaudRateCB.Items.Count);
                BaudRateCB.SelectedIndex = 0;

                ParityCB.Update();
                foreach (Parity item in SurveyHelper.parities)
                {
                    ParityCB.Items.Add(new StringObjectPair<Parity>(item.ToString(), item));
                }
                //ParityCB.EndUpdate();
                Console.WriteLine(" parity counts :" + ParityCB.Items.Count);
                ParityCB.SelectedIndex = 0;

                DataBitsCB.Update();
                foreach (var item in SurveyHelper.databitList)
                {
                    DataBitsCB.Items.Add(new StringObjectPair<int>(item.ToString(),item));
                }
               // DataBitsCB.EndUpdate();
                DataBitsCB.SelectedIndex = 0;

                StopBitsCB.Update();
                foreach (var item in SurveyHelper.StopBitList)
                {
                    StopBitsCB.Items.Add(new StringObjectPair<StopBits>(item.ToString(),item));
                }
                //StopBitsCB.EndUpdate();
                StopBitsCB.SelectedIndex = 0;



            }

          }

        //private void COMportCB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(COMportCB.Items != null)
        //    {

        //        SurveyHelper.cOMPort = ((StringObjectPair<string>)COMportCB.SelectedItem).value;
        //        Console.WriteLine("which comport :" + SurveyHelper.cOMPort);
        //    }
        //}




        ////private void COMportCB_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    SurveyHelper.cOMPort = (string)COMportCB.SelectedValue;

        ////}

        ////private void OpenPortButton_Click(object sender, EventArgs e)
        ////{
        ////    if(serialPort1.IsOpen==false)
        ////       serialPort1.Open();
        ////}
        //private void ParityCB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ParityCB.Items != null)
        //    {             
        //        //((StringObjectPair<FullDuplexDevice>)ComboBoxFullDuplexDevice.SelectedItem).value;
        //        SurveyHelper.parity = ((StringObjectPair<Parity>)ParityCB.SelectedItem).value;
        //        Console.WriteLine(" parity :" + SurveyHelper.parity.ToString());
        //    }      
        //}
        //private void DataBitsCB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(DataBitsCB.Items != null)
        //    {
        //        SurveyHelper.dataBits = ((StringObjectPair<int>)DataBitsCB.SelectedItem).value;
        //    }

        //}

        //private void StopBitsCB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(StopBitsCB.Items != null)
        //    {
        //        SurveyHelper.stopBits = ((StringObjectPair<StopBits>)StopBitsCB.SelectedItem).value;
        //    }

        //}

        private void COMportCB_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (COMportCB.Items != null)
            {

                SurveyHelper.cOMPort = ((StringObjectPair<string>)COMportCB.SelectedItem).value;
                Console.WriteLine("which comport :" + SurveyHelper.cOMPort);
            }
        }

        private void BaudRateCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BaudRateCB.Items != null)
            {
                SurveyHelper.baudRate= ((StringObjectPair<int>)BaudRateCB.SelectedItem).value;
            }
        }

        private void ParityCB_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ParityCB.Items != null)
            {
                //((StringObjectPair<FullDuplexDevice>)ComboBoxFullDuplexDevice.SelectedItem).value;
                SurveyHelper.parity = ((StringObjectPair<Parity>)ParityCB.SelectedItem).value;
                Console.WriteLine(" parity :" + SurveyHelper.parity.ToString());
            }
        }

        private void DataBitsCB_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (DataBitsCB.Items != null)
            {
                SurveyHelper.dataBits = ((StringObjectPair<int>)DataBitsCB.SelectedItem).value;
            }
        }

        private void StopBitsCB_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (StopBitsCB.Items != null)
            {
                SurveyHelper.stopBits = ((StringObjectPair<StopBits>)StopBitsCB.SelectedItem).value;
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
                        //IsPortOpenLbl.Text = "port is open";
                        OpenPortButton.Text = "Close Port";
                    }
                    else
                    {
                        //IsPortOpenLbl.Text = "port is NOT open";
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
                //IsPortOpenLbl.Text = "port is NOT open";
            }
        }


        #endregion

        #region Recording
        public void InitializeRecordingSetting()
        {
            if (recordingSetting != null)
            {
                CutOffCB.Update();
                foreach (var item in RecordingSettings.cutOffOptions)
                {
                    CutOffCB.Items.Add(new StringObjectPair<int>(item.ToString(), item));

                }
                //CutOffCB.EndUpdate();
                CutOffCB.SelectedIndex = 0;
            }

            RecordingCodecCB.Items.Clear();
           // RecordingCodecCB.Items.Add(new StringObjectPair<VideoCodec>(VideoCodec.MPEG4.ToString(), VideoCodec.MPEG4));
           // RecordingCodecCB.Items.Add(new StringObjectPair<VideoCodec>(VideoCodec.WMV1.ToString(), VideoCodec.WMV1));
            
            //RecordingCodecCB.Items.Add(new StringObjectPair<VideoCodec>(VideoCodec.H264.ToString(), VideoCodec.H264));
            //RecordingCodecCB.Items.Add(new StringObjectPair<VideoCodec>(VideoCodec.FLV1.ToString(), VideoCodec.FLV1));
            RecordingCodecCB.SelectedIndex = 0;
        }


        private void CutOffCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CutOffCB.Items != null)
            {
                recordingSetting.CuttOffDuration = ((StringObjectPair<int>)CutOffCB.SelectedItem).value;

            }
        }
        private void RecordingCodecCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string FFMPEGexe;
        private void FfmpegBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.AddExtension = true;

            DialogResult ret = STAShowDialog(dialog);
            if (ret == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    FFMPEGexe = dialog.FileName;
                    FFMpegLocTB.Text = FFMPEGexe;
                }
                //string filename = dialog.FileName;
                //IFormatter formatter = new BinaryFormatter();
                //Console.WriteLine("filename :" + filename);
                //Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //OnlineProject currproj = (OnlineProject)formatter.Deserialize(stream);
                //_SIROS.CurrentProject = currproj;
                //dialog.Dispose();
            }
        }
        #endregion

        #region Data Block
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

        private void ApplyDataBlockBtn_Click_1(object sender, EventArgs e)
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

                   // MessageBox.Show(ex.Message);
                }


            }
            SurveyData.NavConfigModel = navConfigModel;

           // Console.WriteLine(" data ");
        }  
        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
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

        private void ComboBoxFullDuplexDevice_Click(object sender, EventArgs e)
        {

        }

        Action cbDuplexDeviceEnabled;

        public void RegisterDuplexDeviceEnabled(Action _callback)
        {
            cbDuplexDeviceEnabled += _callback;
        }


        private void FDD1ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (FDD1ChkBox.Checked)
            //{
            //    // input device channel 1 output device channel 2.
            //    DeckLinkInputDevice devIn = ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[0]).value;
            //    DeckLinkOutputDevice devOut = ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[1]).value;
            //    _fullDuplexDevice.Add(new FullDuplexDevice("Channel1",devIn, devOut));
            //}
            //else
            //{
            //    if (_fullDuplexDevice != null)
            //    {
            //        var FDD = (from FullDuplexDevice d in _fullDuplexDevice
            //                   where d.inputDevice == ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[0]).value && d.outputDevice == ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[1]).value
            //                   select d).Single();
            //        if(FDD != null)
            //        {
            //            _fullDuplexDevice.Remove(FDD);
            //            FDD = null;
            //        }
            //    }

            //   // FullDuplexDevice fullDuplexDevice1=FullDuplexDevices
            //}


            if (FDD1ChkBox.Checked)
            {
                _fullDuplexDevice.Add(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[0]).value);
            }
            else
            {
                if (_fullDuplexDevice.Contains(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[0]).value))
                {
                    _fullDuplexDevice.Remove(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[0]).value);
                }
            }
            cbDuplexDeviceEnabled();
        }

        private void FDD2ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (FDD2ChkBox.Checked)
            //{
            //    // input device channel 1 output device channel 2.
            //    DeckLinkInputDevice devIn = ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[2]).value;
            //    DeckLinkOutputDevice devOut = ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[3]).value;
            //    _fullDuplexDevice.Add(new FullDuplexDevice("Channel2", devIn, devOut));
            //}
            //if (!FDD2ChkBox.Checked)
            //{
            //    if (_fullDuplexDevice != null)
            //    {
            //        var FDD = (from FullDuplexDevice d in _fullDuplexDevice
            //                   where d.inputDevice == ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[2]).value && d.outputDevice == ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[3]).value
            //                   select d).Single();
            //        if (FDD != null)
            //        {
            //            _fullDuplexDevice.Remove(FDD);
            //            FDD = null;
            //        }
            //    }
            //    // FullDuplexDevice fullDuplexDevice1=FullDuplexDevices
            //}
            if (FDD2ChkBox.Checked)
            {
                _fullDuplexDevice.Add(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[1]).value);
            }
            else
            {
                if (_fullDuplexDevice.Contains(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[1]).value))
                {
                    _fullDuplexDevice.Remove(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[1]).value);
                }
            }
            cbDuplexDeviceEnabled();
        }

        private void FDD3ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (FDD3ChkBox.Checked)
            //{
            //    // input device channel 1 output device channel 2.
            //    DeckLinkInputDevice devIn = ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[4]).value;
            //    DeckLinkOutputDevice devOut = ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[5]).value;
            //    _fullDuplexDevice.Add(new FullDuplexDevice("Channel3", devIn, devOut));
            //}
            //if (!FDD3ChkBox.Checked)
            //{
            //    if (_fullDuplexDevice != null)
            //    {
            //        var FDD = (from FullDuplexDevice d in _fullDuplexDevice
            //                   where d.inputDevice == ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[4]).value && d.outputDevice == ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[5]).value
            //                   select d).Single();
            //        if (FDD != null)
            //        {
            //            _fullDuplexDevice.Remove(FDD);
            //            FDD = null;
            //        }

            //    }

            //    // FullDuplexDevice fullDuplexDevice1=FullDuplexDevices
            //}
            if (FDD3ChkBox.Checked)
            {
                _fullDuplexDevice.Add(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[2]).value);
            }
            else
            {
                if (_fullDuplexDevice.Contains(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[2]).value))
                {
                    _fullDuplexDevice.Remove(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[2]).value);
                }
            }
            cbDuplexDeviceEnabled();
            
        }

        private void FDD4ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (FDD4ChkBox.Checked)
            //{
            //    // input device channel 1 output device channel 2.
            //    DeckLinkInputDevice devIn = ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[6]).value;
            //    DeckLinkOutputDevice devOut = ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[7]).value;
            //    _fullDuplexDevice.Add(new FullDuplexDevice("Channel4", devIn, devOut));
            //}
            //if (!FDD4ChkBox.Checked)
            //{
            //    if (_fullDuplexDevice != null)
            //    {
            //        var FDD = (from FullDuplexDevice d in _fullDuplexDevice
            //                   where d.inputDevice == ((StringObjectPair<DeckLinkInputDevice>)DecklinkDeviceCB.Items[6]).value && d.outputDevice == ((StringObjectPair<DeckLinkOutputDevice>)DecklinkDeviceCB.Items[7]).value
            //                   select d).Single();
            //        if (FDD != null)
            //        {
            //            _fullDuplexDevice.Remove(FDD);
            //            FDD = null;
            //        }

            //    }

            //    // FullDuplexDevice fullDuplexDevice1=FullDuplexDevices
            //}
            if (FDD4ChkBox.Checked)
            {
                _fullDuplexDevice.Add(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[3]).value);
            }
            else
            {
                if (_fullDuplexDevice.Contains(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[3]).value))
                {
                    _fullDuplexDevice.Remove(((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.Items[3]).value);
                }
            }
            cbDuplexDeviceEnabled();
        }

        private void panelCH1_Click(object sender, EventArgs e)
        {
            //panelCH1.back
        }

        private void DecklinkDeviceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            M_selectedDecklinkDevice = null;
            if (DecklinkDeviceCB.SelectedIndex < 0)
            {
                return;
            }
            //if(DecklinkDeviceCB.SelectedIndex==0|| DecklinkDeviceCB.SelectedIndex % 2 == 0)
            //{
            //    M_selectedDecklinkInput = ((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.SelectedItem).value;
            //    RefreshCaptureVideoModeList();
            //}
            M_selectedDecklinkDevice = ((StringObjectPair<FullDuplexDevice>)DecklinkDeviceCB.SelectedItem).value;
            RefreshCaptureVideoModeList();
            checkBoxEnableFormatDetection.Checked = M_selectedDecklinkDevice.VideoFormatDetection;
        }

        private void comboBoxCapturePixelFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxCapturePixelFormat.SelectedItem!=null)
                M_selectedDecklinkDevice.currentPixelFormat = ((StringObjectPair<_BMDPixelFormat>)comboBoxCapturePixelFormat.SelectedItem).value;
        }

        #region Audio Settings


        void InitializeAudioRateCB()
        {
            AudioBitRateCB.Items.Add(new StringObjectPair<_BMDAudioSampleRate>("48000 Hz ", _BMDAudioSampleRate.bmdAudioSampleRate48kHz));
            AudioBitRateCB.SelectedIndex = 0;
        }
        void InitializeAudioSampleDepth()
        {
            AudioSampleDepthCB.Items.Add(new StringObjectPair<_BMDAudioSampleType>("16 bit ", _BMDAudioSampleType.bmdAudioSampleType16bitInteger));
            AudioSampleDepthCB.Items.Add(new StringObjectPair<_BMDAudioSampleType>("32 bit ", _BMDAudioSampleType.bmdAudioSampleType32bitInteger));
            AudioSampleDepthCB.SelectedIndex = 0;
        }

        private void AudioBitRateCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AudioSampleDepthCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }









        #endregion
























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

      
    }

}
