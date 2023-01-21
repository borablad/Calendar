using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models
{
    public class Event
    {
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
       
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public string Topic { get; set; }
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
