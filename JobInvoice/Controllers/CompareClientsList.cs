using JobInvoice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Controllers
{
    class CompareClientsList
    {
        public List<string> Compare(ObservableCollection<Client> source, ObservableCollection<Client> toCompare)
        {
            List<string> deletedItems = new List<string>();
            foreach (Client item in source)
            {
                bool found = false;
                Client foundItem = null;
                foreach (Client tstItem in toCompare)
                {
                    if (tstItem.ClientID == item.ClientID)
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
                    deletedItems.Add(item.ClientID);
                }
            }
            return deletedItems;
        }
    }
}
