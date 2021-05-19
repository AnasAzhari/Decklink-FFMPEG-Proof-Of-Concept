using System;
using DeckLinkAPI;

namespace SIR_Online
{
    public class DeckLinkDevice
    {
        private IDeckLink m_deckLink;
        private IDeckLinkConfiguration decklinkConfig;
        //private DeckLinkInputDevice channelInputDevice;
        //private DeckLinkOutputDevice channelOutputDevice;

        public DeckLinkDevice(IDeckLink deckLink)
        {
            m_deckLink = deckLink;
            decklinkConfig = (IDeckLinkConfiguration)m_deckLink;
            //if ((IDeckLinkInput)m_deckLink !=null)
            //{
            //    channelInputDevice = new DeckLinkInputDevice(this.m_deckLink);
            //}
            //if ((IDeckLinkOutput)m_deckLink != null)
            //{
            //    channelOutputDevice = new DeckLinkOutputDevice(this.m_deckLink);
            //}
        }

        public IDeckLink DeckLink
        {
            get { return m_deckLink; }
        }
        public string DeviceName
        {
            get
            {
                string deviceName;
                m_deckLink.GetDisplayName(out deviceName);
                return deviceName;
            }
        }

        public bool CaptureDevice
        {
            get { return IOSupport.HasFlag(_BMDVideoIOSupport.bmdDeviceSupportsCapture); }
        }

        public bool PlaybackDevice
        {
            get { return IOSupport.HasFlag(_BMDVideoIOSupport.bmdDeviceSupportsPlayback); }
        }

      
        public bool SupportsFormatDetection
        {
            get
            {
                int flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.BMDDeckLinkSupportsInputFormatDetection, out flag);
                return flag != 0;
            }
        }
        public bool SupportInternalKeying
        {
            get
            {
                int flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.BMDDeckLinkSupportsInternalKeying,out flag);
                return flag != 0;
            }
        }
        //public bool SupportHDKeying
        //{
        //    get
        //    {
        //        //BMDDeckLinkSupportsHDKeying
        //        int flag;
        //        var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
        //        decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.bmddecklinksupports, out flag);
        //        return flag != 0;
        //    }
        //}
        //public bool SupportFullDuplex
        //{
        //    get
        //    {
        //        int flag;
        //        var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
        //        decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.BMDDeckLinkSupportsFullDuplex, out flag);
        //        return flag != 0;
        //    }
        //}
        //public bool SupportDuplexModeConfiguration
        //{
        //    get
        //    {
        //        int flag;
        //        var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
        //        //BMDDeckLinkSupportsDuplexModeConfiguration
        //        decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.bmddecklinksupports, out flag);
        //        return flag != 0;
        //    }
        //}

        public bool SupportAudioInput
        {
            get
            {
                int flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetFlag(_BMDDeckLinkAttributeID.BMDDeckLinkAudioInputConnections, out flag);
                return flag != 0;
            }
        }

        //public bool AudioInputGain
        //{
        //    get
        //    {
        //        int flag;
        //        var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
        //        decklinkAttributes.GetFlag(_BMDDeckLinkConfigurationID.bmdDeckLinkConfigMicrophoneInputGain, out flag);
        //        return flag != 0;
        //    }
        //}

        public int SupportMaximumAudioChannelNumber
        {
            get
            {
                long flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkMaximumAudioChannels, out flag);
                return (int)flag;
            }
        }

        public int AudioInputRCAChannelCount
        {
            get
            {
                long flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkAudioInputRCAChannelCount, out flag);
                return (int)flag;
            }
        }
        public void SetAnalogAudioInputConnection()
        {          
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkConfig.SetInt(_BMDDeckLinkConfigurationID.bmdDeckLinkConfigAudioInputConnection, (int)_BMDAudioConnection.bmdAudioConnectionEmbedded);                      
        }

        public int SupportDuplexDeviceNumber
        {
            get
            {
                long flag;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkNumberOfSubDevices, out flag);
                return (int)flag;
            }
        }


        private _BMDVideoIOSupport IOSupport
        {
            get
            {
                long ioSupportAttribute;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkVideoIOSupport, out ioSupportAttribute);
                return (_BMDVideoIOSupport)ioSupportAttribute;
            }
        }

        private _BMDProfileID IOSupportID

        {
            get
            {
                long ioSupportAttribute;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)m_deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkVideoIOSupport, out ioSupportAttribute);
                return (_BMDProfileID)ioSupportAttribute;
            }
        }


        //public int GetDevice()
        //{
        //    ge
        //}
        //public int getdevices()
        //{
           
        //}

        //public IDeckLinkIterator GetPeers()
        //{
        //    Get
        //    {
        //        IDeckLink deck;
        //        var decklinkAttributes = (IDeckLinkProfile)m_deckLink;
        //        decklinkAttributes.GetDevice(out deck);
        //        return deck;
        //    }
        //}

        public void RemoveInterlacing()
        {
            decklinkConfig.SetFlag(_BMDDeckLinkConfigurationID.bmdDeckLinkConfigFieldFlickerRemoval,1 );
        }
        //public void SetFullDuplexMode()
        //{
        //    //bmdDeckLinkConfigDuplexMode
        //    decklinkConfig.SetInt(_BMDDeckLinkConfigurationID.bmddecklinkconfigd,(int)_BMDDuplexMode.bmdDuplexModeFull);
        //}



    }
}
