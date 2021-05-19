using System;
using DeckLinkAPI;

namespace SIR_Online
{
    public class DeckLinkDiscoveryEventArgs:EventArgs
    {
        public readonly IDeckLink deckLink;
        public DeckLinkDiscoveryEventArgs(IDeckLink deckLink)
        {
            this.deckLink = deckLink;
            
        }

        private _BMDVideoIOSupport IOSupport
        {
            get
            {
                long ioSupportAttribute;
                var decklinkAttributes = (IDeckLinkAttributes_v10_11)deckLink;
                decklinkAttributes.GetInt(_BMDDeckLinkAttributeID.BMDDeckLinkVideoIOSupport, out ioSupportAttribute);
                return (_BMDVideoIOSupport)ioSupportAttribute;
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



    }

    public class DeckLinkDeviceDiscovery:IDeckLinkDeviceNotificationCallback
    {
        public IDeckLinkDiscovery m_deckLinkDiscovery;
        private bool m_deckLinkDiscoveryEnabled = false;

        public event EventHandler<DeckLinkDiscoveryEventArgs> DeviceArrived;
        public event EventHandler<DeckLinkDiscoveryEventArgs> DeviceRemoved;

        public DeckLinkDeviceDiscovery()
        {
            m_deckLinkDiscovery = new CDeckLinkDiscovery();

        }
        ~DeckLinkDeviceDiscovery()
        {
            Disable();
        }
        public void Enable()
        {
            m_deckLinkDiscovery.InstallDeviceNotifications(this);
            m_deckLinkDiscoveryEnabled = true;
        }

        public void Disable()
        {
            if (m_deckLinkDiscoveryEnabled)
            {
                m_deckLinkDiscovery.UninstallDeviceNotifications();
                m_deckLinkDiscoveryEnabled = false;
            }
        }

        #region callBacks
        void IDeckLinkDeviceNotificationCallback.DeckLinkDeviceArrived(IDeckLink deckLinkDevice)
        {
            EventHandler<DeckLinkDiscoveryEventArgs> handler = DeviceArrived;

            // check whter there are any subscribers to DeviceArrived event
            if (handler != null)
            {
                handler(this, new DeckLinkDiscoveryEventArgs(deckLinkDevice));
            }
        }
        void IDeckLinkDeviceNotificationCallback.DeckLinkDeviceRemoved(IDeckLink deckLinkDevice)
        {
            EventHandler<DeckLinkDiscoveryEventArgs> handler = DeviceRemoved;

            // check whter there are any subscribers to DeviceRemoved event
            if(handler != null)
            {
                handler(this, new DeckLinkDiscoveryEventArgs(deckLinkDevice));
            }
        }
        #endregion
    }
}
