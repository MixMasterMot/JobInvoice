using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.Models;

namespace JobInvoice.ViewModels
{
    public class AllJobsViewModel: INotifyPropertyChanged
    {
        ObservableCollection<Job> jobListObservable;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Job> JobListObservable
        {
            get { return jobListObservable; }
            set
            {
                jobListObservable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("JobListObservable"));
            }
        }

        public AllJobsViewModel()
        {
            JobListObservable = new ObservableCollection<Job>();
        }

    }
}
