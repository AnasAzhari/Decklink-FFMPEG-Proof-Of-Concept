using System;
using System.Collections.Generic;
using DeckLinkAPI;

namespace SIR_Online
{
    class DeckLinkOutputInvalidException : Exception { }
    class DeckLinkOutputNotEnabledException : Exception { }

    public class DeckLinkOutputFrameCompletitonEventArgs : EventArgs
    {
        public readonly _BMDOutputFrameCompletionResult completionResult;

        public DeckLinkOutputFrameCompletitonEventArgs(_BMDOutputFrameCompletionResult completionResult)
        {
            this.completionResult = completionResult;
        }
    }
    public class DeckLinkOutputDevice : DeckLinkDevice, IDeckLinkVideoOutputCallback, IDeckLinkAudioOutputCallback, IEnumerable<IDeckLinkDisplayMode>
    {
        private IDeckLinkOutput m_deckLinkOutput;
        private IDeckLinkKeyer m_keyer;          
        private long m_frameDuration;
        private long m_frameTimescale;



        private bool m_videoOutputEnabled = false;
        private bool m_playingOutput = false;
        private bool m_recording = false;

        public bool IsRecordingOutput
        {
            get
            {
                return m_recording;
            }
            set
            {
                if(value==true || value == false)
                {
                    m_recording = value;
                }
            }
        }

        private bool m_recordingShortClip = false;

        public bool IsRecordingShortClip

        {
            get
            {
                return m_recordingShortClip;
            }
            set
            {
                if (value == true || value == false)
                {
                    m_recordingShortClip = value;
                }
            }
        }


        private bool preRollOn;

        public DeckLinkOutputDevice(IDeckLink deckLink) : base(deckLink)
        {
            if (!PlaybackDevice)
                throw new DeckLinkOutputInvalidException();

            // Query output interface
            m_deckLinkOutput = (IDeckLinkOutput)deckLink;
            //m_keyer = (IDeckLinkKeyer)m_deckLinkOutput;
            m_playingOutput = false;
            // Provide the delegate to the audio and video output interfaces
            m_deckLinkOutput.SetScheduledFrameCompletionCallback(this);
            m_deckLinkOutput.SetAudioCallback(this);
        }

        public event EventHandler<DeckLinkOutputFrameCompletitonEventArgs> VideoFrameCompletedHandler;
        public event EventHandler PlaybackStoppedHandler;
        public event EventHandler AudioOutputRequestedHandler;

        public IDeckLinkOutput DeckLinkOutput
        {
            get { return m_deckLinkOutput; }
        }
        public IDeckLinkKeyer DecklinkKeyer
        {
            get { return m_keyer; }
        }

        
        public double FrameDurationMs
        {
            get { return 1000 * (double)m_frameDuration / (double)m_frameTimescale; }
        }

        IEnumerator<IDeckLinkDisplayMode> IEnumerable<IDeckLinkDisplayMode>.GetEnumerator()
        {
            IDeckLinkDisplayModeIterator displayModeIterator;
            m_deckLinkOutput.GetDisplayModeIterator(out displayModeIterator);
            return new DisplayModeEnum(displayModeIterator);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }
        public void SetScreenPreview(IDeckLinkScreenPreviewCallback previewCallback)
        {
            m_deckLinkOutput.SetScreenPreviewCallback(previewCallback);
        }

        public void EnableAudioOutput(uint audioChannelCount, _BMDAudioSampleType audioSampleDepth)
        {
            // Set the audio output mode
            m_deckLinkOutput.EnableAudioOutput(_BMDAudioSampleRate.bmdAudioSampleRate48kHz, audioSampleDepth, audioChannelCount, _BMDAudioOutputStreamType.bmdAudioOutputStreamContinuous);

            // Begin audio preroll.  This will begin calling our audio callback, which will start the DeckLink output stream.
            m_deckLinkOutput.BeginAudioPreroll();
        }

        public void EnableVideoOutput(IDeckLinkDisplayMode videoDisplayMode)
        {
            videoDisplayMode.GetFrameRate(out m_frameDuration, out m_frameTimescale);

            // Set the video output mode
            m_deckLinkOutput.EnableVideoOutput(videoDisplayMode.GetDisplayMode(), _BMDVideoOutputFlags.bmdVideoOutputFlagDefault);

            m_videoOutputEnabled = true;
        }

        public void DisableVideoOutput()
        {
            m_deckLinkOutput.DisableVideoOutput();

            m_videoOutputEnabled = false;
        }

        public void DisableAudioOutput()
        {
            m_deckLinkOutput.DisableAudioOutput();
        }

        public void DisplayVideoFrame(IDeckLinkVideoFrame videoFrame)                                                                                                                                                                                                 
        {
            if (!m_videoOutputEnabled)
                throw new DeckLinkOutputNotEnabledException();

            m_deckLinkOutput.DisplayVideoFrameSync(videoFrame);
        }
        public void PlayAudioFrame(IDeckLinkAudioInputPacket packet)
        {
            
        }

        public bool IsVideoModeSupported(IDeckLinkDisplayMode displayMode, _BMDPixelFormat pixelFormat)
        {
           
            _BMDDisplayMode ResultDisplayMode;
            int supported;
            try
            {
                m_deckLinkOutput.DoesSupportVideoMode(_BMDVideoConnection.bmdVideoConnectionUnspecified, displayMode.GetDisplayMode(), pixelFormat, _BMDSupportedVideoModeFlags.bmdSupportedVideoModeDefault, out ResultDisplayMode, out supported);
            }
            catch (Exception)
            {

                supported = 0;
            }
            return (supported != 0);
        }

        #region callbacks
        // Explicit implementation of IDeckLinkVideoOutputCallback and IDeckLinkAudioOutputCallback
        void IDeckLinkVideoOutputCallback.ScheduledFrameCompleted(IDeckLinkVideoFrame completedFrame, _BMDOutputFrameCompletionResult result)
        {
            // When a video frame has been completed, generate event to schedule next frame
            var handler = VideoFrameCompletedHandler;

            // Check whether any subscribers to VideoFrameCompletedHandler event
            if (handler != null)
            {
                handler(this, new DeckLinkOutputFrameCompletitonEventArgs(result));
            }
        }

        void IDeckLinkVideoOutputCallback.ScheduledPlaybackHasStopped()
        {
            var handler = PlaybackStoppedHandler;

            // Check whether any subscribers to PlaybackStoppedHandler event
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        void IDeckLinkAudioOutputCallback.RenderAudioSamples(int preroll)
        {
            // Provide further audio samples to the DeckLink API until our preferred buffer waterlevel is reached
            var handler = AudioOutputRequestedHandler;

            // Check whether any subscribers to AudioOutputRequestedHandler event
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            if (preroll != 0)
            {
                m_deckLinkOutput.StartScheduledPlayback(0, 100, 1.0);
            }
        }
        #endregion

        public void StartDisplayVideoFrameSync()
        {
            m_playingOutput = true;
            //m_deckLinkOutput.StartScheduledPlayback
        }
        public void StopDisplayVideoFrameSync()
        {
            m_playingOutput = false;
        }


    }

}
