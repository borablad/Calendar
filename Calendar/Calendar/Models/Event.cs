using Newtonsoft.Json;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Transactions;

namespace Calendar.Models
{
    public class Event
    {
      
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        /*        [JsonProperty("RoomCode")]
        public string RoomCode { get; set; }
       
        [JsonProperty("RoomName")]
        public string RoomName { get; set; }
        [JsonProperty("Topic")] 
        public string Topic { get; set; }*/
        [JsonProperty("OwnerName")] 
        public string OwnerName { get; set; }

        [JsonProperty("StatusCode")]
        public string StatusCodeRaw { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedName { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }   


        [JsonIgnore]
        public StatusCode StatusCode {
            get { return Enum.TryParse(StatusCodeRaw, out StatusCode result) ? result : StatusCode.NotStarted; }
            set { StatusCodeRaw = value.ToString(); }
        }


        [JsonIgnore]
        public string StatusCodeInXaml { 
            get
            {
            
                 switch (StatusCode)
                {
                    case StatusCode.NotStarted:
                        return "Не начата";
                    case StatusCode.Agreed:
                        return "Согласована";
                    case StatusCode.Canceled:
                        return "Отменена";
                    case StatusCode.Done:
                        return "Выполнена";
                    case StatusCode.InProgress:
                        return "В работе";
                    case StatusCode.Verificated:
                        return "Верифицирована";
                    case StatusCode.OnVisa:
                        return "На визировании";
                    case StatusCode.OnVerification:
                        return "На верификации";
                    case StatusCode.Finished:
                        return "Завершена";
                    default:
                        return "Unknown";
                     

                }
            } 
        }

        /*public bool IsMeeting 
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
*/
       

        
    }
}
