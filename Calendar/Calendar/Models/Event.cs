using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models
{
    public class Event
    {
        public DateTime StartDate 
        { 
            get => StartDate; 
            set 
            { 
                try 
                {
                    StartDate = Convert.ToDateTime(value);
                }
                catch
                {
                    StartDate = DateTime.MinValue;
                }
            }
        }
        public DateTime EndDate 
        { 
            get => EndDate; 
            set
            {
                try
                {
                    EndDate = Convert.ToDateTime(value);
                }
                catch
                {
                    EndDate = DateTime.MinValue;
                }
            }
        }
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
                if (Topic is "IsMeetUp")
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
    }
}
