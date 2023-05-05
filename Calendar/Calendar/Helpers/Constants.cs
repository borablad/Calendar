
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
        public static string HostUrl = "pre-prod-crm.astana-motors.kz/pre-production/0/ServiceModel/BnzIntegrationService.svc/Call/";

        public static string Scheme = " https"; 
        public static string Port = "443";
       
        public static string RestUrl = $"{Scheme}://{HostUrl}:{Port}/{{0}}";


        public static string GetEvents = $"getRoomReservations";
      



    }
}
