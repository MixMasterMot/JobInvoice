using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class StockItem
    {
        public string Name { get; set; }
        public double Price { get; set; } = 0;

        public string ToStockString => Name + "_%_" + Price.ToString();
    }
}
