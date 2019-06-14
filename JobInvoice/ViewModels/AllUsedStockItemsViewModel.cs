using JobInvoice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobInvoice.ViewModels
{
    public class AllUsedStockItemsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<UsedStockItem> usedStockItemsObservable;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UsedStockItem> UsedStockItemsObservable
        {
            get { return usedStockItemsObservable; }
            set
            {
                usedStockItemsObservable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UsedStockItemsObservable"));
            }
        }
        public AllUsedStockItemsViewModel()
        {
            UsedStockItemsObservable = new ObservableCollection<UsedStockItem>();
        }
    }
}
