using Newtonsoft.Json;
using StockDemo.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Net.Http;

namespace StockDemo.ViewModel
{
    public class StockViewModel
    {
        public QuoteData data { get; set; } = null;
        public bool IsBusy { get; set; } = false;
        public async Task<bool> GetQuote(string ticker)
        {
            if (IsBusy) { return false};

            try
            {
                IsBusy = true;
                var url = "https://motzstocks2.azurewebsites.net/api/"
                    + $"HttpTriggerSharp1?ticker={ticker}";

                var client = new HttpClient();
                var json = await client.GetStringAsync(url);

                Data = JsonConvert.DeserializeObject<QuoteData>(json);
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
