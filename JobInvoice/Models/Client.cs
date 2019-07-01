using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    
    public class Client : INotifyPropertyChanged, IEditableObject
    {
        public string _ClientID;

        public string _Street = "";
        public string _StreetNumber = "";
        public string _Suburb = "";
        public string _City = "";
        public string _Country = "";
        public string _PostalCode = "";
        public string _IDNumber = "";

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
        public string Street
        {
            get { return _Street; }
            set
            {
                if (_Street != value)
                {
                    _Street = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string StreetNumber
        {
            get { return _StreetNumber; }
            set
            {
                if (_StreetNumber != value)
                {
                    _StreetNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Suburb
        {
            get { return _Suburb; }
            set
            {
                if (_Suburb != value)
                {
                    _Suburb = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string City
        {
            get { return _City; }
            set
            {
                if (_City != value)
                {
                    _City = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Country
        {
            get { return _Country; }
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                if (_PostalCode != value)
                {
                    _PostalCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string IDNumber
        {
            get { return _IDNumber; }
            set
            {
                if (_IDNumber != value)
                {
                    _IDNumber = value;
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
                    _Email = value;
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

        private Client backupCopy;
        private bool inEdit;
        public void BeginEdit()
        {
            if (!inEdit) return;
            inEdit = true;
            backupCopy = (Client)this.MemberwiseClone();
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
            modified = true;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.modified = backupCopy.modified;
            this.ClientID = backupCopy.ClientID;
            this.FirstName = backupCopy.FirstName;
            this.Surname = backupCopy.Surname;
            this.Number = backupCopy.Number;
            this.Email = backupCopy.Email;
            this.Street = backupCopy.Street;
            this.StreetNumber = backupCopy.StreetNumber;
            this.Suburb = backupCopy.Suburb;
            this.City = backupCopy.City;
            this.Country = backupCopy.Country;
            this.PostalCode = backupCopy.PostalCode;
            this.IDNumber = backupCopy.IDNumber;
        }
    }
}
