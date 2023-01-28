using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Calendar.Helpers.REST;
using Calendar;
using Newtonsoft.Json;


namespace Calendar.Servisec
{
    public class MockDataStore : Rest
    {
        
        public async Task<List<Event>> GetEvents()
        {
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
    }
}
