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
    public class AllClientsViewModel:INotifyPropertyChanged
    {
        ObservableCollection<Client> clientListObservable;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Client> ClientListObservable
        {
            get { return clientListObservable; }
            set
            {
                clientListObservable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClientListObservable"));
            }
        }

        public AllClientsViewModel()
        {
            ClientListObservable = new ObservableCollection<Client>();
        }
    }
}
