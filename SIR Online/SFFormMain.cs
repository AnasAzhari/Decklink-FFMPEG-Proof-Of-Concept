using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using SIRModel.Eventing;
using SIRModel.Online;
using d3d = Microsoft.DirectX.Direct3D;
using DeckLinkAPI;
//using Accord.Video.FFMPEG; // must copy all the dll in package/accord ffmpeg/build to bin debug folder.
// and also Accord.Video (not ffmpeg)
// must use Accord ver 3.7.0,Accord.Video ver 3.6.0, Accord.video.FFMPEG ver 3.3.0 
using System.Diagnostics;
using EventLog = SIRModel.Eventing.EventLog;
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

//using Accord;
//using Accord.Video;
//using Accord.Video.FFMPEG;


using Microsoft.DirectX.DirectSound;
using System.Linq;
using System.Threading;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Syncfusion;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using Orientation = System.Windows.Forms.Orientation;
using Action = System.Action;
using Microsoft.Office;
//using MFORMATSLib;


namespace SIR_Online
{
    public partial class SFFormMain : Form
    {
        SIROnlineSession _SIROS;
        SFSystemSettings configForm;
        NewProjectDialog newProjectDialog;
        //StartDivingSession startDivingSession;
        SFConfigureOverlay ovlyConfig;
        ProgressBarAdv progressBarAdv;
        private Bgra32FrameConverter m_frameConverter;
        IDeckLinkVideoConversion frameConverter;

        private List<FullDuplexDevice> DuplexDevicesList
        {
            get
            {
                return configForm.GetFullDuplexDevices;
            }
        }

        private DeckLinkInputDevice m_SelectedCaptureDevice
        {
            get
            {
                return configForm.m_selectedCaptureDevice;
            }
        }
        private DeckLinkOutputDevice OutputDevice
        {
            get { return configForm.m_selectedPlaybackDevice; }
        }

        //private FullDuplexDevice SelectedDevice
        //{
        //    get
        //    {
        //        //return configForm.M_selectedDecklinkInput;
        //    }

        //}

        private List<FullDuplexDevice> DeviceList
        {
            get { return configForm.GetFullDuplexDevices; }
        }

        public SFFormMain()
        {
            InitializeComponent();
            DisableControllers();

            //windowsPreview1.InitD3D();
            // windowsPreviewOutput1.InitD3D();
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomMenuStripColor());
        }
        Color rcordoutBackColor;
        private void SFFormMain_Load(object sender, EventArgs e)
        {
            // just make sure the object is instantiated first._SIROnlineSession Wont be instantiated anywhere.
            _SIROS = SIROnlineSession.Instance;

            configForm = new SFSystemSettings();
            _SIROS.SFSystemSettings = configForm;
            configForm.Hide();
            ovlyConfig = new SFConfigureOverlay();
            ovlyConfig.Hide();
            newProjectDialog = new NewProjectDialog();
            newProjectDialog.Hide();
           // startDivingSession = new StartDivingSession();
           // startDivingSession.Hide();
            m_frameConverter = new Bgra32FrameConverter();
            //InitializeWindowSize();         
            _SIROS.RegisterCurrentProjectSelected(() => { OnCurrentProjectSet(); });
            _PanelSize = ScreenPanel.Size;
            DeviceAndPreview = new Dictionary<FullDuplexDevice, WindowsPreviewOutput>();
            //this.WindowState = FormWindowState.Normal;
            rcordoutBackColor = RcdOutBtn.BackColor;
            lbl_recording.Hide();
            lbl_recording_clip.Hide();
            configForm.RegisterDuplexDeviceEnabled(OnDuplexDeviceEnabled);
            //EnableDisableControls(false);
        }
        void InitializeWindowSize()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void SetControlEnableDisable(bool _set)
        {
            foreach (Control control in this.Controls)
            {
                if (control.Name != "menuStrip1")
                {
                    control.Enabled = _set;
                   
                }
            }
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

        private void dropdown_format_Click(object sender, EventArgs e)
        {

        }

        private void systemSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens System Settings form
            configForm.Show();
        }

        private void configureOverlayAndIntroPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ovlyConfig.Show();
        }


        bool IsrunningPlayback = false;


        #region Run OutputPlayBack

        public void StartRunningOutputPlayback()
        {
            
            int videomodeformatidx = configForm.ComboboxVideoModeFormatCurrIdx;
            DisplayModeEntry displayModeEntry = configForm.CurrentDisplayMode;

            foreach (var item in DuplexDevicesList)
            {

                //SelectedDevice.StartRunningOutputPlayback(videomodeformatidx, displayModeEntry, configForm.videoFormatDetection, (int)configForm.ChannelCount, configForm.AudioSampleType, configForm.AudioSampleRate, null, DeviceAndPreview[item]);
                item.StartRunningOutputPlayback(videomodeformatidx, displayModeEntry, configForm.videoFormatDetection, (int)configForm.ChannelCount, configForm.AudioSampleType, configForm.AudioSampleRate, null, DeviceAndPreview[item]);
                item.RegisterAudioBufferArrived(OnReceivedAudioBuffer);
            }
        }

        void StopRunningOutputPlayback()
        {
            //if (SelectedDevice.outputDevice.IsRecordingOutput = true)
            //{
            //    StopRecordingOutput();
            //}
            //SelectedDevice.inputDevice.StopCapture();
            //SelectedDevice.inputDevice.RemoveAllListeners();
            //SelectedDevice.UnRegisterAudioBufferArrived(OnReceivedAudioBuffer);


            //SelectedDevice.outputDevice.IsRecordingOutput = false;
            //SelectedDevice.outputDevice.DisableAudioOutput();
            //SelectedDevice.outputDevice.DisableVideoOutput();
            //SelectedDevice.outputDevice.StopDisplayVideoFrameSync();
            foreach (var item in DuplexDevicesList)
            {
            
                if (IsRecording)
                {
                    StopRecordingOutput();
                    IsRecording = false;
                }

                item.inputDevice.StopCapture();
                item.inputDevice.RemoveAllListeners();
                item.UnRegisterAudioBufferArrived(OnReceivedAudioBuffer);


                item.outputDevice.IsRecordingOutput = false;
                item.outputDevice.DisableAudioOutput();
                item.outputDevice.DisableVideoOutput();
                item.outputDevice.StopDisplayVideoFrameSync();
            }
        }

        #endregion


        //[DefaultValue(-65.0)]
        //public float MinDb { get; set; }
        public float MinDb = -65;
        /// <summary>
        /// Maximum decibels
        /// </summary>
        //[DefaultValue(0)]
       // public float MaxDb { get; set; }
        public float MaxDb = 0;

        public void OnReceivedAudioBuffer(byte[] _audioBuffer)
        {

            MethodInvoker methodInvoker = delegate ()
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //Int16[] audioLevel = ByteToInt16(_audioBuffer);
                float[] floatvalue = Convert16BitToFloat(_audioBuffer);
                //Console.WriteLine(" int value 1 : {0}, int 2 : {1} , int 3 : {2} , int 4  : {3} ,int 5 : {4}", audioLevel[0], audioLevel[1], audioLevel[2], audioLevel[3], audioLevel[4]);
                // Console.WriteLine(" float value 0 : {0}, float 1 : {1} , float 2 : {2} , float 3  : {3} ,float 4 : {4}", floatvalue[0], floatvalue[1], floatvalue[2], floatvalue[3], floatvalue[4]);
                // Console.WriteLine(" db value 0 : {0}, db 1 : {1} , db 2 : {2} , db 3  : {3} ,db 4 : {4}", (int)(20 * Math.Log10(Math.Abs(floatvalue[0]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[1]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[2]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[3]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[4]))));
                //short[] shortValue = ByteToShort(_audioBuffer);

                //float max = floatvalue.Max();

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                int interval = 5; // 1 ms tick
                //int arrayBlockLength = 50; //
                int arraySegmentCount = 40 / interval;   /// frame time divide by interval . for example 25 fps (has 40 ms period) divided by determined
                                                         /// interval time of 1ms . 
                float[] MaxRValueAtInterval = new float[arraySegmentCount]; // max value at 1ms interval 
                float[] MaxLValueAtInterval = new float[arraySegmentCount];
                int SegmentIteration;
                SegmentIteration = (int)floatvalue.Length / (2 * arraySegmentCount);

                float[] ValueR = new float[(int)floatvalue.Length / 2];
                float[] ValueL = new float[(int)floatvalue.Length / 2];

                int RIndex = 0;
                int LIndex = 0;
                for (int i = 0; i < floatvalue.Length; i++)
                {


                    if (i == 0 || i % 2 == 0)
                    {
                        ValueR[RIndex] = floatvalue[i];
                        RIndex++;
                    }
                    if (i % 2 != 0)
                    {
                        ValueL[LIndex] = floatvalue[i];
                        LIndex++;
                    }

                }


                // Console.WriteLine(" r val :" + ValueR.Length);
                // Console.WriteLine("L val : " + ValueL.Length);

                float maxValR = 0;
                int idxMaxArrayR = 0;
                for (int i = 0; i < ValueR.Length; i++)
                {

                    if (Math.Abs(ValueR[i]) > maxValR)
                    {
                        maxValR = ValueR[i];
                    }
                    if (i % SegmentIteration == 0)
                    {
                        if (idxMaxArrayR > MaxRValueAtInterval.Length - 1)
                        {
                            break;
                        }
                        else
                        {
                            MaxRValueAtInterval[idxMaxArrayR] = maxValR;
                            maxValR = 0;
                            idxMaxArrayR++;
                        }
                    }
                }
                //Console.WriteLine(" max value R : " + MaxRValueAtInterval.Length);

                float maxValL = 0;
                int idxMaxArrayL = 0;
                for (int i = 0; i < ValueL.Length; i++)
                {

                    if (Math.Abs(ValueL[i]) > maxValL)
                    {
                        maxValL = Math.Abs(ValueL[i]);
                    }
                    if (i % SegmentIteration == 0)
                    {
                        if (idxMaxArrayL > MaxLValueAtInterval.Length - 1)
                        {
                            break;

                        }
                        else
                        {
                            MaxLValueAtInterval[idxMaxArrayL] = maxValL;
                            maxValL = 0;
                            idxMaxArrayL++;
                        }

                    }

                }
                //Console.WriteLine(" max value L : " + MaxLValueAtInterval.Length);

                int RLength = MaxRValueAtInterval.Length;
                int LLength = MaxLValueAtInterval.Length;
                timer.Interval = interval;
                timer.Enabled = true;
                int tickeCounter;


                tickeCounter = 0;

                stopwatch.Stop();
                //Console.WriteLine(" time taken : " + stopwatch.ElapsedMilliseconds);

                timer.Start();

                timer.Tick += (s, vd) =>
                {
                    if (tickeCounter > MaxRValueAtInterval.Length - 1)
                    {
                        timer.Stop();
                        timer.Dispose();
                    }
                    else
                    {

                        double r;
                        double L;
                        r = 20 * Math.Log10(Math.Abs(MaxRValueAtInterval[tickeCounter]));
                        L = 20 * Math.Log10(Math.Abs(MaxLValueAtInterval[tickeCounter]));
                       // Console.WriteLine(" r value : {0}, L value : {1} ", r, L);
                        if (r < MinDb || L < MinDb)
                        {
                           r= r < MinDb ?  MinDb : r ;
                           L = L < MinDb ? MinDb : L;
                        }
                            
                        if (r > MaxDb || L > MaxDb)
                        {
                            r = r > MaxDb ? MaxDb : r;
                            L = L > MaxDb ? MaxDb : L;
                        }
                       // Console.WriteLine(" r value After : {0}, L value After : {1} ", r, L);
                        double percentR = (r - MinDb) / (MaxDb - MinDb);
                        double percentL = (L - MinDb) / (MaxDb - MinDb);
                       // Console.WriteLine(" r precent  : {0}, L percent After : {1} ", percentR, percentL);
                        //bar_mic_volumeR.Value = (int)(100 * percentR);
                        bar_mic_volumeL.Value = (int)(100 * percentL);
                        //Console.WriteLine(" progress bar value R :" + bar_mic_volumeR.Value);
                        // volumeMeter1.Amplitude = MaxRValueAtInterval[tickeCounter];
                        //volumeMeter2.Amplitude = MaxLValueAtInterval[tickeCounter];

                        tickeCounter++;

                    }

                };

            };
            //if (bar_mic_volumeR.InvokeRequired || bar_mic_volumeL.InvokeRequired)
              if  (bar_mic_volumeL.InvokeRequired)
                    this.Invoke(methodInvoker);
            else
                methodInvoker();

        }

        public float[] Convert16BitToFloat(byte[] input)
        {
            int inputSamples = input.Length / 2;
            float[] output = new float[inputSamples];
            int outputIndex = 0;
            for (int i = 0; i < inputSamples; i++)
            {
                short sample = BitConverter.ToInt16(input, i * 2);
                output[outputIndex++] = sample / 32768f;
            }
            return output;
        }

        bool _isRecoding;
        bool IsRecording
        {
            get
            {
                return _isRecoding;
            }
            set
            {
                _isRecoding = value;
                if (_isRecoding)
                {
                    lbl_recording.Show();
                    RcdOutBtn.BackColor = Color.Red;
                }

                else
                {
                    lbl_recording.Hide();
                    RcdOutBtn.BackColor = rcordoutBackColor;
                }
                    
            }
        }
        bool _isRecordingShortClip;
        bool IsRecordingShortClip
        {
            get
            {
                return _isRecordingShortClip;
            }
            set
            {
                _isRecordingShortClip = value;
                if (_isRecordingShortClip)
                {
                    lbl_recording_clip.Show();
                    ShortClipbtn.BackColor = Color.Red;
                }
                else
                {
                    lbl_recording_clip.Hide();
                    ShortClipbtn.BackColor = rcordoutBackColor;
                }
            }
        }


        private void RcdOutBtn_Click(object sender, EventArgs e)
        {

            //foreach (var item in DuplexDevicesList)
            //{
            //    if (item.outputDevice != null)
            //    {

            //        if (item.outputDevice.IsRecordingOutput == false)
            //        {
            //            //SelectedDevice.outputDevice.IsRecordingOutput = true;
            //            StartRecordingOutput();
            //            //RcdOutBtn.Text = " Stop Output";
            //        }
            //        else
            //        {
            //            //RcdOutBtn.Text = " Output Recording";
            //            StopRecordingOutput();
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}


            if (IsRecording == false)
            {
                StartRecordingOutput();
                IsRecording = true;
            }
            else
            {
                StopRecordingOutput();
                IsRecording = false;

            }
            //if (SelectedDevice.outputDevice != null)
            //{

            //    if (SelectedDevice.outputDevice.IsRecordingOutput == false)
            //    {
            //        //SelectedDevice.outputDevice.IsRecordingOutput = true;
            //        StartRecordingOutput();
            //        //RcdOutBtn.Text = " Stop Output";
            //    }
            //    else
            //    {
            //        //RcdOutBtn.Text = " Output Recording";
            //        StopRecordingOutput();
            //    }
            //}
            //else
            //{
            //    return;
            //}
        }

     

        private void ShortClipbtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (SelectedDevice.outputDevice != null)
            //    {

            //        if (SelectedDevice.outputDevice.IsRecordingShortClip == false)
            //        {
            //            //SelectedDevice.outputDevice.IsRecordingOutput = true;
            //            StartRecordingShortClip();
            //            //RcdOutBtn.Text = " Stop Output";
            //        }
            //        else
            //        {

            //            StopRecordingShortClip();
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            foreach (var item in DuplexDevicesList)
            {
                try
                {
                    if (item.outputDevice != null)
                    {

                        if (item.outputDevice.IsRecordingShortClip == false)
                        {
                            //SelectedDevice.outputDevice.IsRecordingOutput = true;
                            StartRecordingShortClip();
                            //RcdOutBtn.Text = " Stop Output";
                        }
                        else
                        {

                            StopRecordingShortClip();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
           
        }

        void StopRecordingShortClip()
        {
            foreach (var item in DuplexDevicesList)
            {
                item.StopRecordingShortClip();
            }
            
            IsRecordingShortClip = false;
        }

        void StartRecordingShortClip()
        {
              var dialog = new SaveFileDialog();
                dialog.InitialDirectory = _SIROS.GetCurrentDiveSnapshotFoler(_SIROS.CurrentStructure, _SIROS.CurrentDiveType);
                string filename = dialog.FileName;
                dialog.DefaultExt = ".avi";
                dialog.AddExtension = true;
                DialogResult ret = STAShowDialog(dialog);
            if (ret == DialogResult.OK)
            {
                var displayMode = configForm.CurrentDisplayMode;

                int _width = (int)displayMode.displayMode.GetWidth();
                int _height = (int)displayMode.displayMode.GetHeight();
                long frameDuration;
                long timeScale;
                displayMode.displayMode.GetFrameRate(out frameDuration, out timeScale);
                int frameRate = ((int)timeScale / (int)frameDuration);
                //SelectedDevice.StartRecordingShortClip(dialog.FileName, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
                //SelectedDevice.outputDevice.IsRecordingShortClip = true;
                foreach (var item in DuplexDevicesList)
                {
                    //item.StartRecordingShortClip(dialog.FileName, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
                    item.outputDevice.IsRecordingShortClip = true;
                }
                IsRecordingShortClip = true;

            }
        }

        #region Recording

        void StartRecordingOutput()
        {
            //if (SelectedDevice.IsAlreadyDefineFilename == false)
            //{
            //    var dialog = new SaveFileDialog();
            //    dialog.InitialDirectory = _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType);
            //    string filename = dialog.FileName;
            //    dialog.DefaultExt = ".avi";
            //    dialog.AddExtension = true;
            //    DialogResult ret = STAShowDialog(dialog);
            //    if (ret == DialogResult.OK)
            //    {
            //        var displayMode = configForm.CurrentDisplayMode;

            //        int _width = (int)displayMode.displayMode.GetWidth();
            //        int _height = (int)displayMode.displayMode.GetHeight();
            //        long frameDuration;
            //        long timeScale;
            //        displayMode.displayMode.GetFrameRate(out frameDuration, out timeScale);
            //        int frameRate = ((int)timeScale / (int)frameDuration);
            //        //SelectedDevice.
            //        //string backupstring = (from str in dialog.FileName.Split(new string[] { _SIROS.GetCurrentDiveVideoFolder() }, StringSplitOptions.RemoveEmptyEntries)
            //        //                       where !str.Contains(_SIROS.GetCurrentDiveVideoFolder()) 
            //        //                       select str).First();
            //        var bckup = dialog.FileName.Remove(0, _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType).Length + 1);
            //        // will be replace by FullDuplexDevice class

            //        SelectedDevice.StartDeviceRecording(dialog.FileName, bckup, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
            //        SelectedDevice.outputDevice.IsRecordingOutput = true;
            //        IsRecording = true;
            //        #region event start recording log
            //        //EventModelv1 ev = new EventModelv1();
            //        //ev.EventName = "Recording Output Started";
            //        //ev.isAnomaly = false;
            //        //EventLog vidstartedlog = new EventLog();
            //        //vidstartedlog.evented = ev;
            //        //vidstartedlog.loggedTime = DateTime.Now;
            //        //SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
            //        //logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
            //        //logEventDGV.Refresh();
            //        #endregion
            //    };
            //}
            //else
            //{
            //    SelectedDevice.outputDevice.IsRecordingOutput = true;
            //    IsRecording = true;
            //    SelectedDevice.ContinueRecording();

            //}


           
                var dialog = new SaveFileDialog();
                dialog.InitialDirectory = _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType);
                string filename = dialog.FileName;
                dialog.DefaultExt = ".avi";
                dialog.AddExtension = true;
                DialogResult ret = STAShowDialog(dialog);
                if (ret == DialogResult.OK)
                {
                    var displayMode = configForm.CurrentDisplayMode;
            
                    var bckup = dialog.FileName.Remove(0, _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType).Length + 1);
                    

                    foreach (var item in DuplexDevicesList)
                    {
                        if (item.IsAlreadyDefineFilename == false)
                        {
                             IDeckLinkDisplayMode deckLinkDisplayMode = (from IDeckLinkDisplayMode _displayMode in item.inputDevice
                                                                    where _displayMode.GetDisplayMode() == item.currentDisplay
                                                                    select _displayMode).Single();
                             int _width= deckLinkDisplayMode.GetWidth(); ;
                             int _height= deckLinkDisplayMode.GetHeight(); ;
                             long frameDuration;
                             long timeScale;
                             displayMode.displayMode.GetFrameRate(out frameDuration, out timeScale);
                             int frameRate = ((int)timeScale / (int)frameDuration);
                        //item.StartDeviceRecording(dialog.FileName, bckup, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
                        //item.StartDeviceRecording(dialog.FileName, bckup, _width, _height, frameRate, configForm.SelectedVideoCOdec, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
                        item.StartDeviceRecording(configForm.FFMPEGexe, _width, _height, dialog.FileName, frameRate, 5000);
                        item.outputDevice.IsRecordingOutput = true;
                            IsRecording = true;

                        getRecordTimer();
                        }
                        else
                        {
                            item.outputDevice.IsRecordingOutput = true;
                            IsRecording = true;
                            item.ContinueRecording();
                        }
                    }
                   
                    #region event start recording log
                    //EventModelv1 ev = new EventModelv1();
                    //ev.EventName = "Recording Output Started";
                    //ev.isAnomaly = false;
                    //EventLog vidstartedlog = new EventLog();
                    //vidstartedlog.evented = ev;
                    //vidstartedlog.loggedTime = DateTime.Now;
                    //SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
                    //logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
                    //logEventDGV.Refresh();
                    #endregion
                };           
     
        }
        void StopRecordingOutput()
        {
            //SelectedDevice.StopRecordingOutput();
            IsRecording = false;
            foreach (var item in DuplexDevicesList)
            {
                item.StopRecordingOutput();
                item.outputDevice.IsRecordingOutput = false;
            }

            

            //SelectedDevice.outputDevice.IsRecordingOutput = false;
            //SelectedDevice.OutputVideoWriter.Close();
            //// _OutputVideowriter.Dispose();
            //SelectedDevice.BackupVideoFileWriter.Close();
            //_backupVideoFile.Dispose();
        }

        #endregion

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProjectDialog.Show();
        }

       

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SIROS.SaveProject();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        public void OpenProject()
        {
            var dialog = new OpenFileDialog();

            dialog.AddExtension = true;

            DialogResult ret = STAShowDialog(dialog);
            if (ret == DialogResult.OK)
            {
                string filename = dialog.FileName;
                IFormatter formatter = new BinaryFormatter();
                Console.WriteLine("filename :" + filename);
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                OnlineProject currproj = (OnlineProject)formatter.Deserialize(stream);
                _SIROS.CurrentProject = currproj;
                dialog.Dispose();

            }
        }

        private void InitializeDiveTypeCB()
        {
            DiveTypeCB.Items.Clear();
            DiveTypeCB.Update();
            foreach (DivingType item in ((StringObjectPair<Structure>)ProjectStructureCB.SelectedItem).value.dives)
            {
                switch (item)
                {
                    case DivingType.Air_Diving:
                        DiveTypeCB.Items.Add(new StringObjectPair<DivingType>(" Air Diving", item));
                        break;
                    case DivingType.Sat_Diving:
                        DiveTypeCB.Items.Add(new StringObjectPair<DivingType>(" Saturation Diving", item));
                        break;
                    case DivingType.ROV:
                        DiveTypeCB.Items.Add(new StringObjectPair<DivingType>(" ROV Diving ", item));
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnCurrentProjectSet()
        {
            this.Text = _SIROS.CurrentProject.ProjectName;
            InitializeProjectStructureCB();
        }

        private void InitializeProjectStructureCB()
        {
            ProjectStructureCB.Items.Clear();
            ProjectStructureCB.Update();
            foreach (Structure item in _SIROS.CurrentProject.structures)
            {
                ProjectStructureCB.Items.Add(new StringObjectPair<Structure>(item.StructureName, item));

            }
            if (ProjectStructureCB.Items.Count > 0)
            {
                ProjectStructureCB.SelectedIndex = 0;
            }
        }

        private void ProjectStructureCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDiveTypeCB();
        }
        bool diveStarted = false;
        private void btn_start_dive_Click(object sender, EventArgs e)
        {
            try
            {
                if (_SIROS.CurrentProject == null)
                {
                    MessageBox.Show(" Project is not selected");
                }
                else if (ProjectStructureCB.SelectedItem== null)
                {
                    MessageBox.Show(" Structure is not selected");
                }
                else if (DiveTypeCB.SelectedItem == null)
                {
                    MessageBox.Show(" Dive Type is not selected");
                }
                else if (DuplexDevicesList == null)
                {
                    MessageBox.Show(" Device not configured");
                }
                else if (DeviceAndPreview.Count == 0)
                {
                    configForm.Show();
                }
                else
                {
                    if (diveStarted == false)
                    {
                        _SIROS.CurrentStructure = ((StringObjectPair<Structure>)ProjectStructureCB.SelectedItem).value;
                        _SIROS.CurrentDiveType = ((StringObjectPair<DivingType>)DiveTypeCB.SelectedItem).value;
                        AddDataStringColumnToDataGridView();
                        EnableDisableControls(true);
                        InitalizeChannelCB();
                        StartRunningOutputPlayback();
                        diveStarted = true;
                        btn_start_dive.Text = "Stop Diving";

                        // Enable controller buttons
                        EnableControllers();
                    }
                    else
                    {
                        StopRunningOutputPlayback();
                        btn_start_dive.Text = "Start Diving";
                        diveStarted = false;

                        // Disable controller buttons
                        DisableControllers();
                    }
                   

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }        
        }

        void EnableDisableControls(bool _trigger)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control item in this.Controls)
            {
                //if(item ==menuStrip1 && item ==DiveTypeCB && item == ProjectStructureCB)
               // {
                    controlList.Add(item);
               // }
            }
            foreach (var item in controlList)
            {
                item.Enabled = _trigger;
            }

        }

        void InitalizeChannelCB()
        {
            channelCB.Items.Clear();
            int count = 1;
            foreach (var item in DuplexDevicesList)
            {
                channelCB.Items.Add(new StringObjectPair<FullDuplexDevice>(" Channel "+ count, item));
                count++;
            }
        }
        private void btn_multiview_Click(object sender, EventArgs e)
        {

        }

       
        bool isChannelFullscreen;

        private void channelCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            isChannelFullscreen = true;

            FullDuplexDevice fdd = ((StringObjectPair<FullDuplexDevice>)channelCB.SelectedItem).value;
            ScreenTableLayout.Controls.Remove(DeviceAndPreview[fdd]);
            ScreenTableLayout.Hide();
            DeviceAndPreview[fdd].Parent = ScreenPanel;
            DeviceAndPreview[fdd].Dock = DockStyle.Fill;
        }

        #region MarkingTool
        MarkingTools CurrentMarkingTool;
        void InitializeMarkingTools()
        {
            CurrentMarkingTool = MarkingTools.None;

        }

        private void btn_marking_circle_Click(object sender, EventArgs e)
        {
            MarkingToolOperation.markingTools = MarkingTools.Circle;

        }

        private void btn_marking_arrow_Click(object sender, EventArgs e)
        {
            MarkingToolOperation.markingTools = MarkingTools.Arrow;
        }
        #endregion

        #region FullScreen
        Size _PanelSize;
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this.TopMost = true;
            //this.menuStrip1.Hide();
            //Console.WriteLine(" window preview size before :" + windowsPreviewOutput1.Width);
            this.WindowState = FormWindowState.Normal;
            
            this.FormBorderStyle = FormBorderStyle.None;
            
            this.WindowState = FormWindowState.Maximized;
            Console.WriteLine(" screenpanel parent : " + ScreenPanel.Parent.Name); 
            this.ScreenPanel.Parent = this;
            this.ScreenPanel.Dock = DockStyle.Fill;
            this.ScreenPanel.Size = this.ClientSize;
            ScreenTableLayout.Dock = DockStyle.Fill;
            this.ScreenPanel.BringToFront();
            //Console.WriteLine(" window preview size after :" + windowsPreviewOutput1.Width);

        }

        private void windowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.menuStrip1.Show();
            this.WindowState=FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            this.ScreenPanel.Parent = tableLayoutPanel1;
            this.ScreenPanel.Dock = DockStyle.Fill;

        }
        #endregion

        private void SnapShotBtn_Click(object sender, EventArgs e)
        {
            foreach (var item in DuplexDevicesList)
            {
                item.IsCaptureStill = true;
            }
        }
        #region UnUsed Event Handler
        private void ProjectStructureCB_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void windowsPreviewOutput1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        Dictionary<FullDuplexDevice, WindowsPreviewOutput> DeviceAndPreview;

    
        public void OnDuplexDeviceEnabled()
        {
            DeviceAndPreview.Clear();
            foreach (Control item in ScreenTableLayout.Controls)
            {
                if (item.GetType() == typeof(WindowsPreviewOutput))
                {
                    item.Dispose();
                }
            }
            ScreenTableLayout.Controls.Clear();
            //ScreenTableLayout.RowCount = 0;
            //ScreenTableLayout.ColumnCount = 0;



            foreach (var item in DuplexDevicesList)
            {
                WindowsPreviewOutput windowsPreviewOutput = new WindowsPreviewOutput();
                windowsPreviewOutput.Enabled = false;
                DeviceAndPreview.Add(item, windowsPreviewOutput);

            }


            int deviceCOunt = DuplexDevicesList.Count;

            switch (deviceCOunt)
            {
                case 1:
                    //ScreenTableLayout.RowCount = 1;
                    //ScreenTableLayout.ColumnCount = 1;

                    foreach (var item in DuplexDevicesList)
                    {
                        var preview = DeviceAndPreview[item];
                        preview.Enabled = true;
                        preview.BackColor = Color.LightYellow;
                        ScreenTableLayout.Controls.Add(preview);                                              
                        preview.Dock = DockStyle.Fill;
                        preview.InitD3D();


                    }

                    break;

                case 2:
                    //ScreenTableLayout.RowCount = 1;
                    //ScreenTableLayout.ColumnCount = 2;

                    foreach (var item in DuplexDevicesList)
                    {
                        var preview = DeviceAndPreview[item];
                        preview.Enabled = true;
                        preview.BackColor = Color.LightYellow;
                        ScreenTableLayout.Controls.Add(preview);
                        preview.Dock = DockStyle.Fill;
                        preview.InitD3D();
                    }

                    break;

                case 3:
                    foreach (var item in DuplexDevicesList)
                    {
                        var preview = DeviceAndPreview[item];
                        preview.Enabled = true;
                        preview.BackColor = Color.LightYellow;
                        ScreenTableLayout.Controls.Add(preview);
                        preview.Dock = DockStyle.Fill;
                        preview.InitD3D();
                        

                    }
                    break;
                case 4:
                    foreach (var item in DuplexDevicesList)
                    {
                        var preview = DeviceAndPreview[item];
                        preview.Enabled = true;
                        preview.BackColor = Color.LightYellow;
                        ScreenTableLayout.Controls.Add(preview);

                        preview.Dock = DockStyle.Fill;
                        preview.InitD3D();


                    }
                    break;

                default:
                    //ScreenTableLayout.RowCount = 1;
                    //ScreenTableLayout.ColumnCount = 1;
                    break;
            }


        }

       void AddDataStringColumnToDataGridView()
        {
            if (SurveyData.NavConfigModel != null)
            {
                if (SurveyData.NavConfigModel.CommaFieldIndexes != null)
                {
                    foreach (var item in SurveyData.NavConfigModel.CommaFieldIndexes.Keys)
                    {
                        DataGridViewTextBoxColumn dataBlockColumn = new DataGridViewTextBoxColumn();
                        dataBlockColumn.Name = item;
                        dataBlockColumn.ReadOnly = true;

                        dataGridView1.Columns.Add(dataBlockColumn);

                    }
                }
            }
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {
            //diver off Deck
            //EventModelv1 ev = new EventModelv1();
            //ev.EventName = "Recording Output Started";ev.EventName = "Recording Output Started";
            //        ev.isAnomaly = false;
            //        EventLog vidstartedlog = new EventLog();
            //vidstartedlog.evented = ev;
            //        vidstartedlog.loggedTime = DateTime.Now;
            //        SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
            //        logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
            //        logEventDGV.Refresh();
            OnlineEventType _type;
            _type = OnlineEventType.Diver_off_Deck;
            EventLog eventLog = OnlineEventing.CreateNewOnlineEventLog(_type);
            dataGridView1.Rows.Add(eventLog.loggedTime, eventLog.evented.EventName, eventLog.Comment, eventLog.DataString[dataGridView1.Columns[3].Name], eventLog.DataString[dataGridView1.Columns[4].Name] );
            _SIROS.EventLogs.Add(eventLog);

        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            OnlineEventType _type;
            _type = OnlineEventType.Diver_left_Surface;
            EventLog eventLog = OnlineEventing.CreateNewOnlineEventLog(_type);
            dataGridView1.Rows.Add(eventLog.loggedTime, eventLog.evented.EventName, eventLog.Comment, eventLog.DataString[dataGridView1.Columns[3].Name], eventLog.DataString[dataGridView1.Columns[4].Name]);
            _SIROS.EventLogs.Add(eventLog);
        }

        private void buttonAdv3_Click(object sender, EventArgs e)
        {
            OnlineEventType _type;
            _type = OnlineEventType.Diver_at_Bottom;
            EventLog eventLog = OnlineEventing.CreateNewOnlineEventLog(_type);
            dataGridView1.Rows.Add(eventLog.loggedTime, eventLog.evented.EventName, eventLog.Comment, eventLog.DataString[dataGridView1.Columns[3].Name], eventLog.DataString[dataGridView1.Columns[4].Name]);
            _SIROS.EventLogs.Add(eventLog);
        }

        private void buttonAdv4_Click(object sender, EventArgs e)
        {

            OnlineEventType _type;
            _type = OnlineEventType.Diver_at_Work_Site;
            EventLog eventLog = OnlineEventing.CreateNewOnlineEventLog(_type);
            dataGridView1.Rows.Add(eventLog.loggedTime, eventLog.evented.EventName, eventLog.Comment, eventLog.DataString[dataGridView1.Columns[3].Name], eventLog.DataString[dataGridView1.Columns[4].Name]);
            _SIROS.EventLogs.Add(eventLog);
        }

        private void exportLogsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Data Logs "+_SIROS.CurrentProject.ProjectName;
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  


            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = _SIROS.GetCurrentDiveLogsFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType);
            string filename = dialog.FileName;
            dialog.DefaultExt = ".xls";
            dialog.AddExtension = true;
            DialogResult ret = STAShowDialog(dialog);
            if (ret == DialogResult.OK)
            {
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application  
                // app.Quit();
            }
        }

        private void ExportLogToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Data Logs " + _SIROS.CurrentProject.ProjectName;
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  


            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = _SIROS.GetCurrentDiveLogsFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType);
            string filename = dialog.FileName;
            dialog.DefaultExt = ".xls";
            dialog.AddExtension = true;
            DialogResult ret = STAShowDialog(dialog);
            if (ret == DialogResult.OK)
            {
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application  
                // app.Quit();
            }
        }

        private void DisableControllers()
        {
            buttonAdv5.Enabled = false;
            RcdOutBtn.Enabled = false;
            SnapShotBtn.Enabled = false;
            ShortClipbtn.Enabled = false;
            btn_stop.Enabled = false;
            btn_overlay.Enabled = false;
            btn_marking_circle.Enabled = false;
            btn_marking_arrow.Enabled = false;
            btn_microphone.Enabled = false;
        }

        private void EnableControllers()
        {
            buttonAdv5.Enabled = true;
            RcdOutBtn.Enabled = true;
            SnapShotBtn.Enabled = true;
            ShortClipbtn.Enabled = true;
            btn_stop.Enabled = true;
            btn_overlay.Enabled = true;
            btn_marking_circle.Enabled = true;
            btn_marking_arrow.Enabled = true;
            btn_microphone.Enabled = true;
        }

        private void Txt_duration_TextChanged(object sender, EventArgs e)
        {

        }

        private void getRecordTimer()
        {
            txt_duration.Text = "Timer goes here"; 
        }
    }



    #region Helper class and Structs



    public class DialogState
    {
        public DialogResult result;
        public FileDialog dialog;

        public void ThreadProcShowDialog()
        {
            result = dialog.ShowDialog();
        }
    }
    public struct StringObjectPair<T>
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
    public struct DisplayModeEntry
    {
        public IDeckLinkDisplayMode displayMode;

        public DisplayModeEntry(IDeckLinkDisplayMode displayMode)
        {
            this.displayMode = displayMode;
        }

        public override string ToString()
        {
            string str;

            displayMode.GetName(out str);

            return str;
        }
    }
    public enum MarkingTools
    {
        None = 0,
        Circle,
        Arrow,
        Rectangle,
        Line
    }

    public static class MarkingToolOperation
    {
        public static MarkingTools markingTools;
        public static bool isDrawing;
    }

    public class AudioReceivedEventArgs : EventArgs
    {
        public double Percentage;

        public AudioReceivedEventArgs(double _pecentage)
        {
            Percentage = _pecentage;
        }
    }
    #endregion


}
