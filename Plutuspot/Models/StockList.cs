using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plutuspot.Models
{

    //[JsonSerializable(typeof(StockList))]
    //public partial class StockListContext : JsonSerializerContext
    //{
    //}

    public class StockList
    {

        public string globalId { get; set; }

        public string message { get; set; }
        public int status { get; set; }

        public int code { get; set; }

        public List<StockItem> data { get; set; } = new List<StockItem>();

        public object stack { get; set; }
    }

    public class StockItem
    {
        public string sc { get; set; }
        public int rk { get; set; }
        public int rc { get; set; }
        public int hisRc { get; set; }
    }

}
