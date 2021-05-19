using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SIR_Online.Model
{
    public class OverlayModel
    {
        List<OverlayText> overlayTexts;
        List<OverlayTextGroup> overlayTextGroups;


        public OverlayModel()
        {
            overlayTexts = new List<OverlayText>();
            overlayTextGroups = new List<OverlayTextGroup>();
            
        }
    }

    // position of text according to screen
    public class OverlayText
    {
        Label label;
        Font textFont;
        Rectangle textBound;
        Point textLocation;
        string text;
        
        public OverlayText(Label _label)
        {
            label = _label;
            if (label != null)
            {
                textFont = label.Font;
                textBound = label.Bounds;
                textLocation = label.Location;
            }         
        }
    }

    public class OverlayTextGroup
    {

    }
}
