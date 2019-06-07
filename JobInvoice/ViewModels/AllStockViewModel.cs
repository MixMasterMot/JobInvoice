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
    public class AllStockViewModel : INotifyPropertyChanged
    {
        
        ObservableCollection<StockItem> stockListObservable;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<StockItem> StockListObservable
        {
            get { return stockListObservable; }
            set
            {
                stockListObservable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StockListObservable"));
            }
        }

        public AllStockViewModel()
        {
            StockListObservable = new ObservableCollection<StockItem>();
        }
    }
}