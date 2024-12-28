using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plutuspot.Models
{

    public class StockNews
    {
        public string req_trace { get; set; }
        public int success { get; set; }
        public string error { get; set; }
        public List<News> data { get; set; } = new List<News>();
        public int size { get; set; }
    }

    public class News
    {
        public string artTitle { get; set; }
        public string artCode { get; set; }
        public string summary { get; set; }
        public int pinglunNum { get; set; }
        public int clickNum { get; set; }
        public int shareNum { get; set; }
        public int likeNum { get; set; }
        public int autoHotValue { get; set; }
        public string mediaName { get; set; }
        public string showTime { get; set; }
        public int sort { get; set; }
    }

}
