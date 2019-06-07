using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class Job : INotifyPropertyChanged
    {
        private int _timeReamining = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string JobID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public List<StockItem> StockItems { get; set; } = new List<StockItem>();
        public int TimeRemaining
        {
            get { return _timeReamining; }
            set
            {
                if (_timeReamining != value)
                {
                    _timeReamining = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Working { get; set; } = false;
        public bool Completed { get; set; } = false;
        public decimal HourRate { get; set; } = 0;
        public decimal CompletionTime { get; set; } = 0;
        public decimal LabourCost { get; set; } = 0;
        public decimal JobTotalExcVat { get; set; } = 0;
        public decimal JobVat { get; set; } = 0;
        public decimal JobTotalIncVat { get; set; } = 0;
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
