using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIR_Online
{
    public enum ScreenSize
    {
        NTSC,
        PAL,
        SD,
        HD1080,
        HD2K,
        HD4K
    }
    public enum VideoCodec
    {      
        h264,
        mpeg4,
        hevc,
        h264_cuvid,
    }
    public enum AudioCodec
    {
      
        pcm_s16be,
        pcm_s32be
    }
    public enum AudioFormat
    {
        s16le,
        s32le
    }
    public enum AudioBitRate
    {
        ABR900000,
        ABR1000000,
        ABR1500000

    }
    public enum HardwareAccelarations
    {
        cuda,
        cuvid
    }
    public enum FPS
    {
        FPS24,
        FPS25,
        FPS30,
        FPS50,
        FPS60
    }
    public enum Formats
    {
        rawvideo,
        matroska,
        image2pipe,
        h264

    }
    public  enum Rate
    {

    }
    
    public class VideoAudioEnum
    {


    }
}
