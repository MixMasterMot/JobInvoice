using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    
    public class Client : INotifyPropertyChanged
    {
        public string _ClientID;
        public string _Address = "";
        public string _FirstName;
        public string _Surname = "";
        public string _Number = "";
        public string _Email = "";
        public bool NewEntery { get; set; } = true;
        public string ClientID
        {
            get { return _ClientID; }
            set
            {
                if (_ClientID != value)
                {
                    _ClientID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get { return _Surname; }
            set
            {
                if (_Surname != value)
                {
                    _Surname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Number
        {
            get { return _Number; }
            set
            {
                if (_Number != value)
                {
                    _Number = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _ClientID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DisplayName
        {
            get { return _FirstName + " " + _Surname; }
        }

        public bool modified { get; set; } = false;
   
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            modified = true;
        }
    }
}
