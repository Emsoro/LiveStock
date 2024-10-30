using Avalonia.Threading;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Win32;

namespace LiveStock.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        DispatcherTimer dispatcherTimer;
        private string url = "https://qt.gtimg.cn/q=s_";
        private string prefix = "sh";

        public MainWindowViewModel() {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);

            dispatcherTimer.Tick += DispatcherTimer_Tick;

            StockCode = ReadRegister();
        }

        private async void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
      
            var request = new RestRequest("/", Method.Get);
            var options = new RestClientOptions($"{url}{string.Join(",",_fullStockCode)}")
            {
                MaxTimeout = 1000
            };
            var client = new RestClient(options);
            client.AddDefaultHeader("Content-Type", "text/html;charset=utf-8");

            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string responseBody = response.Content;
                    // 处理响应内容，例如更新UI
                    if (responseBody.Contains("v_pv_none_match"))
                    {
                        StockPrice = "no match";
                    }
                    else
                    {
                        var results = responseBody.Split(";");
                        List<string> prices = new List<string>();
                        foreach(var result in results)
                        {
                            if (result == "\n") continue;
                            var items = result.Split(new char[] { '=', '~' });
                            prices.Add($"{items[2]} {items[4]} {items[5]} {items[6]}%");
                        }
              
                        StockPrice = string.Join('\n',prices);
                    }
                }
                else
                {
                    StockPrice = $"请求失败，状态码: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                StockPrice = $"请求失败: {ex.Message}";
            }
        }

        [ObservableProperty]
        private string  windowSystemDecorations = "Normal";
        [ObservableProperty]
        private string chromeHints = "Default";

        [ObservableProperty]
        private bool displayState = true;

        [ObservableProperty]
        private int windowHeight = 130;
        [ObservableProperty]
        private int titleHeight = 40;
        

        [ObservableProperty]
        private string stockCode;

        [ObservableProperty]
        private string stockPrice;

        private List<string> _fullStockCode = new List<string>();

        private string StockName;

        [RelayCommand]
        public void SetTitleDisplay()
        {
            if (displayState == true)
            {
                DisplayState = false;
                WindowSystemDecorations = "None";
                ChromeHints = "BorderOnly";
                WindowHeight = 60;
                TitleHeight = 0;
            }
            else
            {
                DisplayState = true;
                WindowSystemDecorations = "Normal";
                ChromeHints = "Default";
                WindowHeight = 130;
                TitleHeight = 40;
            }

        }

        [RelayCommand]
        public void SetStockCode()
        {
            dispatcherTimer.Stop();
            _fullStockCode.Clear();
            WriteRegister(stockCode);
            var stockCodes = stockCode.Split(' ');
            foreach ( var scode in stockCodes )
            {
                if (scode.Length < 6 && int.TryParse(scode, out _))
                {
                    prefix = "hk";
                }
                else
                {
                    switch (scode.Substring(0, 1))
                    {
                        case "6":
                            prefix = "sh";
                            break;
                        case "4":
                        case "8":
                            prefix = "bj";
                            break;
                        case "0":
                        case "2":
                        case "3":
                            prefix = "sz";
                            break;
                        default:
                            prefix = "us";
                            break;
                    }
                }

                _fullStockCode.Add($"{prefix}{scode}");
            }
            
            dispatcherTimer.Start();
        }

        private string ReadRegister()
        {
            try
            {
                var valueName = @"Software\LiveStock";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(valueName))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("StockCode");
                        return value != null ? value.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading registry value: " + ex.Message);
            }
            
            return string.Empty;
        }

        private void WriteRegister(string value)
        {
            try
            {
                var valueName = @"Software\LiveStock";
                RegistryKey key = Registry.CurrentUser.CreateSubKey(valueName);
                if (key != null)
                {
                    key.SetValue(@"StockCode", value);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing registry value: " + ex.Message);
            }
        }
    }
}
