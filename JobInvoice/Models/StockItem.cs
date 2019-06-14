using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.SQLFunctions;

namespace JobInvoice.Models
{
    public class StockItem : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            modified = true;
            //SqlStock sqlStock = new SqlStock();
            //sqlStock.UpdateAndInsertStockItems(this);
        }
        public int StockID { get; set; } = -1;

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal _Price = 0;

        public decimal Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool modified { get; set; } = true;

        public string ToStockString => Name + "_%_" + Price.ToString();
    }
}
