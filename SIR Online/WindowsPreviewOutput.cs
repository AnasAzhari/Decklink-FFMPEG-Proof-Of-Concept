using System;
using System.Drawing;
using System.Windows.Forms;
using DeckLinkAPI;
using Direct3D = Microsoft.DirectX.Direct3D;


namespace SIR_Online
{
    public partial class WindowsPreviewOutput : Control, IDeckLinkScreenPreviewCallback
    {
        private IDeckLinkDX9ScreenPreviewHelper m_previewHelper;

        private Direct3D.Device m_d3DDevice;
        Point clickedPosition;
        bool isDrawing;
        public bool IsDrawing
        {
            get
            {
                return isDrawing;
            }
            set
            {
                bool oldValue = isDrawing;
                if (value != oldValue)
                {
                    isDrawing = value;
                }
            }
        }
        public WindowsPreviewOutput()
        {
            m_previewHelper = new CDeckLinkDX9ScreenPreviewHelper();
            InitializeComponent();
        }
        public void InitD3D()
        {
            // var d3dpp = new Direct3D.PresentParameters();
            // d3dpp.BackBufferFormat = Direct3D.Format.Unknown;
            var d3dpp = new Direct3D.PresentParameters();
            d3dpp.BackBufferFormat = Direct3D.Format.Unknown;
            d3dpp.BackBufferCount = 2;
            d3dpp.Windowed = true;
            d3dpp.SwapEffect = Direct3D.SwapEffect.Discard;
            d3dpp.DeviceWindow = this;
            d3dpp.PresentationInterval = Direct3D.PresentInterval.Default;

            m_d3DDevice = new Direct3D.Device(0, Direct3D.DeviceType.Hardware, this, Direct3D.CreateFlags.HardwareVertexProcessing | Direct3D.CreateFlags.MultiThreaded, d3dpp);
            
            unsafe
            {
                m_previewHelper.Initialize(new IntPtr(m_d3DDevice.UnmanagedComPointer));
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        void Render()
        {
            try
            {
                m_d3DDevice.BeginScene();
                tagRECT rect;
                rect.top = m_d3DDevice.Viewport.Y;
                rect.left = m_d3DDevice.Viewport.X;
                rect.bottom = m_d3DDevice.Viewport.Y + m_d3DDevice.Viewport.Height;
                rect.right = m_d3DDevice.Viewport.X + m_d3DDevice.Viewport.Width;

                m_previewHelper.Render(rect);

                m_d3DDevice.EndScene();
                m_d3DDevice.Present();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
          
        }

        void IDeckLinkScreenPreviewCallback.DrawFrame(IDeckLinkVideoFrame theFrame)
        {
            if (theFrame != null)
            {
                // First, pass the frame to the DeckLink screen preview helper
                m_previewHelper.SetFrame(theFrame);

                // Then draw the frame to the scene
                Render();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(theFrame);
            }
        }
        public Point center { get; protected set; }
        public bool isClicked;

        private void WindowsPreviewOutput_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left&&MarkingToolOperation.markingTools!=null)
            //{
            //    center = e.Location;
            //    //Console.WriteLine(" x position :" + e.X.ToString() + ", y position :" + e.Y.ToString());
            //}
        }
        public Point EndLocation { get; protected set; }

        private void WindowsPreviewOutput_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MarkingToolOperation.markingTools!=MarkingTools.None)
            {
                //MarkingToolOperation.isDrawing = true;
                IsDrawing = true;
                EndLocation = e.Location;   
            }
        }

        private void WindowsPreviewOutput_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsDrawing = false;
                MarkingToolOperation.markingTools = MarkingTools.None;
            }


        }

        private void WindowsPreviewOutput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MarkingToolOperation.markingTools != null)
            {
                center = e.Location;
                Console.WriteLine(" x position :" + e.X.ToString() + ", y position :" + e.Y.ToString());
            }
        }
    }

}
