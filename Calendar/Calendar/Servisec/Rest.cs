using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Servisec
{
   public interface Rest
    {

        public  Task<List<Event>> GetEvents();
    }
}
