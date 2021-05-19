using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeckLinkAPI;
using Direct3D = Microsoft.DirectX.Direct3D;

namespace SIR_Online
{
    public partial class WindowsPreview : Control, IDeckLinkScreenPreviewCallback
    {

        private IDeckLinkDX9ScreenPreviewHelper m_previewHelper;
        private string m_timeCodeString;
        

        private Direct3D.Device m_d3DDevice;
        
        private Direct3D.Font m_d3DFont;
    
        public WindowsPreview()
        {
            m_previewHelper = new CDeckLinkDX9ScreenPreviewHelper();
            InitializeComponent();
   
        }
        public void InitD3D()
        {

            var d3dpp = new Direct3D.PresentParameters();
            d3dpp.BackBufferFormat = Direct3D.Format.Unknown;
            d3dpp.BackBufferCount = 2;
            d3dpp.Windowed = true;
            d3dpp.SwapEffect = Direct3D.SwapEffect.Discard;
            d3dpp.DeviceWindow = this;
            d3dpp.PresentationInterval = Direct3D.PresentInterval.Default;

            m_d3DDevice = new Direct3D.Device(0, Direct3D.DeviceType.Hardware, this, Direct3D.CreateFlags.HardwareVertexProcessing | Direct3D.CreateFlags.MultiThreaded, d3dpp);

            m_d3DDevice.DeviceReset += device_DeviceReset;
            device_DeviceReset(m_d3DDevice, null);

            unsafe
            {
                m_previewHelper.Initialize(new IntPtr(m_d3DDevice.UnmanagedComPointer));
            }
        }

        void device_DeviceReset(object sender, EventArgs e)
        {
            System.Drawing.Font systemfont = new System.Drawing.Font(FontFamily.GenericMonospace, 14f, FontStyle.Regular);
            m_d3DFont = new Microsoft.DirectX.Direct3D.Font(m_d3DDevice, systemfont);
        }

        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);
        //}
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
        }
        void Render()
        {
            m_d3DDevice.BeginScene();

            // Render target needs to be set to 640x360 for optimal scaling. However the pixel coordinates for 
            // Direct3D 9 render target is actually (-0.5,-0.5) to (639.5,359.5).  As such the Viewport is set 
            // to 639x359 to account for the pixel coordinate offset of render target
            tagRECT rect;
            rect.top = m_d3DDevice.Viewport.Y;
            rect.left = m_d3DDevice.Viewport.X;
            rect.bottom = m_d3DDevice.Viewport.Y + m_d3DDevice.Viewport.Height;
            rect.right = m_d3DDevice.Viewport.X + m_d3DDevice.Viewport.Width;

            m_previewHelper.Render(rect);

            // Draw the timecode top-center with a slight drop-shadow
           
            Rectangle rc = m_d3DFont.MeasureString(null, m_timeCodeString, Direct3D.DrawTextFormat.Center, Color.Black);
            int x = (m_d3DDevice.Viewport.Width / 2) - (rc.Width / 2);
            int y = 10;
            m_d3DFont.DrawText(null, m_timeCodeString, x + 1, y + 1, Color.Black);
            m_d3DFont.DrawText(null, m_timeCodeString, x, y, Color.White);
           
            m_d3DDevice.EndScene();
            m_d3DDevice.Present();
            
        }

        void SetTimecode(IDeckLinkVideoFrame videoFrame)
        {
            IDeckLinkTimecode timecode;

            m_timeCodeString = "00:00:00:00";

            videoFrame.GetTimecode(_BMDTimecodeFormat.bmdTimecodeRP188Any, out timecode);

            if (timecode != null)
                timecode.GetString(out m_timeCodeString);
        }

        void IDeckLinkScreenPreviewCallback.DrawFrame(IDeckLinkVideoFrame theFrame)
        {
            // First, pass the frame to the DeckLink screen preview helper
            m_previewHelper.SetFrame(theFrame);
            SetTimecode(theFrame);

            // Then draw the frame to the scene
            Render();
           
            System.Runtime.InteropServices.Marshal.ReleaseComObject(theFrame);
        }

      

    }
}
