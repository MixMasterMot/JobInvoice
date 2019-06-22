using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class UsedStockItem : INotifyPropertyChanged, IEditableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            modified = true;
        }

        private int _JobID;
        private string _Name;
        private int _Height;
        private int _Width;
        private bool _Vat;
        private decimal _Price;

        public int JobID
        {
            get { return _JobID; }
            set
            {
                if (_JobID != value)
                {
                    _JobID = value;
                    NotifyPropertyChanged("JobID");
                }
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        public int Height
        {
            get { return _Height; }
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    NotifyPropertyChanged("Height");
                }
            }
        }
        public int Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    NotifyPropertyChanged("Width");
                }
            }
        }
        public bool Vat
        {
            get { return _Vat; }
            set
            {
                if (_Vat != value)
                {
                    _Vat = value;
                    NotifyPropertyChanged("Vat");
                }
            }
        }
        public decimal Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    NotifyPropertyChanged("Price");
                }
            }
        }
        public bool modified { get; set; } = true;

        private UsedStockItem backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (!inEdit) return;
            inEdit = true;
            backupCopy = (UsedStockItem)this.MemberwiseClone();
        }
        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.JobID = backupCopy.JobID;
            this.Name = backupCopy.Name;
            this.Height = backupCopy.Height;
            this.Width = backupCopy.Width;
            this.Vat = backupCopy.Vat;
            this.Price = backupCopy.Price;
            this.modified = backupCopy.modified;
        }
        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }
    }
}
