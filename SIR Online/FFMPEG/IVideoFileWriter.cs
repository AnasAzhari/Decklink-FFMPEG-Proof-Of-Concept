using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SIR_Online.FFMPEG
{
    interface IVideoFileWriter
    {
        bool IsOpen { get; set; }
        void Open(string _devicename,string _ffmpegLoc,int width,int height,int _bitrate, int _fps, string _OutputFileName);
        void Close();
        void WriteVideoFrame(Bitmap _bmp);
        void WriteAudioFrame(byte[] _audioBuffer);
    }
}
