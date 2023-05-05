using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Calendar.Helpers.REST;
using Calendar;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Calendar.Servisec
{
    public class MockDataStore : Rest
    {
        
        public async Task<List<Event>> GetEvents()
        {
            GetRooms();
            var result = new List<Event>();
            try
            {


                var response = await new RequestServiceREST().Get(Constants.GetEvents);
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject< List<Event>> (await response.Content.ReadAsStringAsync());
                  


                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    throw new Exception(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<List<Room>> GetRooms()
        {
            var result = new List<Room>();

            string inRes = "{\r\n    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n    \"data\": [\r\n        {\r\n            \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n            \"RoomCode\": \"one\",\r\n            \"RoomName\": \"Комната 1\",\r\n            \"RoomLocation\": \"Здание 1\",\r\n            \"Reservations\": [\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662670800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662674400000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"asdfasdf\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662604200000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662607800000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Супермитинг2\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662611400000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662624000000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Test Tsi Sotr\",\r\n                    \"CreatedBy\": \"Test Tsi Sotr\",\r\n                    \"Title\": \" митинг1\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662597000000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662600600000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Супермитинг1\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662490800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662505200000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Митинг 1\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662525000000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662530400000+0600)/\",\r\n                    \"StatusCode\": \"Finished\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Митинг 3\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662625800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662640200000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Test Tsi Sotr\",\r\n                    \"CreatedBy\": \"Test Tsi Sotr\",\r\n                    \"Title\": \" митинг2\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1664503200000+0600)/\",\r\n                    \"EndDate\": \"/Date(1664510400000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"asdfasdf\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1663444800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1663455600000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"asdfasdf\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1663462800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1663477200000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"asdfasdf\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1664485200000+0600)/\",\r\n                    \"EndDate\": \"/Date(1664492400000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"asdfasdf\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662660000000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662663600000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"sdfgsdfg\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n            \"RoomCode\": \"two\",\r\n            \"RoomName\": \"Комната 2\",\r\n            \"RoomLocation\": \"Здание 1\",\r\n            \"Reservations\": [\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662620400000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662634800000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Test Tsi Sotr\",\r\n                    \"CreatedBy\": \"Test Tsi Sotr\",\r\n                    \"Title\": \"Митинг 3\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662600600000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662613200000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Test Tsi Sotr\",\r\n                    \"CreatedBy\": \"Test Tsi Sotr\",\r\n                    \"Title\": \"Митинг4\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662492600000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662508800000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Митинг 2\"\r\n                },\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1662521400000+0600)/\",\r\n                    \"EndDate\": \"/Date(1662528600000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Tsisupervisor\",\r\n                    \"CreatedBy\": \"Tsisupervisor\",\r\n                    \"Title\": \"Митинг 4\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n            \"RoomCode\": \"аква\",\r\n            \"RoomName\": \"аквариум\",\r\n            \"RoomLocation\": \"Здание тест\",\r\n            \"Reservations\": [\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1667278800000+0600)/\",\r\n                    \"EndDate\": \"/Date(1667286000000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Testtesttestovich\",\r\n                    \"CreatedBy\": \"Вячеслав Медляковский\",\r\n                    \"Title\": \"11111\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n            \"RoomCode\": \"серп\",\r\n            \"RoomName\": \"серпентарий\",\r\n            \"RoomLocation\": \"Здание тест\",\r\n            \"Reservations\": [\r\n                {\r\n                    \"__type\": \"BaseDTO:#Banza.Integration.Model\",\r\n                    \"StartDate\": \"/Date(1667271600000+0600)/\",\r\n                    \"EndDate\": \"/Date(1667278800000+0600)/\",\r\n                    \"StatusCode\": \"NotStarted\",\r\n                    \"OwnerName\": \"Testtesttestovich\",\r\n                    \"CreatedBy\": \"Вячеслав Медляковский\",\r\n                    \"Title\": \"222\"\r\n                }\r\n            ]\r\n        }\r\n    ]\r\n}";
            var obj = JObject.FromObject(JObject.Parse(inRes));
            try
            {
                var str = obj["data"].ToString();
                var list = JsonConvert.DeserializeObject<List<Room>>(str) ;


            }
            catch (Exception ex)
            {
                var i = 0;
            }
          /*  try
            {


                var response = await new RequestServiceREST().Get(Constants.GetEvents);
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<Room>>(await response.Content.ReadAsStringAsync());



                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    throw new Exception(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }*/


            return result;
        }
    }
}
