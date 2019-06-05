using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JobInvoice.ViewModels;
using JobInvoice.Models;
using JobInvoice.Windows;
using System.Windows.Threading;

namespace JobInvoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AllJobsViewModel AllJobsViewModel = new AllJobsViewModel();
        private int timerTickCount = 0;
        private bool hasSelectionChanged = false;
        private DispatcherTimer timer;
        private int datagridIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            SetDataGridSource();
            initializeTimer();
        }

        private void initializeTimer()
        {
            timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 1, 0); // will 'tick' once every minute
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            AllJobsViewModel.JobListObservable[datagridIndex].TimeRemaining--;
            
        }

        private void SetDataGridSource()
        {
            JobDataGrid.ItemsSource = AllJobsViewModel.JobListObservable;
            //JobDataGrid.DataContext = jobs;
        }

        private void btnNewJob_Click(object sender, RoutedEventArgs e)
        {
            NewJob newJob = new NewJob();
            if (newJob.ShowDialog() == true)
            {
                Job result = newJob.result;
                //JobDataGrid.Items.Add(result);
                AllJobsViewModel.JobListObservable.Add(result);
            }
            //newJob.Show();
        }

        private void JobDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //// Have to do this in the unusual case where the border of the cell gets selected
            //// and causes a crash 'EditItem is not allowed'
            e.Cancel = true;
        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            NewClient newClient = new NewClient();
            newClient.Show();
        }

        private void btnManageStock_Click(object sender, RoutedEventArgs e)
        {
            ManageStock manageStock = new ManageStock();
            manageStock.Show();
        }

        // search box functions
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.IsDropDownOpen = true;
        }

        private void CheckWorking_Checked(object sender, RoutedEventArgs e)
        {
            // start timer
            datagridIndex = JobDataGrid.SelectedIndex;
            timer.Start();
        }

        private void CheckWorking_Unchecked(object sender, RoutedEventArgs e)
        {
            // stop timer
            timer.Stop();
        }
    }
}
