using Newtonsoft.Json;
using StockDemo.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace StockDemo.ViewModel
{
    public class StockViewModel
    {
        public QuoteData Data { get; set; } = null;
        public bool IsBusy { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public async Task<bool> GetQuote(string ticker)
        {
            if (IsBusy) { return false; };

            try
            {
                IsBusy = true;
                var url = "https://motzstocks2.azurewebsites.net/api/"
                    + $"HttpTriggerSharp1?ticker={ticker}";

                // https://www.codeproject.com/articles/37550/stock-quote-and-chart-from-yahoo-in-c
                //string yahooUrl = @"http://download.finance.yahoo.com/d/quotes.csv?s=" +
                //                  ticker + "&f=sl1d1t1c1hgvbap2";
                //string[] symbols = symbol.Replace(",", " ").Split(' ');
                //url = yahooUrl;
                // https://www.codeproject.com/articles/618516/stock-quotes-and-charts-from-google-finance-using
                //ticker = ticker.Trim();
                //ticker = ticker.Replace(" ", "&stock=");
                //ticker = ticker.Replace(",", "&stock=");
                //var url = "https://www.google.com/ig/api?stock=" + (ticker);
                // https://www.quantshare.com/sa-426-6-ways-to-download-free-intraday-and-tick-data-for-the-us-stock-market
                // https://www.quantshare.com/sa-43-10-ways-to-download-historical-stock-quotes-data-for-free

                var client = new HttpClient();
                var json = await client.GetStringAsync(url);

                Data = JsonConvert.DeserializeObject<QuoteData>(json);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Data = null;
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
