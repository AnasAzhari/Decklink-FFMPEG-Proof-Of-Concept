using System;
using System.Collections.Generic;
using DeckLinkAPI;

namespace SIR_Online
{
    public class DisplayModeEnum : IEnumerator<IDeckLinkDisplayMode>
    {
        private IDeckLinkDisplayModeIterator m_displayModeIterator;
        private IDeckLinkDisplayMode m_displayMode;

        public DisplayModeEnum(IDeckLinkDisplayModeIterator displayModeIterator)
        {
            m_displayModeIterator = displayModeIterator;
        }

        IDeckLinkDisplayMode IEnumerator<IDeckLinkDisplayMode>.Current
        {
            get { return m_displayMode; }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            m_displayModeIterator.Next(out m_displayMode);
            return m_displayMode != null;
        }

        void IDisposable.Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { return m_displayMode; }
        }

        void System.Collections.IEnumerator.Reset()
        {
            throw new InvalidOperationException();
        }
    }
}
