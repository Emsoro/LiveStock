using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plutuspot.Models
{

    public class StockList
    {
        public string globalId { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public int code { get; set; }
        public StockItem[] data { get; set; }
        public object stack { get; set; }
    }

    public class StockItem
    {
        public string sc { get; set; }
        public int rk { get; set; }
        public int rc { get; set; }
        public int hisRc { get; set; }
        public string name { get; set; }= string.Empty;
    }

}
