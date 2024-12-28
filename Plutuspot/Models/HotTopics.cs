using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plutuspot.Models
{

    public class HotTopics
    {
        public List<Re> re { get; set; } = new List<Re>();
        public int count { get; set; }
        public string time { get; set; }
        public int rc { get; set; }
        public string me { get; set; }
    }

    public class Re
    {
        public List<Stocklist> stockList { get; set; } = new List<Stocklist>();
        public Voteinfo voteInfo { get; set; }
        public Hotpostentity hotPostEntity { get; set; }
        public object relateIndex { get; set; }
        public List<Toprelatestock> topRelateStock { get; set; } = new List<Toprelatestock>();
        public List<Barcodelist> barCodeList { get; set; } = new List<Barcodelist>();
        public int appShow { get; set; }
        public int fireStyle { get; set; }
        public DateTime mTime { get; set; }
        public DateTime cTime { get; set; }
        public int topicShowTag { get; set; }
        public int topicRealTag { get; set; }
        public int showPkTag { get; set; }
        public object pkPost { get; set; }
        public int topicid { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string pic { get; set; }
        public int participantCount { get; set; }
        public int clickCount { get; set; }
        public List<string> StockListNew { get; set; } = new List<string>();
        public int sortType { get; set; }
        public List<object> RelateIndexNew { get; set; } = new List<object>();
        public List<string> TopRelateStockNew { get; set; } = new List<string>();
        public object extend { get; set; }
    }

    public class Voteinfo
    {
        public int voteId { get; set; }
        public string title { get; set; }
        public int state { get; set; }
        public int count { get; set; }
        public List<Optionlist> optionList { get; set; } = new List<Optionlist>();
    }

    public class Optionlist
    {
        public int voId { get; set; }
        public string content { get; set; }
    }

    public class Hotpostentity
    {
        public string postid { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string user_avatar { get; set; }
        public string third_id { get; set; }
        public string post_type { get; set; }
        public string code { get; set; }
    }

    public class Stocklist
    {
        public string outerCode { get; set; }
        public int category { get; set; }
        public int market { get; set; }
        public int qMarket { get; set; }
        public string qCode { get; set; }
    }

    public class Toprelatestock
    {
        public string outerCode { get; set; }
        public int category { get; set; }
        public int market { get; set; }
        public int qMarket { get; set; }
        public string qCode { get; set; }
    }

    public class Barcodelist
    {
        public string outerCode { get; set; }
        public int category { get; set; }
        public int market { get; set; }
        public int qMarket { get; set; }
        public string qCode { get; set; }
    }

}
