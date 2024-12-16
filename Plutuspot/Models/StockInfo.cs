using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plutuspot.Models
{

    public class StockInfo : ObservableObject
    {
        public int rc { get; set; }
        public int rt { get; set; }
        public int svr { get; set; }
        public int lt { get; set; }
        public int full { get; set; }
        public string dlmkts { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int total { get; set; }
        public List<Diff> diff { get; set; } = new List<Diff>();
    }

    public class Diff : ObservableObject
    {
        public int index { get; set; }
        public float f2 { get; set; }
        public float f3 { get; set; }
        public string f12 { get; set; }
        public int f13 { get; set; }
        public string f14 { get; set; }
        public int f29 { get; set; }
        public int f148 { get; set; }
    }

    

}
