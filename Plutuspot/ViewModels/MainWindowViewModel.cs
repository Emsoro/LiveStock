using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plutuspot.Core;
using Plutuspot.Models;
using System.Collections.ObjectModel;

namespace Plutuspot.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        [ObservableProperty]
        private string msg = "";

        public Stocks stocks = new Stocks();
        public int index = 0;
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string> { "热门股票", "热门板块" };
        public ObservableCollection<Diff> StockItems { get; set; } = new ObservableCollection<Diff>();

        public MainWindowViewModel()
        {
            Init();
        }

        [RelayCommand]
        public void Loaded()
        {
            Init();
        }

        public async void Init()
        {

            if (stocks.StocksList.data.Count == 0)
            {
                Msg += await stocks.GetAllStocksList();
                Msg += await stocks.GetAllStocksPriceAndNames();
                foreach (var stock in stocks.StocksInfo.data.diff)
                {
                    index++;
                    stock.index = index;
                    StockItems.Add(stock);
                }
            }

        }

    }
}
