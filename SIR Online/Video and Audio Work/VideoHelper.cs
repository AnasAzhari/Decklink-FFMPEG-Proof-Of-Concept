using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIR_Online
{

    public static  class VideoHelper
    {
        public static string GetFFMPEGScreenSizeArgs(ScreenSize _panel)
        {
            switch (_panel)
            {
                case ScreenSize.NTSC:
                    return "720x486";
                case ScreenSize.PAL:
                    return "720x576";
                case ScreenSize.HD1080:
                    return "1920x1080";                  
                default:
                    return "";
                    break;
            }
        }




    }
}
