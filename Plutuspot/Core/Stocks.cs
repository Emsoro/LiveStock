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
        public StockNews StockNews = new StockNews();
        public HotTopics HotTopics = new HotTopics();

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

        public async Task<string> GetHotTopics()
        {
            try
            {
                var request = new RestRequest("/", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""url"":""newctopic/api/Topic/HomeTopicRead?deviceid=IPHONE&version=10001000&product=Guba&plat=Iphone&p=1&ps=20&needPkPost=true"",""type"":""get"",""parm"":""""}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var client = new RestClient("https://emcreative.eastmoney.com/FortuneApi/GuBaApi/common");
                RestResponse response;

                response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string responseBody = response.Content;
                    // 处理响应内容，例如更新UI
                    if (responseBody.Contains("操作成功"))
                    {
                        var msg = "GetHotTopics OK";
                        var options = new JsonSerializerOptions()
                        {
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                        };
                        HotTopics = JsonSerializer.Deserialize<HotTopics>(responseBody, options);
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

        public async Task<String> GetNews()
        {
            try
            {
                var request = new RestRequest("/", Method.Get);
                request.AddHeader("Content-Type", "application/json; charset=UTF-8");
                DateTime time = DateTime.Now;
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//当地时区
                TimeSpan ts = time - startTime;
                var timestamp = Convert.ToInt64(ts.TotalSeconds);
                var client = new RestClient($@"https://np-listapi.eastmoney.com/sec/hotNews?biz=sec_hotnews&client=sec_all&needInteractData=0&req_trace=11&size=100&v=028095{timestamp}");
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
                        StockNews = JsonSerializer.Deserialize<StockNews>(responseBody, options);
                        return "OK";
                    }
                    else
                    {
                        Trace.WriteLine("GetNews Error");
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
