using Microsoft.VisualBasic;
using Plutuspot.Core;
using Plutuspot.Models;
using System.Collections.ObjectModel;

namespace Plutuspot.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string> {"热门股票" , "热门板块" };
        public ObservableCollection<Diff> StockItems { get; set; } = new ObservableCollection<Diff>();

        public MainWindowViewModel() {  
            Init();
        }

        public async void Init()
        {
            Stocks stocks = new Stocks();
            await stocks.GetAllStocksList();
            await stocks.GetAllStocksPriceAndNames();
            int index = 0;
            stocks.StocksInfo.data.diff.ForEach(stock => {
                index++;
                stock.index = index;
                StockItems.Add(stock); });
        }

    }
}
