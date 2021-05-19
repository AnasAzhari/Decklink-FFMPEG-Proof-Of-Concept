using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SIRModel;
using SIRModel.Eventing;
using SIR_Online;




namespace SIR_Online
{
    public static class SaveAndLoadJson
    {
       
        public static void SyncEventLogFromEventing()
        {
            string filepath = SIROnlineSession.Instance.GetEventingSavePathLocation();
            if (File.Exists(filepath))
            {
                
                string json = File.ReadAllText(filepath);
                List<EventLog> evlog = JsonConvert.DeserializeObject<List<EventLog>>(json);


                foreach (EventLog evl in evlog)
                {
                    if(!SIROnlineSession.Instance.EventLogs.Any(item =>item.evented.EventName==evl.evented.EventName && item.loggedTime == evl.loggedTime))
                    {
                        SIROnlineSession.Instance.EventLogs.Add(evl);
                    }
                 
                    //if (!SIROnlineSession.Instance.EventLogs.Contains(evl))
                    //{
                    //    SIROnlineSession.Instance.EventLogs.Add(evl);

                    //}

                }
            }

            
        }


     
    }
    

}
