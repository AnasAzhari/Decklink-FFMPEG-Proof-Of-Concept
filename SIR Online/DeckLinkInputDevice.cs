using System;
using System.Collections;
using System.Collections.Generic;
using DeckLinkAPI;

namespace SIR_Online
{
    public class DecklinkInputInvalidException : Exception { }

    #region EventArgs
    public class DeckLinkInputFormatChangedEventArgs:EventArgs
    {
        public readonly _BMDVideoInputFormatChangedEvents notificationEvents;
        public readonly _BMDDisplayMode displayMode;
        public readonly _BMDPixelFormat pixelFormat;

        public DeckLinkInputFormatChangedEventArgs(_BMDVideoInputFormatChangedEvents notificationEvents,_BMDDisplayMode displayMode,_BMDPixelFormat pixelFormat)
        {
            this.notificationEvents = notificationEvents;
            this.displayMode = displayMode;
            this.pixelFormat = pixelFormat;
        }
    }
    public class DecklinkAudioPacketArrivedEventArgs:EventArgs
    {
        public readonly IDeckLinkAudioInputPacket audioPacket;
        public DecklinkAudioPacketArrivedEventArgs(IDeckLinkAudioInputPacket audioInputPacket)
        {
            this.audioPacket = audioInputPacket;
        }
    }

    public class DeckLinkVideoFrameArrivedEventArgs:EventArgs
    {
        public readonly IDeckLinkVideoInputFrame videoFrame;
        public readonly IDeckLinkAudioInputPacket audioPacket;
        public readonly bool inputInvalid;
        public DeckLinkVideoFrameArrivedEventArgs(IDeckLinkVideoInputFrame videoFrame, IDeckLinkAudioInputPacket _audioPacket,bool inputInvalid)
        {
            this.videoFrame = videoFrame;
            this.audioPacket = _audioPacket;
            this.inputInvalid = inputInvalid;
        }
    }

    #endregion


    public class DeckLinkInputDevice:DeckLinkDevice,IDeckLinkInputCallback,IEnumerable<IDeckLinkDisplayMode>
    {
        private IDeckLinkInput m_deckLinkInput;
        private IDeckLinkKeyer in_keyer;
       
        private bool m_applyDetectedInputMode = true;
        private bool m_currentlyCapturing = false;
        private bool m_prevInputSignalAbsent = true;

        public bool isRecording = false;
        public bool isCaptureStill = false;

        public DeckLinkInputDevice(IDeckLink deckLink):base(deckLink)
        {
            if (!CaptureDevice)
                throw new DecklinkInputInvalidException();

            m_deckLinkInput = (IDeckLinkInput)deckLink;
            //in_keyer = (IDeckLinkKeyer)m_deckLinkInput;
        }

        public event EventHandler<DecklinkAudioPacketArrivedEventArgs> AudioPacketArrivedHandler;
        public event EventHandler<DeckLinkInputFormatChangedEventArgs> InputFormatChangedHandler;
        public event EventHandler<DeckLinkVideoFrameArrivedEventArgs> VideoFrameArrivedHandler;


        public IDeckLinkInput deckLinkInput
        {
            get
            {
                return m_deckLinkInput;
            }
        }
        public IDeckLinkKeyer InKeyer
        {
            get
            {
                return in_keyer;
            }
        }

        public bool isCapturing
        {
            get { return m_currentlyCapturing; }
        }

        private _BMDVideoInputFlags InputFlags
        {
            get { return m_applyDetectedInputMode ? _BMDVideoInputFlags.bmdVideoInputEnableFormatDetection : _BMDVideoInputFlags.bmdVideoInputFlagDefault; }
        }

        public bool IsvideoModeSupported(IDeckLinkDisplayMode displayMode,_BMDPixelFormat pixelFormat)
        {
            _BMDDisplayMode resultDisplayMode;
            int supported;
            try
            {
                m_deckLinkInput.DoesSupportVideoMode((_BMDVideoConnection)0, displayMode.GetDisplayMode(), pixelFormat, _BMDSupportedVideoModeFlags.bmdSupportedVideoModeDefault, out supported);
            }
            catch (Exception)
            {
              
                supported = 0;
            }
            return (supported != 0);

        }

        _BMDAudioSampleRate m_audioSampleRate = _BMDAudioSampleRate.bmdAudioSampleRate48kHz;
        _BMDAudioSampleType m_audioSampleDepth = _BMDAudioSampleType.bmdAudioSampleType16bitInteger;

        void IDeckLinkInputCallback.VideoInputFormatChanged(_BMDVideoInputFormatChangedEvents notificationEvents, IDeckLinkDisplayMode newDisplayMode, _BMDDetectedVideoInputFormatFlags detectedSignalFlags)
        {
            // Restart capture with the new video mode if told to
            if (!m_applyDetectedInputMode)
                return;

            var pixelFormat = _BMDPixelFormat.bmdFormat8BitYUV;
            if (detectedSignalFlags.HasFlag(_BMDDetectedVideoInputFormatFlags.bmdDetectedVideoInputRGB444))
                pixelFormat = _BMDPixelFormat.bmdFormat8BitBGRA;

            // Stop the capture
            m_deckLinkInput.StopStreams();

            var displayMode = newDisplayMode.GetDisplayMode();
            long frameDur;
            long timescale;
            newDisplayMode.GetFrameRate(out frameDur, out timescale);
            int frameRate = (int)(timescale / frameDur);
            Console.WriteLine(" Frame Rate : " + frameRate);

            // Set the video input mode
            m_deckLinkInput.EnableVideoInput(displayMode, pixelFormat, _BMDVideoInputFlags.bmdVideoInputEnableFormatDetection);

            m_deckLinkInput.EnableAudioInput(m_audioSampleRate, m_audioSampleDepth, 2);
            

            // Start the capture
            m_deckLinkInput.StartStreams();

            // Register input format changed event
            var handler = InputFormatChangedHandler;           // Check whether there are any subscribers to InputFormatChangedHandler
            if (handler != null)
            {
                handler(this, new DeckLinkInputFormatChangedEventArgs(notificationEvents, displayMode, pixelFormat));
            }
        }

        
        void IDeckLinkInputCallback.VideoInputFrameArrived(IDeckLinkVideoInputFrame videoFrame, IDeckLinkAudioInputPacket audioPacket)
        {
           
            if (videoFrame != null)
            {
                int frameRate;
                
                
                bool inputSignalAbsent = videoFrame.GetFlags().HasFlag(_BMDFrameFlags.bmdFrameHasNoInputSource);

                // Detect change in input signal, restart stream when valid stream detected 
                if (!inputSignalAbsent && m_prevInputSignalAbsent)
                {
                    m_deckLinkInput.StopStreams();
                    m_deckLinkInput.FlushStreams();
                    m_deckLinkInput.StartStreams();
                }
                m_prevInputSignalAbsent = inputSignalAbsent;

                // Register video frame received event
                var handler = VideoFrameArrivedHandler; 
            
                // Check whether there are any subscribers to VideoFrameArrivedHandler
                if (handler != null)
                {
                    handler(this, new DeckLinkVideoFrameArrivedEventArgs(videoFrame,audioPacket, inputSignalAbsent));

                }
                if (audioPacket != null)
                {

                    // Register audio packet received event
                    var handlerAudio = AudioPacketArrivedHandler;

                    // Check whether there are any subscribers to AudioPacketArrivedHandler
                    if (handlerAudio != null)
                    {
                        //Console.WriteLine(" audio packet is not null, " + audioPacket.GetPacketTime);
                        handlerAudio(this, new DecklinkAudioPacketArrivedEventArgs(audioPacket));
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(videoFrame);

            }
            else
            {
                Console.WriteLine(" video inout is NULL");
            }
            if (audioPacket == null)
            {
               // Console.WriteLine("Decklink Input audio packet null ");
            }        
        }

        IEnumerator<IDeckLinkDisplayMode> IEnumerable<IDeckLinkDisplayMode>.GetEnumerator()
        {
            IDeckLinkDisplayModeIterator displayModeIterator;
            m_deckLinkInput.GetDisplayModeIterator(out displayModeIterator);
            return new DisplayModeEnum(displayModeIterator);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }
        IDeckLinkDisplayMode _currentDisplayMode;
        public IDeckLinkDisplayMode CurrentDisplayMode
        {
            get { return _currentDisplayMode; }
            protected set { _currentDisplayMode = value; }
        }
       
        public void StartCapture(IDeckLinkDisplayMode displayMode, IDeckLinkScreenPreviewCallback screenPreviewCallback, bool applyDetectedInputMode,_BMDAudioSampleRate _audioRate,_BMDAudioSampleType _sampleDepth,uint channelCount)
        {
            
            if (m_currentlyCapturing)
                return;

            var videoInputFlags = _BMDVideoInputFlags.bmdVideoInputFlagDefault;

            m_applyDetectedInputMode = applyDetectedInputMode;
            m_prevInputSignalAbsent = true;

            // Enable input video mode detection if the device supports it
            if (SupportsFormatDetection && m_applyDetectedInputMode)
                videoInputFlags |= _BMDVideoInputFlags.bmdVideoInputEnableFormatDetection;

            // Set the screen preview
            if (screenPreviewCallback != null)
                m_deckLinkInput.SetScreenPreviewCallback(screenPreviewCallback);

            // Set capture callback
            m_deckLinkInput.SetCallback(this);
         

            // Set the video input mode
            m_deckLinkInput.EnableVideoInput(displayMode.GetDisplayMode(), _BMDPixelFormat.bmdFormat8BitYUV, videoInputFlags);
            CurrentDisplayMode = displayMode;
            Console.WriteLine(" width : " + displayMode.GetWidth() + "Height :  " + displayMode.GetHeight());
            // set audio input settings
            m_deckLinkInput.EnableAudioInput(_audioRate,_sampleDepth, channelCount);

            // Start the capture
            m_deckLinkInput.StartStreams();

            m_currentlyCapturing = true;
        }

        public void StopCapture()
        {
            if (!m_currentlyCapturing)
                return;

            RemoveAllListeners();

            // Stop the capture
            m_deckLinkInput.StopStreams();

            // Disable video input
            m_deckLinkInput.DisableVideoInput();

            // Disable callbacks
            m_deckLinkInput.SetScreenPreviewCallback(null);
            m_deckLinkInput.SetCallback(null);

            m_currentlyCapturing = false;
        }

       public void RemoveAllListeners()
        {
            AudioPacketArrivedHandler = null;
            InputFormatChangedHandler = null;
            VideoFrameArrivedHandler = null;
        }

    }
}
