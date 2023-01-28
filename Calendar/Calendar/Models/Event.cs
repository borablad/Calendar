using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models
{
    public class Event
    {
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        [JsonProperty("RoomCode")]
        public string RoomCode { get; set; }
       
        [JsonProperty("RoomName")]
        public string RoomName { get; set; }
        [JsonProperty("Topic")] 
        public string Topic { get; set; }
        [JsonProperty("Owner")] 
        public string Owner { get; set; }

        

        public bool IsMeeting 
        { 
            get 
            {
                if(Topic is "Совещание") 
                    return true;
                else
                    return false;
            }  
        }
        public bool IsMeetUp
        {
            get
            {
                if (Topic is "MeetUp")
                    return true;
                else
                    return false;
            }
        }
        public bool IsUnknown
        {
            get
            {
                if (!IsMeetUp && !IsMeeting)
                    return true;
                else
                    return false;
            }
        }

        public Event()
        {

        }

        
    }
}
