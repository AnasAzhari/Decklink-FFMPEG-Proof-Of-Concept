using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIR_Online.FFMPEG
{
    public class Arguments
    {
       string args { get; set; }
       //public Arguments(InputArguments _inputArgs,OutputArguments _outputArgs)
       // {
       //     args = _inputArgs + " " + _outputArgs;
       // }
        public Arguments(params string[] _string)
        {
            
                for (int i = 0; i < _string.Length; i++)
                {
                    args +=_string[i]+ " " ;
                }
            
        }

        public override string ToString()
        {
            return args;
        }
    }

    public static class InputArgumentOptions
    {
        public static string GetInputOptionStr(string pipeName)
        {
            string input = "-i ";
            string pipe= string.Format(@"""\\.\pipe\{0}""",pipeName);
            input += pipe;
            return input;

        }
        public static  string GetFormatOptionStr(Formats _fmt)
        {
            string f = "-f ";
            string opt;
            switch (_fmt)
            {
                case Formats.rawvideo:
                    opt = Formats.rawvideo.ToString();
                    break;
                case Formats.matroska:
                    opt = Formats.rawvideo.ToString();
                    break;
                case Formats.image2pipe:
                    opt = Formats.image2pipe.ToString();
                    break;
                case Formats.h264:
                    opt = Formats.h264.ToString();
                    break;
                default:
                    opt = "";
                    break;
            }
            f += opt;
            return f;
        }
        public static string GetMaxBitRateStr(int _rate)
        {
            string MaxRate = "-maxrate ";
            string opt = _rate.ToString();
            MaxRate += opt + "k";
            return MaxRate;
        }

        public static  string GetFPSOptionStr(FPS _fps)
        {
            string MaxRate = "-r ";
            string opt = _fps.ToString().Remove(0,3);
            MaxRate += opt;
            return MaxRate;
        }
        public static string GetVariableFPSOptionStr(int _fps)
        {
            string MaxRate = "-r ";
            string opt = _fps.ToString();
            MaxRate += opt ;

            return MaxRate;
        }
        public static string GetVideoSizeOptionStr(int _width, int _height)
        {
            string Size = "-s ";
            string opt = string.Format("{0}x{1}", _width.ToString(), _height.ToString());
            Size += opt;
            return Size;
        }

        public static  string GetAudioEnableOptionStr(bool _isEnable)
        {
            string Au = "-an ";
            if (_isEnable == true)
            {
                Au += "-y";
            }
            else
            {
                Au += "-n";
            }
            return Au;
        }

        public static string GetHwAccelsOptionStr(HardwareAccelarations _hw)
        {
            string hwaccel = "-hwaccel ";
            string opt = _hw.ToString();
            hwaccel += opt;
            return hwaccel;

        }

        //public static string GetAudioBitRateOptionStr(AudioBitRate _abr)
        //{


        //}
        public static string GetVideoCodecOptionStr(VideoCodec _videoCodec)
        {
            string codec = "-codec ";
            codec += _videoCodec.ToString();
            return codec;
        }
    

        public static class OutputArgumentOptions
        {
            public static string GetOuputFilename(string _outputFile)
            {
                return _outputFile;
            }
            
            

        }





       
        
    }
}
