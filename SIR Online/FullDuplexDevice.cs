using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DeckLinkAPI;
using System.Diagnostics;
//using Accord.Video.FFMPEG; // must copy all the dll in package/accord ffmpeg/build to bin debug folder or just clean and rebuild
// and also Accord.Video (not ffmpeg)
// must use Accord ver 3.7.0,Accord.Video ver 3.6.0, Accord.video.FFMPEG ver 3.3.0 
// only use mpeg4 as videoCodec
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

using SIR_Online.FFMPEG;

namespace SIR_Online
{
    public class FullDuplexDevice 
    {
        public DeckLinkInputDevice inputDevice;
        public DeckLinkOutputDevice outputDevice;

        SFSystemSettings configForm;

        SIROnlineSession _SIROS=SIROnlineSession.Instance;

        Bgra32FrameConverter m_frameConverter;
        IDeckLinkVideoConversion frameConverter;
     
        VideoFileWriter _videoFileWriter;
     
       
        VideoFileWriter _backupVideoFile;
        VideoFileWriter _shortClipWriter;
        GDIKeying GDIkey;

        WindowsPreviewOutput windowsPreviewOutput;

        public string DeviceName;


        public VideoFileWriter OutputVideoWriter
        {
            get
            {
                return _videoFileWriter;
            }
        }
        public VideoFileWriter BackupVideoFileWriter
        {
            get
            {
                return _backupVideoFile;
            }
        }
        public VideoFileWriter ShortClipWriter
        {
            get
            {
                return _shortClipWriter;
            }
        }
        bool _isRecordingShortClip;

        public bool isRecordingShortClip{
            get
            {
                return _isRecordingShortClip;
            }
            set
            {
                _isRecordingShortClip = value;
            }

            }
        public bool IsCaptureStill = false;

        //public FullDuplexDevice(string _deviceName,DeckLinkInputDevice _deckLinkInput,DeckLinkOutputDevice _decklinkOutputDevice) 
        //{
        //    try
        //    {
        //        DeviceName = _deviceName;
        //        inputDevice = _deckLinkInput;
        //        outputDevice = _decklinkOutputDevice;
        //        _videoFileWriter = new VideoFileWriter();


        //        _backupVideoFile = new VideoFileWriter();
        //        _shortClipWriter = new VideoFileWriter();
        //        m_frameConverter = new Bgra32FrameConverter();

        //        //configForm = _configureForm;
        //        GDIkey = new GDIKeying();
        //        _SIROS = SIROnlineSession.Instance;
        //        videoFramArriveHandler = new EventHandler<DeckLinkVideoFrameArrivedEventArgs>((s, e) => { OutputSync(e); });
        //        VideoInputFormatChangeHandler = new EventHandler<DeckLinkInputFormatChangedEventArgs>((j, r) => { _SIROS.SFSystemSettings.DisplayModeChanged(j, r); });
        //    }
        //    catch (Exception)
        //    {


        //    }
        //    finally
        //    {

        //    }
        //}
        public FullDuplexDevice(string _deviceName, IDeckLink _decklink)
        {
            try
            {
                DeviceName = _deviceName;
                inputDevice = new DeckLinkInputDevice(_decklink);
                outputDevice = new DeckLinkOutputDevice(_decklink);
                _videoFileWriter = new VideoFileWriter();


                _backupVideoFile = new VideoFileWriter();
                _shortClipWriter = new VideoFileWriter();
                m_frameConverter = new Bgra32FrameConverter();

                //configForm = _configureForm;
                GDIkey = new GDIKeying();
                _SIROS = SIROnlineSession.Instance;
                videoFramArriveHandler = new EventHandler<DeckLinkVideoFrameArrivedEventArgs>((s, e) => { OutputSync(e); });
                VideoInputFormatChangeHandler = new EventHandler<DeckLinkInputFormatChangedEventArgs>((j, r) => { DisplayModeChanged(j, r); });
            }
            catch (Exception)
            {


            }
            finally
            {
                VideoFormatDetection = true;
            }
        }

        public bool VideoFormatDetection;


        public _BMDDisplayMode currentDisplay;
        public _BMDPixelFormat currentPixelFormat;

        public void DisplayModeChanged(object sender, DeckLinkInputFormatChangedEventArgs e)
        {
            Console.WriteLine("Display Mode Changed");
            if (e.notificationEvents.HasFlag(_BMDVideoInputFormatChangedEvents.bmdVideoInputDisplayModeChanged))
            {
                //// Video input mode has changed, update combo-box
                //foreach (DisplayModeEntry item in comboBoxCaptureVideoMode.Items)
                //{
                //    if (item.displayMode.GetDisplayMode() == e.displayMode)
                //        this.Invoke(new MethodInvoker(() => { comboBoxCaptureVideoMode.SelectedItem = item; }));
                //}
                foreach (IDeckLinkDisplayMode displayMode in inputDevice)
                {
                    if (displayMode.GetDisplayMode() == e.displayMode)
                    {
                        currentDisplay = e.displayMode;
                       
                        //if (_SIROS.SFSystemSettings.M_selectedDecklinkDevice == this)
                        //{

                        //}
                    }
                        
                }
            }
            if (e.notificationEvents.HasFlag(_BMDVideoInputFormatChangedEvents.bmdVideoInputColorspaceChanged))
            {
                // Input pixel format has changed, update combo-box
                //foreach (StringObjectPair<_BMDPixelFormat> item in comboBoxCapturePixelFormat.Items)
                //{
                //    if (item.value == e.pixelFormat)
                //        comboBoxCaptureVideoMode.SelectedItem = item;
                //}
                //foreach (StringObjectPair<_BMDPixelFormat> item in _SIROS.SFSystemSettings.k)
                //{
                //    if (item.value == e.pixelFormat)
                //        comboBoxCaptureVideoMode.SelectedItem = item;
                //}
                currentPixelFormat = e.pixelFormat;
            }
        }



        public void StartRunningOutputPlayback(int vidModeFmtIdx,DisplayModeEntry displayMode,bool _formatDetection,int auChCount,_BMDAudioSampleType auSampleType,_BMDAudioSampleRate auSampleRate,WindowsPreview windowsinputPview,WindowsPreviewOutput windOutPView)
        {
            windowsPreviewOutput = windOutPView;
            int videomodeformatidx = vidModeFmtIdx;
            DisplayModeEntry displayModeEntry = displayMode;

            StartCapture(videomodeformatidx, displayModeEntry, windowsinputPview, _formatDetection,auSampleRate,auSampleType,auChCount);
     
            outputDevice.EnableVideoOutput(displayModeEntry.displayMode);
            outputDevice.EnableAudioOutput((uint)auChCount, auSampleType);
            outputDevice.SetScreenPreview(windOutPView);

            //outputDevice.DecklinkKeyer.Enable(1);
            //inputDevice.VideoFrameArrivedHandler += new EventHandler<DeckLinkVideoFrameArrivedEventArgs>((s, e) => this.Invoke((Action)(() => { OutPutGrahpicsWithInputFrame(e); })));
            //inputDevice.VideoFrameArrivedHandler += new EventHandler<DeckLinkVideoFrameArrivedEventArgs>((s, e) => { OutputSync(e); });
            inputDevice.VideoFrameArrivedHandler += videoFramArriveHandler;
            inputDevice.InputFormatChangedHandler += VideoInputFormatChangeHandler;
        }
        private void StartCapture(int videoModeFormatCurrIdx, DisplayModeEntry displayModeEntry,WindowsPreview windowsPreview,bool formatDetection,_BMDAudioSampleRate auSampleRate,_BMDAudioSampleType auSampleType,int auChCount)
        {
            //if (configForm.ComboboxVideoModeFormatCurrIdx < 0)
            //    return;
            //var displayMode = configForm.CurrentDisplayMode;
            if (videoModeFormatCurrIdx < 0)
                return;

            var displayMode = displayModeEntry;
           
            inputDevice.SetAnalogAudioInputConnection();
            //inputDevice.InputFormatChangedHandler += new EventHandler<DeckLinkInputFormatChangedEventArgs>((s, e) => { configForm.DisplayModeChanged(s, e); });

        
            //Console.WriteLine(" audio input RCA Channel Count : " + m_SelectedCaptureDevice.AudioInputRCAChannelCount);

            if (inputDevice != null)
            {
                // m_SelectedCaptureDevice.         
                inputDevice.StartCapture(displayMode.displayMode, windowsPreview, formatDetection, auSampleRate, auSampleType, (uint)auChCount);
            }

            // Update UI
            //buttonStartStop.Text = "Stop Capture";
            //EnableInterface(false);
        }
        EventHandler<DeckLinkVideoFrameArrivedEventArgs> videoFramArriveHandler;
        EventHandler<DeckLinkInputFormatChangedEventArgs> VideoInputFormatChangeHandler;
        int sec;

        public void RegisterAudioBufferArrived(Action<byte[]> _callback)
        {
            cbAudioBuffer += _callback;
        }
        public void UnRegisterAudioBufferArrived(Action<byte[]> _callback)
        {
            cbAudioBuffer -= _callback;
        }

        Action<byte[]> cbAudioBuffer;
        byte[] _audioBuffer;
        public byte[] AudioBuffer
        {
            get { return _audioBuffer; }
            set
            {
                if (value != null)
                {
                    byte[] oldvalue = _audioBuffer;
                    byte[] NewValue = value;
                    if (oldvalue != NewValue)
                    {
                        _audioBuffer = NewValue;
                        if(cbAudioBuffer!=null)
                            cbAudioBuffer(_audioBuffer);
                    }
                }
                

            }
        }
        
        void OutputSync(DeckLinkVideoFrameArrivedEventArgs inputframe)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            DateTime start=DateTime.Now;
               
            //Marshal.Copy()
            //Console.WriteLine(" frame count :" + inputframe.audioPacket.GetSampleFrameCount());

            // IDeckLinkVideoFrame vidFrame = m_frameConverter.ConvertFrame(inputframe.videoFrame);
            var converter = new Bgra32FrameConverter();
            IDeckLinkVideoFrame vidFrame = converter.ConvertFrame(inputframe.videoFrame);
            // IDeckLinkVideoFrame vidFrame;
            _BMDPixelFormat _BMDPixelFormat = vidFrame.GetPixelFormat();
           
            int width = vidFrame.GetWidth();
            int height = vidFrame.GetHeight();
           // var ratio = (double)width / height;
           // Console.WriteLine(" Ratio :" + ratio.ToString());
            _BMDPixelFormat pxF = vidFrame.GetPixelFormat();

            IntPtr bufferVid;
            vidFrame.GetBytes(out bufferVid);
         
            // output frame
            IDeckLinkMutableVideoFrame md_frame;

            outputDevice.DeckLinkOutput.CreateVideoFrame(width, height, width * 4, _BMDPixelFormat.bmdFormat8BitBGRA, _BMDFrameFlags.bmdFrameFlagDefault, out md_frame);

            IntPtr buffer;
            md_frame.GetBytes(out buffer);
            IntPtr[] bufferpointers = new IntPtr[1] { buffer };
            int stride = md_frame.GetRowBytes();
           
            // copy bytes from inputframe to outputframe
            GDIkey.CopyInputFrameMemtoOutput(buffer, bufferVid, height * stride);
       
            //byte[] audiobuffer = null;
            long packetDuration;
            long timeScale = 1;
            // audio
           
            if (inputframe.audioPacket != null)
            {
                IDeckLinkAudioInputPacket audioInputPacket = inputframe.audioPacket;
                //string s = audioInputPacket.GetDescription<IDeckLinkAudioInputPacket>();
                // Console.WriteLine(" audio input packet details :" + s);
                audioInputPacket.GetPacketTime(out packetDuration, 1);
                _BMDAudioSampleRate audioSampleRate = _SIROS.SFSystemSettings.AudioSampleRate;
                _BMDAudioSampleType audioDepth = _SIROS.SFSystemSettings.AudioSampleType;
                uint audioSamplesForthisFrame = (uint)((uint)audioSampleRate * (uint)packetDuration / (uint)timeScale);

                IntPtr InputaudioBuffer;
                audioInputPacket.GetBytes(out InputaudioBuffer);

                //audiobuffer = CopyAudioInputMemoryToByteArray(InputaudioBuffer, audioInputPacket.GetSampleFrameCount(), (int)configForm.ChannelCount, configForm.AudioSampleType);
                AudioBuffer = CopyAudioInputMemoryToByteArray(InputaudioBuffer, audioInputPacket.GetSampleFrameCount(), (int)_SIROS.SFSystemSettings.ChannelCount, _SIROS.SFSystemSettings.AudioSampleType);
                GCHandle pinnedArray = GCHandle.Alloc(AudioBuffer, GCHandleType.Pinned);
                IntPtr pointer = pinnedArray.AddrOfPinnedObject();

                uint framewritten;
                outputDevice.DeckLinkOutput.WriteAudioSamplesSync(pointer, (uint)audioInputPacket.GetSampleFrameCount(), out framewritten);
                pinnedArray.Free();

            }
            else
            {
               
            }

            using (var v = new Bitmap(md_frame.GetWidth(), md_frame.GetHeight(), stride, PixelFormat.Format32bppArgb, buffer))
            {
                lock (v)
                {
                    int widthframe = md_frame.GetWidth();
                    int heightframe = md_frame.GetHeight();
                    var graphics = Graphics.FromImage(v);
                    graphics.Flush();

                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    #region Overlay and Marking Tool Drawing
                    Pen pen = new Pen(Color.AntiqueWhite, 0.0004f);
                    if (windowsPreviewOutput.IsDrawing)
                    {
                        DrawMarkingTool(graphics, pen, widthframe, heightframe);
                    }

                    OverlayDrawing(width, height, graphics);
                    #endregion
                    DateTime now = DateTime.Now;
                    TimeSpan span = now - start;

                    #region Recording Operation
                    if (outputDevice.IsRecordingOutput)
                    {

                        sec = (int)(DateTime.Now - startTime).TotalSeconds;

                        if ((sec % (RecordingSettingData.CutOffDur * 60) == 0) && sec != 0)
                        {
                            // Console.WriteLine(" callback called minute :" + sec+" cutoff dur :" +RecordingSettingData.CutOffDur);
                            if (!IsAlreadyOpenWriter)
                                cbCutOffCaller();

                        }
                        else
                        {
                            IsAlreadyOpenWriter = false;    
                        }
                        // if(_videoFileWriter.IsOpen && _backupVideoFile.IsOpen && outputDevice.IsRecordingOutput)
                        if (_videoFileWriter.IsOpen && outputDevice.IsRecordingOutput)
                        {
                            RecordingOutput(v, AudioBuffer, span);
                        }
                                                 
                    }
                    if (outputDevice.IsRecordingShortClip && _shortClipWriter.IsOpen && _shortClipWriter != null)
                    {
                        RecordingOutput(v, AudioBuffer, span);
                    }
                    #endregion

                    #region SnapShot
                    if (IsCaptureStill && _SIROS.CurrentProject != null && _SIROS.CurrentStructure != null)
                    {
                        string OutputFilename = System.IO.Path.Combine(_SIROS.GetCurrentDiveSnapshotFoler(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), buffer.ToString() + ".jpeg");
                        //v.Save(OutputFilename, ImageFormat.Jpeg);
                        v.Save(OutputFilename, ImageFormat.Jpeg);
                        IsCaptureStill = false;
                    }

                    #endregion
                }
            }
            outputDevice.DeckLinkOutput.DisplayVideoFrameSync(md_frame);
            sw.Stop();
            //Console.WriteLine("Device :" + DeviceName + " Logged milisecond : " + sw.ElapsedMilliseconds + " ms");
            Marshal.ReleaseComObject(md_frame);                            
        }
        
        #region OverlayDrawing
        void OverlayDrawing(int width,int height,Graphics graphics)
        {
            var ratio = (double)width / height;
            
            OverlayScreen overlayScreen;
            OverlayScreen introScreen;

            switch (ratio)
            {
                case 1.48:
                    overlayScreen = Overlay.NTSCScreen;
                    introScreen = Overlay.NTSCIntroPage;
                    break;
                case 1.25:
                    overlayScreen = Overlay.PALScreen;
                    introScreen = Overlay.PALIntro;
                    break;
                case 1.77:
                    overlayScreen = Overlay.HDScreen;
                    introScreen = Overlay.HDIntro;
                    break;
                 
                default:
                    overlayScreen = Overlay.HDScreen;
                    introScreen = Overlay.HDIntro;
                    break;
            }
         
            // Intro Page
        
            if(overlayScreen ==null || introScreen == null)
            {
                return;
            }
            if (sec!=0 && sec <= introScreen.Seconds)
            {
                if (introScreen != null)
                {
                    for (int i = 0; i < introScreen.TextOverlays.Length; i++)
                    {

                        SolidBrush brush = new SolidBrush(Color.FromArgb(200, introScreen.TextOverlays[i].Textcolor));
                        StringFormat sf = new StringFormat();
                        graphics.DrawString(introScreen.TextOverlays[i].Text, introScreen.TextOverlays[i].RealFont, brush, introScreen.TextOverlays[i].Location.X, introScreen.TextOverlays[i].Location.Y);
                    }
                }
            }
            else
            {
                if (overlayScreen != null)
                {
                    for (int i = 0; i < overlayScreen.TextOverlays.Length; i++)
                    {
                        SolidBrush brush = new SolidBrush(Color.FromArgb(200, overlayScreen.TextOverlays[i].Textcolor));
                        StringFormat sf = new StringFormat();
                        if (!overlayScreen.TextOverlays[i].isDataString)
                        {
                            graphics.DrawString(overlayScreen.TextOverlays[i].Text, overlayScreen.TextOverlays[i].RealFont, brush, overlayScreen.TextOverlays[i].Location.X, overlayScreen.TextOverlays[i].Location.Y);
                        }
                        else
                        {
                            graphics.DrawString(overlayScreen.TextOverlays[i].GetTextFromSurveyData(), overlayScreen.TextOverlays[i].RealFont, brush, overlayScreen.TextOverlays[i].Location.X, overlayScreen.TextOverlays[i].Location.Y);
                            Console.WriteLine(" text info : " + overlayScreen.TextOverlays[i].GetTextFromSurveyData());
                        }                      
                    }
                }
            }
        }
        #endregion

        Action cbCutOffCaller;
        DateTime startTime;
        int cutOffCount;
        bool IsAlreadyOpenWriter;
        string singleFilenama;

      
        int Width,Height,FramRate,BitRate,AuBitRate,SampleRate,AudioChannel;

        VideoCodec VideoCodeck;
        AudioCodec AudioCodeck;
      
        void OnCutOff()
        {
             Console.WriteLine(" callback ");
 
            _videoFileWriter.Close();
            //_backupVideoFile.Close();
          
            cutOffCount++;
            string File_nama = System.IO.Path.Combine(_SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName + "_"+cutOffCount.ToString() + singleFilenama);
            //string BackupName = System.IO.Path.Combine(_SIROS.GetCurrentBackupDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName + "_"+cutOffCount.ToString() + singleFilenama);
            //_videoFileWriter.Open(File_nama, Width, Height, FramRate, VideoCodeck, BitRate, AudioCodeck, AuBitRate, SampleRate, AudioChannel);
            _videoFileWriter.Open(DeviceName,ffmpegloc, Width, Height, BitRate, FramRate, File_nama);
            //_backupVideoFile.Open(BackupName, Width, Height, FramRate, VideoCodeck, BitRate, AudioCodeck, AuBitRate, SampleRate, AudioChannel);
            IsAlreadyOpenWriter = true;           
        }

        // Pause
        bool _isaldredyDefineFileName=false;
        public bool IsAlreadyDefineFilename { get { return _isaldredyDefineFileName; } protected set { _isaldredyDefineFileName = value; } }

        //public void StartDeviceRecording(string _filename, string _backupFilename, int _width, int _height, int _frameRate, VideoCodec _vidCodec, int _bitrate, AudioCodec _audioCodec, int _aubitRate, int _sampleRate, int _auChannel)
        //{

        //    try
        //    {
        //        if (IsAlreadyDefineFilename == false)
        //        {
        //            cutOffCount = 0;
        //            Width = _width;
        //            Height = _height;
        //            FramRate = _frameRate;
        //            VideoCodeck = _vidCodec;
        //            BitRate = _bitrate;
        //            AudioCodeck = _audioCodec;
        //            AuBitRate = _aubitRate;
        //            SampleRate = _sampleRate;
        //            AudioChannel = _auChannel;
        //            //Console.WriteLine(DeviceName + " width height size : " + _width + "_" + _height);
        //            //var bckup = dialog.FileName.Remove(0, _SIROS.GetCurrentDiveVideoFolder().Length + 1);
        //            singleFilenama = _filename.Remove(0, _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType).Length + 1);
        //            string File_name = System.IO.Path.Combine(_SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName+"_"+ cutOffCount.ToString() + singleFilenama);
        //            string BackupName = System.IO.Path.Combine(_SIROS.GetCurrentBackupDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName+ "_"+ cutOffCount.ToString() + singleFilenama);
        //            _videoFileWriter.Open(File_name, _width, _height, _frameRate, _vidCodec, _bitrate, _audioCodec, _aubitRate, _sampleRate, _auChannel);
        //            _backupVideoFile.Open(BackupName, _width, _height, _frameRate, _vidCodec, _bitrate, _audioCodec, _aubitRate, _sampleRate, _auChannel);
        //            IsAlreadyDefineFilename = true;
        //        }
        //        else
        //        {

        //            // if is already define file info. then continue recording

        //        }


        //        //BackupFileName = _backupFilename;

        //        //Console.WriteLine("writer openend :");
        //        startTime = DateTime.Now;

        //        cbCutOffCaller += OnCutOff;
        //    }
        //    catch (Exception e)
        //    {


        //    }

        //}
        string ffmpegloc;
        public void StartDeviceRecording(string ffmpegLoc, int _width, int _height, string _filename, int _frameRate, int _bitrate)
        {
            FramRate = _frameRate;
            BitRate = _bitrate;
            ffmpegloc = ffmpegLoc;
            Width = _width;
            Height = _height;
            singleFilenama = _filename.Remove(0, _SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType).Length + 1);
            string File_name = System.IO.Path.Combine(_SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName + "_" + cutOffCount.ToString() + singleFilenama);
            _videoFileWriter.Open(DeviceName,ffmpegLoc, _width, _height, _bitrate, _frameRate, File_name);
            cbCutOffCaller += OnCutOff;
            startTime = DateTime.Now;
        }

        //public void StartRecordingShortClip(string _filename, int _width, int _height, int _frameRate, VideoCodec _vidCodec, int _bitrate, AudioCodec _audioCodec, int _aubitRate, int _sampleRate, int _auChannel)
        //{
        //    string snapshotFilename = _filename.Remove(0, _SIROS.GetCurrentShortClipfolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType).Length + 1);
        //    string File_name = System.IO.Path.Combine(_SIROS.GetCurrentShortClipfolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName+ snapshotFilename);
        //    _shortClipWriter.Open(File_name, _width, _height, _frameRate, _vidCodec, _bitrate, _audioCodec, _aubitRate, _sampleRate, _auChannel);
        //}
        public void ContinueRecording()
        {
            string File_nama = System.IO.Path.Combine(_SIROS.GetCurrentDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName + "_" + cutOffCount.ToString() + singleFilenama);
            //string BackupName = System.IO.Path.Combine(_SIROS.GetCurrentBackupDiveVideoFolder(_SIROS.CurrentStructure, _SIROS.CurrentDiveType), DeviceName + "_" + cutOffCount.ToString() + singleFilenama);
            //_videoFileWriter.Open(File_nama, Width, Height, FramRate, VideoCodeck, BitRate, AudioCodeck, AuBitRate, SampleRate, AudioChannel);
            _videoFileWriter.Open(DeviceName,ffmpegloc, Width, Height, BitRate, FramRate, File_nama);
            // _backupVideoFile.Open(BackupName, Width, Height, FramRate, VideoCodeck, BitRate, AudioCodeck, AuBitRate, SampleRate, AudioChannel);
            cbCutOffCaller += OnCutOff;
        }

        Byte[] CopyAudioInputMemoryToByteArray(IntPtr audioBuffer, int samplesToWrite, int channels, _BMDAudioSampleType sampleDepth)
        {
            byte[] buffer = null;
            if ((uint)sampleDepth == 16)
            {
                //Int16[] buffer16 = new Int16[channels * samplesToWrite];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite*2));
                //Marshal.Copy(audioBuffer,0,)
                //Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                //Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                //buffer = new byte[(int)(channels * samplesToWrite * sizeof(Int16))];
                ////Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));
                //System.Buffer.BlockCopy(buffer16, 0, buffer, 0, buffer.Length);

                buffer = new byte[(int)(channels * samplesToWrite * sizeof(Int16))];
                Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(Int16)));

                return buffer;

                //return buffer;
            }
            else if ((uint)sampleDepth == 32)
            {
           
                buffer = new byte[(int)(channels * samplesToWrite * sizeof(int))];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(samplesToWrite * sizeof(int)));
                Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));
           
                return buffer;             
            }
            return buffer;
        }
        private bool Writing;

        async void RecordingOutput(Bitmap videoFrame, byte[] audiobuffer,TimeSpan timeSpan)
        {
            if (!Writing)
            {
                Writing = true;

                _videoFileWriter.WriteVideoFrame(videoFrame);
              
                //_backupVideoFile.WriteVideoFrame(videoFrame);

                if (audiobuffer != null)
                {
                    
                    _videoFileWriter.WriteAudioFrame(audiobuffer);
                   // _backupVideoFile.WriteAudioFrame(audiobuffer);

                }

           
                if (_shortClipWriter != null && _shortClipWriter.IsOpen && videoFrame != null)
                {
                    _shortClipWriter.WriteVideoFrame(videoFrame);
                    if (audiobuffer != null)
                    {
                        _shortClipWriter.WriteAudioFrame(audiobuffer);
                    }
                }
            Writing = false;
        }

    }

        public void StopRecordingShortClip()
        {
            outputDevice.IsRecordingShortClip = false;
            _shortClipWriter.Close();
        }

      
        public void StopRecordingOutput()
        {
            
            //inputDevice.VideoFrameArrivedHandler -= videoFramArriveHandler;
            outputDevice.IsRecordingOutput = false;
            while (Writing)
            {

            }
   
            _videoFileWriter.Close();
            
           // _backupVideoFile.Close();       

            cutOffCount++;
            
            cbCutOffCaller -= OnCutOff;
        }

        #region MarkingToolDrawing

        void DrawMarkingTool(Graphics graphics,Pen _p,int widthbitmap,int heightBitmap)
        {
            try
            {
                switch (MarkingToolOperation.markingTools)
                {
                    case MarkingTools.None:
                        break;
                    case MarkingTools.Circle:
                        Pen pen = new Pen(Color.Red, 1);

                        int X = (int)(windowsPreviewOutput.center.X / ((float)windowsPreviewOutput.Width / (float)widthbitmap));
                        int Y = (int)(windowsPreviewOutput.center.Y / ((float)windowsPreviewOutput.Height / (float)heightBitmap));
                        System.Drawing.Point Center = new System.Drawing.Point(X, Y);
                        //System.Drawing.Point Center = windowsPreviewOutput.center;
                        int xend = (int)(windowsPreviewOutput.EndLocation.X / ((float)windowsPreviewOutput.Width / (float)widthbitmap));
                        int yend = (int)(windowsPreviewOutput.EndLocation.Y / ((float)windowsPreviewOutput.Height / (float)heightBitmap));
                        Point EndLoc = new Point(xend, yend);
                        //System.Drawing.Point EndLoc = windowsPreviewOutput.EndLocation;

                        Rectangle rect = new Rectangle(Center, new Size(Math.Abs(Center.X - EndLoc.X) * 2, Math.Abs(Center.Y - EndLoc.Y) * 2));

                        int length = (int)Math.Pow((Math.Pow(EndLoc.X - Center.X, 2) + Math.Pow(EndLoc.Y - Center.Y, 2)), 0.5);
                        graphics.DrawArc(pen, Center.X, Center.Y, length, length, 0, 360);

                        break;
                    case MarkingTools.Arrow:
                        Pen penArrow = new Pen(Color.Red, 1);
                        AdjustableArrowCap bigCap = new AdjustableArrowCap(5, 5);
                        penArrow.CustomEndCap = bigCap;

                        int XArrow = (int)(windowsPreviewOutput.center.X / ((float)windowsPreviewOutput.Width / (float)widthbitmap));
                        int YArrow = (int)(windowsPreviewOutput.center.Y / ((float)windowsPreviewOutput.Height / (float)heightBitmap));
                        Point CenterArrow = new Point(XArrow, YArrow);

                        int XArrowEnd = (int)(windowsPreviewOutput.EndLocation.X / ((float)windowsPreviewOutput.Width / (float)widthbitmap));
                        int YArrowEnd = (int)(windowsPreviewOutput.EndLocation.Y / ((float)windowsPreviewOutput.Height / (float)heightBitmap));
                        Point EndLocArrow = new Point(XArrowEnd, YArrowEnd);
                        //System.Drawing.Point CenterArrow = windowsPreviewOutput.center;                  
                        //System.Drawing.Point EndLocArrow = windowsPreviewOutput.EndLocation;
                        graphics.DrawLine(penArrow, CenterArrow, EndLocArrow);

                        break;
                    case MarkingTools.Rectangle:
                        break;
                    case MarkingTools.Line:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                
                
            }        
        }
        #endregion
    }
    //Task Scheduler on Single Thread.

    public sealed class SingleThreadTaskScheduler : TaskScheduler
    {
        [ThreadStatic]
        private static bool isExecuting;
        private readonly CancellationToken cancellationToken;

        private readonly System.Collections.Concurrent.BlockingCollection<Task> Taskqueue;

        public SingleThreadTaskScheduler(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            this.Taskqueue = new System.Collections.Concurrent.BlockingCollection<Task>();
        }

        public void Start()
        {
            new Thread(RunOnCurrentThread) { Name = "STTS Thread" }.Start();
        }

        // Just a helper for the sample code
        public Task Schedule(Action action)
        {
            return
                Task.Factory.StartNew
                    (
                        action,
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        this
                    );
        }

        // You can have this public if you want - just make sure to hide it
        private void RunOnCurrentThread()
        {
            isExecuting = true;

            try
            {
                foreach (var task in Taskqueue.GetConsumingEnumerable(cancellationToken))
                {
                    TryExecuteTask(task);
                }
            }
            catch (OperationCanceledException)
            { }
            finally
            {
                isExecuting = false;
            }
        }

        // Signaling this allows the task scheduler to finish after all tasks complete
        public void Complete() { Taskqueue.CompleteAdding(); }
        protected override IEnumerable<Task> GetScheduledTasks() { return null; }

        protected override void QueueTask(Task task)
        {
            try
            {
                Taskqueue.Add(task, cancellationToken);
            }
            catch (OperationCanceledException)
            { }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // We'd need to remove the task from queue if it was already queued. 
            // That would be too hard.
            if (taskWasPreviouslyQueued) return false;

            return isExecuting && TryExecuteTask(task);
        }
    }
}