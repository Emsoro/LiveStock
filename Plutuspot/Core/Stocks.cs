//using Newtonsoft.Json;
using Plutuspot.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;


namespace Plutuspot.Core
{
    public class Stocks
    {
        public Stocks() { }

        public StockList StocksList = new StockList();
        public StockInfo StocksInfo = new StockInfo();

        public ObservableCollection<string> StockNames { get; set; }

        public async Task<string> GetAllStocksList()
        {
            try
            {
                var request = new RestRequest("/", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""appId"":""appId01"",""globalId"":""786e4c21-70dc-435a-93bb-38"",""marketType"":"""",""pageNo"":1,""pageSize"":100}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var client = new RestClient("https://emappdata.eastmoney.com/stockrank/getAllCurrentList");
                RestResponse response;
            
                response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string responseBody = response.Content;
                    // 处理响应内容，例如更新UI
                    if (responseBody.Contains("OK"))
                    {
                        var msg = "GetAllStocksList OK";
                        var options = new JsonSerializerOptions()
                        {
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                        };
                        StocksList = JsonSerializer.Deserialize<StockList>(responseBody, options);

                        //StocksList = JsonSerializer.Deserialize<StockList>(responseBody, StockListContext.Default.StockList);
                        return msg;
                    }
                    else
                    {

                    }

                }
                else
                {
                    var msg = $"请求失败，状态码: {response.StatusCode}";
                    return msg;
                }
            }
            catch (Exception ex)
            {
                var msg = $"请求失败: {ex.Message}";
                return msg;
            }

            return "";

        }

        public async Task<StockInfo> GetAllStocksPriceAndNames()
        {
            try
            {
                var request = new RestRequest("/", Method.Get);
                request.AddHeader("Content-Type", "application/json; charset=UTF-8");
                var stockCodes = StocksList.data.Select(x => x.sc).ToList();
                var body = string.Join(",", stockCodes).Replace("SH","1.").Replace("SZ", "0.");

                var client = new RestClient($@"https://push2.eastmoney.com/api/qt/ulist.np/get?ut=f057cbcbce2a86e2866ab8877db1d059&fltt=2&invt=2&fields=f14%2Cf148%2Cf3%2Cf12%2Cf2%2Cf13%2Cf29&secids={body},&");
                RestResponse response;
            
                response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string responseBody = response.Content;
                    // 处理响应内容，例如更新UI
                    if (responseBody.Contains("total"))
                    {
                        Debug.WriteLine("GetAllStocksPriceAndNames OK");
                        var options = new JsonSerializerOptions()
                        {
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                        };
                        StocksInfo = JsonSerializer.Deserialize<StockInfo>(responseBody, options);
                        //StocksInfo = JsonSerializer.Deserialize<StockInfo>(responseBody, StockInfoContext.Default.StockInfo);
                        return StocksInfo;
                    }
                    else
                    {
                        Trace.WriteLine("GetAllStocksPriceAndNames Error");
                    }

                }
                else
                {
                    var msg = $"请求失败，状态码: {response.StatusCode}";
                    Trace.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                var msg = $"请求失败: {ex.Message}";
                Trace.WriteLine(msg);
            }

            return null;

        }

    }
}
