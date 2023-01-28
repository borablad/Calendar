using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models
{
    public class AllTimeModel
    {
        public DateTime Hour { get; set; } = DateTime.Today;

        public Event Events { get; set; } 
        public bool IsStart { get; set; }
        public bool IsStartMiddle { get; set; }
        public bool IsMiddle { get; set; }
        public bool IsEndMiddle { get; set; }
        public bool IsEnd { get; set; }
    }
}
