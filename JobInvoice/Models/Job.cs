using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class Job
    {
        public string JobID { get; set; }
        public string ClientID { get; set; }
        public string Description { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public List<StockItem> StockItems { get; set; } = new List<StockItem>();
        public int TimeRemaining { get; set; } = 0;
        public bool Completed { get; set; } = false;
        public string StockItemsToString()
        {
            string StockItemString = "";
            foreach(StockItem item in StockItems)
            {
                StockItemString = StockItemString + "[" + item.ToStockString + "]";
            }
            return StockItemString;
        }
    }
}
