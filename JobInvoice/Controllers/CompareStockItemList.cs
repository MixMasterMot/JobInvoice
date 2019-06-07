using JobInvoice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Controllers
{
    public class CompareStockItemList
    {
        public List<int> Compare(ObservableCollection<StockItem> source, ObservableCollection<StockItem> toCompare)
        {
            List<int> deletedItems = new List<int>();
            foreach(StockItem item in source)
            {
                bool found = false;
                StockItem foundItem = null;
                foreach(StockItem tstItem in toCompare)
                {
                    if (tstItem.StockID == item.StockID)
                    {
                        foundItem = tstItem;
                        found = true;
                        break;
                    }
                }
                if (foundItem != null)
                {
                    toCompare.Remove(foundItem);
                }
                if (!found)
                {
                    deletedItems.Add(item.StockID);
                }
            }
            return deletedItems;
        }
    }
}
