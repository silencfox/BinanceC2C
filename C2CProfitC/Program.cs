namespace C2CProfitC
{

    using System;
    using System.Threading.Tasks;
    using Binance.Spot;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Binance.Common;
    using Binance.Spot;
    using Binance.Spot.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Linq;

    //dotnet add package Binance.Spot
    //dotnet add package Microsoft.Extensions.Logging
    //dotnet add package Microsoft.Extensions.Logging.Console
    // installn Newtonsoft.Json

    class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Prueba C2C Binance");

            calc(args);

        }

        public static void calc(string[] args)
        {
            var data = Binance(args);

            //Environment.Exit(0);

            List<C2CHistory> C2Ch = new List<C2CHistory>();
            
            for (int i = 0; i < data.Result.Count; i++)
            {

                double nuorder = System.Convert.ToDouble(data.Result[i].unitPrice);
                C2Ch.Add(new C2CHistory(data.Result[i].orderNumber.ToString(), data.Result[i].tradeType.ToString(), data.Result[i].asset.ToString(), data.Result[i].fiat.ToString(), data.Result[i].fiatSymbol.ToString(), System.Convert.ToDouble(data.Result[i].amount), System.Convert.ToDouble(data.Result[i].totalPrice), System.Convert.ToDouble(data.Result[i].unitPrice), data.Result[i].orderStatus.ToString(), System.Convert.ToDouble(data.Result[i].commission), data.Result[i].counterPartNickName.ToString(), data.Result[i].advertisementRole.ToString()));

            }

            var C2CBuy = C2Ch.Where(p => p.orderStatus == "COMPLETED" && p.TradeType == "BUY" && p.fiat == "DOP")
            .OrderBy(p => p.orderStatus)
            .ThenBy(p => p.TradeType);

            var TotalCompras = C2CBuy.Sum(e => e.totalPrice);
            var CantidadCompras = C2CBuy.Sum(e => e.amount);
            var PriceCompra = C2CBuy.Sum(e => e.unitPrice);
            var FeesBankCompras = C2CBuy.Sum(e => (e.amount * 0.0015));

            Console.WriteLine("total monto comprado: " + Math.Round(TotalCompras, 2) + ", Cantidad Compras: " + Math.Round(CantidadCompras, 2) + ", FeesBank: " + Math.Round(FeesBankCompras, 2) + ", Price: " + Math.Round(PriceCompra, 2));

            foreach (var C2C in C2CBuy)
            {
                Console.WriteLine("Order #: {0},{1},{2},{3},{4},{5},{6}", C2C.OrderNumber, C2C.TradeType, C2C.Asset, C2C.Amount, C2C.TotalPrice, C2C.UnitPrice, C2C.orderStatus);
            }

        }

        public static async Task<dynamic> Binance(string[] args)
        {

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: logger);
            //HttpClient httpClient = new HttpClient(handler: loggingHandler);
            HttpClient httpClient = new HttpClient();

            string apiKey = "FXkpHdw9HldvNtFmPe6iqsdTByhvWUU8UuHxCvzKuDhDewaMIejNbeVITsQptPVT";
            string apiSecret = "25egnaFePsYeb6AbXyAf3MiHmdGGHZdxU7h0WpMOPAkyuBLWdKHGephXz6KKWPwg";

            var c2c = new C2C(httpClient, apiKey: apiKey, apiSecret: apiSecret);



            var strBuy = await c2c.GetC2cTradeHistory(Side.BUY);
            var strSell = await c2c.GetC2cTradeHistory(Side.SELL);

            dynamic rawBuy = JsonConvert.DeserializeObject<dynamic>(strBuy);
            dynamic rawSell = JsonConvert.DeserializeObject<dynamic>(strSell);
            dynamic jsBuy = rawBuy.data;
            dynamic jsSell = rawSell.data;

            jsBuy.Merge(jsSell, new JsonMergeSettings
            {
                // union array values together to avoid duplicates
                MergeArrayHandling = MergeArrayHandling.Union
            });

            dynamic c2cresult = jsBuy;

            return c2cresult;
        }







    }
}