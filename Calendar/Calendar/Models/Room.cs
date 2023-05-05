using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models
{
    public class Room
    {
     /*   private static int _id; 
        [JsonIgnore]
        public int Id;
        public Room() {
            _id++;
            Id = _id;
        }   */
      

        [JsonProperty("RoomCode")]
        public string RoomCode { get; set; }
        
        [JsonProperty("RoomName")]
        public string RoomName { get; set; }

        [JsonProperty("RoomLocation")]
        public string RoomLocation { get; set; }

        [JsonProperty("Reservations")]
        public List<Event> Events { get; set; }
    }
}
