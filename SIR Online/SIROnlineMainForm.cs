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
using Accord;
using Accord.Video;
using Accord.Video.FFMPEG;
using Accord.Audio.Formats;
using Accord.Audio;

using Microsoft.DirectX.DirectSound;
using System.Linq;
using System.Threading;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NAudio;
using NAudio.Wave;
using NAudio.Utils;

namespace SIR_Online
{

    public partial class SIROnlineMainForm : Form
    {
        //private DeckLinkDeviceDiscovery m_deckLinkDiscovery;
        // private DeckLinkDevice m_selectedDevice;
        //private DeckLinkInputDevice m_SelectedCaptureDevice;
        // private DeckLinkOutputDevice m_SelectedPlaybackDevice;
        SIROnlineSession _SIROS;
        ConfigureForm configForm;
        NewProjectDialog newProjectDialog;
        StartDivingSession startDivingSession;
        Overlay_Configuration ovlyConfig;
        Stopwatch sw = new Stopwatch();

        private Bgra32FrameConverter m_frameConverter;
        IDeckLinkVideoConversion frameConverter;

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

        private FullDuplexDevice SelectedDevice
        {
            get
            {
                return configForm.m_SelectedDuplexDevice;
            }

        }


        public SIROnlineMainForm()
        {
            InitializeComponent();
            _PanelSize = ScreenPanel.Size;

            //previewWindow1.InitD3D();
            windowsPreview1.InitD3D();
            windowsPreviewOutput2.InitD3D();
        }


        private void SIROnlineMainForm_Load(object sender, EventArgs e)
        {
            // just make sure the object is instantiated first._SIROnlineSession Wont be Use anywhere.
            _SIROS = SIROnlineSession.Instance;

            configForm = new ConfigureForm();
            configForm.Hide();
            ovlyConfig = new Overlay_Configuration();
            ovlyConfig.Hide();
            newProjectDialog = new NewProjectDialog();
            newProjectDialog.Hide();
            startDivingSession = new StartDivingSession();
            startDivingSession.Hide();

            m_frameConverter = new Bgra32FrameConverter();
            //_SIROnlineSession.m_deckLinkDiscovery.DeviceArrived+=new EventHandler<DeckLinkDiscoveryEventArgs>((s, e) => this.Invoke((Action)(() => AddDevice(s, e))));

            //previewWindow.InitD3D();
            // windowsPreviewOutput2.InitD3D();




            SetControlEnableDisable(false);
            _SIROS.RegisterDiveStarted(OnSelectedCurrentProject);
            volumeMeter1.Orientation = Orientation.Horizontal;
            volumeMeter2.Orientation = Orientation.Horizontal;
            InitializeMarkingToolCB();
        }


        public void StartRunningOutputPlayback()
        {
            sw.Start();
            int videomodeformatidx = configForm.ComboboxVideoModeFormatCurrIdx;
            DisplayModeEntry displayModeEntry = configForm.CurrentDisplayMode;

            SelectedDevice.StartRunningOutputPlayback(videomodeformatidx, displayModeEntry, configForm.videoFormatDetection, (int)configForm.ChannelCount, configForm.AudioSampleType, configForm.AudioSampleRate, windowsPreview1, windowsPreviewOutput2);
            SelectedDevice.RegisterAudioBufferArrived(OnReceivedAudioBuffer);
        }


        void StartRecordingOutput()
        {

            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure);
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

                //string backupstring = (from str in dialog.FileName.Split(new string[] { _SIROS.GetCurrentDiveVideoFolder() }, StringSplitOptions.RemoveEmptyEntries)
                //                       where !str.Contains(_SIROS.GetCurrentDiveVideoFolder()) 
                //                       select str).First();
                var bckup = dialog.FileName.Remove(0, _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure).Length + 1);
                // will be replace by FullDuplexDevice class
                SelectedDevice.StartDeviceRecording(dialog.FileName, bckup, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 1580000, (int)configForm.AudioSampleRate, 2);
                SelectedDevice.outputDevice.IsRecordingOutput = true;

                #region event start recording log
                EventModelv1 ev = new EventModelv1();
                ev.EventName = "Recording Output Started";
                ev.isAnomaly = false;
                EventLog vidstartedlog = new EventLog();
                vidstartedlog.evented = ev;
                vidstartedlog.loggedTime = DateTime.Now;
                SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
                logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
                logEventDGV.Refresh();
                #endregion
            };
        }

      
       
        //private void RecordingBtn_Click(object sender, EventArgs e)
        //{
        //    if (m_SelectedCaptureDevice != null)
        //    {
        //        if (m_SelectedCaptureDevice.isCapturing)
        //        {
        //            if (m_SelectedCaptureDevice.isRecording == false)
        //            {

        //                StartRecording();
        //                RecordingBtn.Text = " Stop Recording";
        //            }
        //            else
        //            {
        //                RecordingBtn.Text = " Record Video";
        //                StopRecording();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        public void InitializeBackgroundWorker()
        {
            //backgroundWorker1.
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerSupportsCancellation = true;

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        //void StartRecording()
        //{
        //    var dialog = new SaveFileDialog();
        //    dialog.InitialDirectory = _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure);
        //    string filename = dialog.FileName;
        //    dialog.DefaultExt = ".avi";

        //    dialog.AddExtension = true;
        //    DialogResult ret = STAShowDialog(dialog);
        //    if (ret == DialogResult.OK)
        //    {

        //        _firstTimeFrame = null;

        //        m_SelectedCaptureDevice.isRecording = true;
        //        //m_SelectedCaptureDevice.VideoFrameArrivedHandler += new EventHandler<DeckLinkVideoFrameArrivedEventArgs>((s, e) => this.Invoke((Action)(() => { VideoFrameArrived(s, e); })));

        //        //var displayMode = ((DisplayModeEntry)comboBoxVideoFormat.SelectedItem).displayMode;
        //        var displayMode = configForm.CurrentDisplayMode;
        //        int _width = (int)displayMode.displayMode.GetWidth();
        //        int _height = (int)displayMode.displayMode.GetHeight();
        //        _videoFileWriter.Open(dialog.FileName, _width, _height);
               
        //        EventModelv1 ev = new EventModelv1();
        //        ev.EventName = "Recording Started";
        //        ev.isAnomaly = false;
        //        EventLog vidstartedlog = new EventLog();
        //        vidstartedlog.evented = ev;
        //        vidstartedlog.loggedTime = DateTime.Now;
        //        SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
        //        logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
        //        logEventDGV.Refresh();

        //    };

        //}
        public void SaveProject()
        {
            OnlineProject currProj = _SIROS.CurrentProject;
            //FileStream file =new FileStream() 
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(_SIROS.GetProjectFilenameFullPath(), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, currProj);
            stream.Close();

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
        //void StopRecording()
        //{

        //    m_SelectedCaptureDevice.isRecording = false;
        //    _videoFileWriter.Close();
        //    // _videoFileWriter.Dispose();
        //}
        void StopRecordingOutput()
        {
            SelectedDevice.StopRecordingOutput();
            //SelectedDevice.outputDevice.IsRecordingOutput = false;
            //SelectedDevice.OutputVideoWriter.Close();
            //// _OutputVideowriter.Dispose();
            //SelectedDevice.BackupVideoFileWriter.Close();
            //_backupVideoFile.Dispose();
        }
   




        private void syncEvent_Click(object sender, EventArgs e)
        {
            _SIROS.SyncWithEventing();
            buildEventLogDGV();
        }

        public void buildEventLogDGV()
        {
            logEventDGV.Rows.Clear();

            foreach (EventLog evl in _SIROS.EventLogs)
            {
                logEventDGV.Rows.Add(evl.loggedTime.ToString(), evl.evented.EventName, evl.evented.isAnomaly, evl.evented.Description);
            }
        }

        private void AudioPacketArrived(object sender, DecklinkAudioPacketArrivedEventArgs e)
        {


        }

        private void StopCapture()
        {
            if (m_SelectedCaptureDevice != null)
            {
                m_SelectedCaptureDevice.StopCapture();
                EventModelv1 ev = new EventModelv1();
                ev.EventName = "Video Stopped";
                ev.isAnomaly = false;
                EventLog vidstartedlog = new EventLog();
                vidstartedlog.evented = ev;
                vidstartedlog.loggedTime = DateTime.Now;
                _SIROS.EventLogs.Add(vidstartedlog);
                logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
                logEventDGV.Refresh();

                // Update UI
                //buttonStartStop.Text = "Start Capture";
                //EnableInterface(true);
                labelInvalidInput.Visible = false;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (m_SelectedCaptureDevice != null)
            {
                m_SelectedCaptureDevice.isCaptureStill = true;
            }

        }

        private void labelInvalidInput_TextChanged(object sender, EventArgs e)
        {

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


        /// <summary>
        /// Used for putting other object types into combo boxes.
        /// </summary>


        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configForm.Visible = true;
        }

        private void SIROnlineMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        int drawframe = 0;
        private void SIROnlineMainForm_Paint(object sender, PaintEventArgs e)
        {


        }
      

        public void OnSelectedCurrentProject(Structure structure)
        {
            SetControlEnableDisable(true);
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


        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(" does device support internal keying :" + configForm.m_SelectedDuplexDevice.SupportInternalKeying);

            Console.WriteLine(" audio Channel Max number :" + configForm.m_SelectedDuplexDevice.SupportMaximumAudioChannelNumber);
            Console.WriteLine(" Number of sub devices : " + configForm.m_SelectedDuplexDevice.SupportDuplexDeviceNumber);

        }

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
                int interval = 1; // 1 ms tick
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

                        int r;
                        int L;
                        r = (int)(20 * Math.Log10(Math.Abs(floatvalue[tickeCounter])));
                        L = (int)(20 * Math.Log10(Math.Abs(floatvalue[tickeCounter + 1])));

                        // volumeMeter1.Amplitude = floatvalue[tickeCounter];
                        //volumeMeter1.Amplitude=from float r in new ArraySegment<float>()

                        //volumeMeter2.Amplitude = floatvalue[tickeCounter + 1];
                        volumeMeter1.Amplitude = MaxRValueAtInterval[tickeCounter];
                        volumeMeter2.Amplitude = MaxLValueAtInterval[tickeCounter];
                        //Console.WriteLine(" ticker counter :" + tickeCounter);
                        //Console.WriteLine(" volemeter 1 amplitude : " + volumeMeter1.Amplitude);
                        //Console.WriteLine(" volemeter 2 amplitude : " + volumeMeter2.Amplitude);
                        //tickeCounter += arrayBlockLength;
                        tickeCounter++;

                    }

                };

            };
            if (volumeMeter1.InvokeRequired)
                this.Invoke(methodInvoker);
            else
                methodInvoker();

        }
        //public void OnReceivedAudioBuffer(byte[] _audioBuffer)
        //{
        //        Task task = new Task(()=>{
        //            Stopwatch stopwatch = new Stopwatch();
        //            stopwatch.Start();
        //            //Int16[] audioLevel = ByteToInt16(_audioBuffer);
        //            float[] floatvalue = Convert16BitToFloat(_audioBuffer);
        //            //Console.WriteLine(" int value 1 : {0}, int 2 : {1} , int 3 : {2} , int 4  : {3} ,int 5 : {4}", audioLevel[0], audioLevel[1], audioLevel[2], audioLevel[3], audioLevel[4]);
        //            // Console.WriteLine(" float value 0 : {0}, float 1 : {1} , float 2 : {2} , float 3  : {3} ,float 4 : {4}", floatvalue[0], floatvalue[1], floatvalue[2], floatvalue[3], floatvalue[4]);
        //            // Console.WriteLine(" db value 0 : {0}, db 1 : {1} , db 2 : {2} , db 3  : {3} ,db 4 : {4}", (int)(20 * Math.Log10(Math.Abs(floatvalue[0]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[1]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[2]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[3]))), (int)(20 * Math.Log10(Math.Abs(floatvalue[4]))));
        //            //short[] shortValue = ByteToShort(_audioBuffer);

        //            //float max = floatvalue.Max();

        //            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        //            int interval = 1; // 1 ms tick
        //                              //int arrayBlockLength = 50; //
        //            int arraySegmentCount = 40 / interval;   /// frame time divide by interval . for example 25 fps (has 40 ms period) divided by determined
        //                                                     /// interval time of 1ms . 
        //            float[] MaxRValueAtInterval = new float[arraySegmentCount]; // max value at 1ms interval 
        //            float[] MaxLValueAtInterval = new float[arraySegmentCount];
        //            int SegmentIteration;
        //            SegmentIteration = (int)floatvalue.Length / (2 * arraySegmentCount);

        //            float[] ValueR = new float[(int)floatvalue.Length / 2];
        //            float[] ValueL = new float[(int)floatvalue.Length / 2];

        //            int RIndex = 0;
        //            int LIndex = 0;
        //            for (int i = 0; i < floatvalue.Length; i++)
        //            {


        //                if (i == 0 || i % 2 == 0)
        //                {
        //                    ValueR[RIndex] = floatvalue[i];
        //                    RIndex++;
        //                }
        //                if (i % 2 != 0)
        //                {
        //                    ValueL[LIndex] = floatvalue[i];
        //                    LIndex++;
        //                }

        //            }


        //            // Console.WriteLine(" r val :" + ValueR.Length);
        //            // Console.WriteLine("L val : " + ValueL.Length);

        //            float maxValR = 0;
        //            int idxMaxArrayR = 0;
        //            for (int i = 0; i < ValueR.Length; i++)
        //            {

        //                if (Math.Abs(ValueR[i]) > maxValR)
        //                {
        //                    maxValR = ValueR[i];
        //                }
        //                if (i % SegmentIteration == 0)
        //                {
        //                    if (idxMaxArrayR > MaxRValueAtInterval.Length - 1)
        //                    {
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        MaxRValueAtInterval[idxMaxArrayR] = maxValR;
        //                        maxValR = 0;
        //                        idxMaxArrayR++;
        //                    }
        //                }
        //            }
        //            //Console.WriteLine(" max value R : " + MaxRValueAtInterval.Length);

        //            float maxValL = 0;
        //            int idxMaxArrayL = 0;
        //            for (int i = 0; i < ValueL.Length; i++)
        //            {

        //                if (Math.Abs(ValueL[i]) > maxValL)
        //                {
        //                    maxValL = Math.Abs(ValueL[i]);
        //                }
        //                if (i % SegmentIteration == 0)
        //                {
        //                    if (idxMaxArrayL > MaxLValueAtInterval.Length - 1)
        //                    {
        //                        break;

        //                    }
        //                    else
        //                    {
        //                        MaxLValueAtInterval[idxMaxArrayL] = maxValL;
        //                        maxValL = 0;
        //                        idxMaxArrayL++;
        //                    }

        //                }

        //            }
        //            //Console.WriteLine(" max value L : " + MaxLValueAtInterval.Length);

        //            int RLength = MaxRValueAtInterval.Length;
        //            int LLength = MaxLValueAtInterval.Length;
        //            timer.Interval = interval;
        //            timer.Enabled = true;
        //            int tickeCounter;


        //            tickeCounter = 0;

        //            stopwatch.Stop();
        //            Console.WriteLine(" time taken : " + stopwatch.ElapsedMilliseconds);

        //            timer.Start();

        //            timer.Tick += (s, vd) =>
        //            {
        //                if (tickeCounter > MaxRValueAtInterval.Length - 1)
        //                {
        //                    timer.Stop();
        //                    timer.Dispose();
        //                }
        //                else
        //                {

        //                    int r;
        //                    int L;
        //                    r = (int)(20 * Math.Log10(Math.Abs(floatvalue[tickeCounter])));
        //                    L = (int)(20 * Math.Log10(Math.Abs(floatvalue[tickeCounter + 1])));

        //                    // volumeMeter1.Amplitude = floatvalue[tickeCounter];
        //                    //volumeMeter1.Amplitude=from float r in new ArraySegment<float>()

        //                    //volumeMeter2.Amplitude = floatvalue[tickeCounter + 1];
        //                    volumeMeter1.Amplitude = MaxRValueAtInterval[tickeCounter];
        //                    volumeMeter2.Amplitude = MaxLValueAtInterval[tickeCounter];
        //                    //Console.WriteLine(" ticker counter :" + tickeCounter);
        //                    //Console.WriteLine(" volemeter 1 amplitude : " + volumeMeter1.Amplitude);
        //                    //Console.WriteLine(" volemeter 2 amplitude : " + volumeMeter2.Amplitude);
        //                    //tickeCounter += arrayBlockLength;
        //                    tickeCounter++;

        //                }

        //            };

        //        });
        //    task.Start();

        //}



        private Int16[] ByteToInt16(byte[] _buffer)
        {
            Int16[] intArray = new Int16[_buffer.Length / 2];
            for (int i = 0; i < _buffer.Length; i+=4)
            {
                intArray[(i/2) ] = BitConverter.ToInt16(_buffer, i);
                intArray[(i / 2) + 1] = BitConverter.ToInt16(_buffer, i + 2);
            }
            return intArray;
        }

        private float[] ByteToFLoat(byte[] buffer)
        {
            // float[] floatArray = new float[buffer.Length/2];
            //System.Buffer.BlockCopy(buffer, 0, floatArray, 0, buffer.Length);
            
            WaveBuffer wave = new WaveBuffer(buffer);
            
            float[] floatbuffer= wave.FloatBuffer;
            return floatbuffer;
        }

        public float[] Convert16BitToFloat(byte[] input)
        {
            int inputSamples = input.Length / 2;
            float[] output = new float[inputSamples];
            int outputIndex = 0;
            for (int i = 0; i < inputSamples; i++)
            {
                short sample = BitConverter.ToInt16(input, i * 2);
                output[outputIndex++]=sample/32768f;
            }
            return output;
        }



        private short[] ByteToShort(byte[] buffer)
        {
            WaveBuffer wave = new WaveBuffer(buffer);
            short[] vs = wave.ShortBuffer;
            return vs; 
        }


        private void strtVidOvlyBtn_Click(object sender, EventArgs e)
        {

            if(SelectedDevice != null)
            {
                if (SelectedDevice.inputDevice.isCapturing == false)
                {
                    try
                    {
                        StartRunningOutputPlayback();
                        strtVidOvlyBtn.Text = "Stop Device";
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    StopRunningOutputPlayback();
                }
            }          
        }
        void StopRunningOutputPlayback()
        {
            if (SelectedDevice.outputDevice.IsRecordingOutput = true)
            {
                StopRecordingOutput();
            }
            SelectedDevice.inputDevice.StopCapture();
            SelectedDevice.UnRegisterAudioBufferArrived(OnReceivedAudioBuffer);
            volumeMeter1.Amplitude = 0;
            volumeMeter2.Amplitude = 0;
            SelectedDevice.outputDevice.IsRecordingOutput = false;
            SelectedDevice.outputDevice.DisableAudioOutput();
            SelectedDevice.outputDevice.DisableVideoOutput();
            SelectedDevice.outputDevice.StopDisplayVideoFrameSync();
            strtVidOvlyBtn.Text = "Stop Device";
        }

        private void RcdOutBtn_Click(object sender, EventArgs e)
        {

            if (SelectedDevice.outputDevice != null)
            {
               
                    if (SelectedDevice.outputDevice.IsRecordingOutput == false)
                    {
                        //SelectedDevice.outputDevice.IsRecordingOutput = true;
                        StartRecordingOutput();
                         RcdOutBtn.Text = " Stop Output";
                    }
                    else
                    {
                        RcdOutBtn.Text = " Output Recording";
                        StopRecordingOutput();
                    }     
            }
            else
            {
                return;
            }
        }

        #region ShortClip Old Code

        //public void StartRecordingShortClip()
        //{
        //    var dialog = new SaveFileDialog();
        //    dialog.InitialDirectory = _SIROS.GetCurrentShortClipfolder(_SIROS.CurrentStructure);
        //    string filename = dialog.FileName;
        //    dialog.DefaultExt = ".avi";


        //    dialog.AddExtension = true;
        //    DialogResult ret = STAShowDialog(dialog);
        //    if (ret == DialogResult.OK)
        //    {

        //        isRecordingShortClip = true;

        //        var displayMode = configForm.CurrentDisplayMode;
        //        int _width = (int)displayMode.displayMode.GetWidth();
        //        int _height = (int)displayMode.displayMode.GetHeight();
        //        long frameDuration;
        //        long timeScale;
        //        displayMode.displayMode.GetFrameRate(out frameDuration, out timeScale);
        //        int frameRate = ((int)timeScale / (int)frameDuration);

        //        // to be replaced by FullDeviceDuplex class
        //        _shortClipWriter.Open(dialog.FileName, _width, _height, frameRate, VideoCodec.MPEG4, 5000000, AudioCodec.MP3, 80000, (int)configForm.AudioSampleRate, 2);
        //        //

        //        EventModelv1 ev = new EventModelv1();
        //        ev.EventName = "Short Clip Recording";
        //        ev.isAnomaly = false;
        //        EventLog vidstartedlog = new EventLog();
        //        vidstartedlog.evented = ev;
        //        vidstartedlog.loggedTime = DateTime.Now;
        //        SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
        //        logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
        //        logEventDGV.Refresh();

        //    };
        //}

        //private void ShortClipbtn_Click(object sender, EventArgs e)
        //{

        //    if (OutputDevice != null)
        //    {

        //        if (isRecordingShortClip==false)
        //        {

        //            StartRecordingShortClip();
        //            ShortClipbtn.Text = " Stop Output";
        //        }
        //        else
        //        {
        //            StopRecordingShortClip();
        //            ShortClipbtn.Text = " Output Recording";
        //            StopRecordingOutput();
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //void StopRecordingShortClip()
        //{
        //    isRecordingShortClip = false;
        //    _shortClipWriter.Close();
        //    //_shortClipWriter.Dispose();
        //}
        #endregion
        private void configureOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ovlyConfig.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProjectDialog.Show();
        }

        private void newDivingSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_SIROS.CurrentProject != null)
            {
                startDivingSession.Show();
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }
        public void InitializeMarkingToolCB()
        {
            MarkingToolCB.BeginUpdate();
            MarkingToolCB.Items.Add(new StringObjectPair<MarkingTools>("None", MarkingTools.None));
            MarkingToolCB.Items.Add(new StringObjectPair<MarkingTools>("Circle", MarkingTools.Circle));
            MarkingToolCB.Items.Add(new StringObjectPair<MarkingTools>("Arrow", MarkingTools.Arrow));
            MarkingToolCB.Items.Add(new StringObjectPair<MarkingTools>("Line", MarkingTools.Line));
            MarkingToolCB.Items.Add(new StringObjectPair<MarkingTools>("Rectangle", MarkingTools.Rectangle));
            MarkingToolCB.EndUpdate();

            MarkingToolCB.SelectedIndex = 0;
        }

        private void MarkingToolCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarkingToolOperation.markingTools = ((StringObjectPair<MarkingTools>)MarkingToolCB.SelectedItem).value;
        }

        private void windowsPreview1_Click(object sender, EventArgs e)
        {

        }

        #region FullScreen
        Size _PanelSize;
        private void ButtonFullScreen_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ScreenPanel.Size = this.ClientSize;

        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }

    //public class DialogState
    //{
    //    public DialogResult result;
    //    public FileDialog dialog;

    //    public void ThreadProcShowDialog()
    //    {
    //        result = dialog.ShowDialog();
    //    }
    //}
    //public struct StringObjectPair<T>
    //{
    //    public string name;
    //    public T value;

    //    public StringObjectPair(string name, T value)
    //    {
    //        this.name = name;
    //        this.value = value;
    //    }

    //    public override string ToString()
    //    {
    //        return name;
    //    }
    //}
    //public struct DisplayModeEntry
    //{
    //    public IDeckLinkDisplayMode displayMode;

    //    public DisplayModeEntry(IDeckLinkDisplayMode displayMode)
    //    {
    //        this.displayMode = displayMode;
    //    }

    //    public override string ToString()
    //    {
    //        string str;

    //        displayMode.GetName(out str);

    //        return str;
    //    }
    //}
    //public enum MarkingTools
    //{
    //    None=0,
    //    Circle,
    //    Arrow,
    //    Rectangle,
    //    Line
    //}

    //public static class MarkingToolOperation
    //{
    //    public static MarkingTools markingTools;
    //    public static bool isDrawing;
    //}
    




}
