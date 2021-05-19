using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.FFMPEG;

namespace SIR_Online
{
    public class RecordingSettings
    {
        int _cutoffdir;
       public int CuttOffDuration
        {
            get { return _cutoffdir; }
            set
            {
                _cutoffdir = value;
                RecordingSettingData.CutOffDur = _cutoffdir;
            }
                
        }

       

        public static int[] cutOffOptions = new int[6] { 1,2,15, 30, 45, 60 };

        public RecordingSettings()
        {
            CuttOffDuration = cutOffOptions[0];

        }

    }
    public static class RecordingSettingData
    {
        public static int  CutOffDur;



    }

}
