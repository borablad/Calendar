
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class Constants
    {
        // Настройки сервера
        public static string HostUrl = "10.80.0.164";

        public static string Scheme = "http"; 
        public static string Port = "8000";
       
        public static string RestUrl = $"{Scheme}://{HostUrl}:{Port}/{{0}}";
        public static string GetEvents = $"Events";
      



    }
}
