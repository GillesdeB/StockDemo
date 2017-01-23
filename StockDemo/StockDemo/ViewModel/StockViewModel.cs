using Newtonsoft.Json;
using StockDemo.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StockDemo.ViewModel
{
    public class StockViewModel
    {
        public QuoteData data { get; set; } = null;
        public bool IsBusy { get; set; } = false;
        public async Task<bool> GetQuote(string ticker)
        {
            if (IsBusy) { return false; };

            try
            {
                IsBusy = true;
                var url = "https://motzstocks2.azurewebsites.net/api/"
                    + $"HttpTriggerSharp1?ticker={ticker}";

                var client = new System.Net.Http.HttpClient();
                var json = await client.GetStringAsync(url);

                var Data = JsonConvert.DeserializeObject<QuoteData>(json);
            }
            catch (Exception ex)
            {
                data = null;
                return false;
            }
            finally
            {
                IsBusy = false;
            }
            return true;
        }
    }
}
