using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRModel;
using SIRModel.Online;
using SIRModel.Eventing;

namespace SIR_Online
{
    public enum OnlineEventType
    {
        Diver_off_Deck,
        Diver_left_Surface,
        Diver_at_Bottom,
        Diver_at_Work_Site


    }
    public static class OnlineEventing
    {
        //EventModelv1 ev = new EventModelv1();
        //ev.EventName = "Recording Output Started";ev.EventName = "Recording Output Started";
        //        ev.isAnomaly = false;
        //        EventLog vidstartedlog = new EventLog();
        //vidstartedlog.evented = ev;
        //        vidstartedlog.loggedTime = DateTime.Now;
        //        SIROnlineSession.Instance.EventLogs.Add(vidstartedlog);
        //        logEventDGV.Rows.Add(vidstartedlog.loggedTime.ToString(), vidstartedlog.evented.EventName, vidstartedlog.evented.isAnomaly, vidstartedlog.evented.Description);
        //        logEventDGV.Refresh();

        public static EventLog CreateNewOnlineEventLog(OnlineEventType _type)
        {
            EventModelv1 ev = new EventModelv1();
            string onlineeventTypename = _type.ToString();
            onlineeventTypename=onlineeventTypename.Replace("_", " ");
            ev.EventName = onlineeventTypename;
            ev.isAnomaly = false;
            EventLog loggedEvent = new EventLog();
            loggedEvent.evented = ev;
            loggedEvent.loggedTime = DateTime.Now;
            loggedEvent.DataString = new Dictionary<string, string>();
            foreach (var item in SurveyData.NavConfigModel.CommaFieldIndexes.Keys)
            {
                loggedEvent.DataString.Add(item, SurveyData.GetStringFromDictKey(item));
            }
            //loggedEvent.DataString

            return loggedEvent;

        }


    }
}
