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
using System.Windows.Shapes;
using JobInvoice.Models;
using JobInvoice.SQLFunctions;
using JobInvoice.ViewModels;

namespace JobInvoice.Windows
{
    /// <summary>
    /// Interaction logic for NewJob.xaml
    /// </summary>
    
    // Jou ma se foken kaffer poes hond 
    
    public partial class NewJob : Window
    {
        // Constants
        AllUsedStockItemsViewModel usedMaterials = new AllUsedStockItemsViewModel();

        // return job to mainwindow
        public Job result { get; set; }

        // Lists to filter clients
        List<string> ClientIDList = new List<string>();
        List<string> ClientNameList = new List<string>();

        // Dictionary to find Clients
        Dictionary<string, string> IdDict = new Dictionary<string, string>();
        Dictionary<string, string> NameDict = new Dictionary<string, string>();
        // List to store stock list
        List<string> stockItems = new List<string>();
        Dictionary<string, StockItem> stockDict = new Dictionary<string, StockItem>();

        //initialize form
        public NewJob(Job job)
        {
            this.Resources.Add("stockItems", stockItems);
            InitializeComponent();
            GetClients();
            GetMaterials();

            if (job.JobID != -1)
            {
                SetExistingJob(job);
            }

            comboClientID.ItemsSource = ClientIDList;
            comboClientName.ItemsSource = ClientNameList;
            datagridMaterials.ItemsSource = usedMaterials.UsedStockItemsObservable;
        }
       
        // get constants
        private void GetClients()
        {
            SqlClient sqlClient = new SqlClient();
            ObservableCollection<Client> clientsObs = sqlClient.GetClientList();
            foreach (Client client in clientsObs)
            {
                ClientIDList.Add(client.ClientID);
                ClientNameList.Add(client.DisplayName);
                IdDict.Add(client.ClientID, client.DisplayName);
                //NameDict.Add(client.DisplayName, client.ClientID);
            }
            ClientIDList.Sort();
            ClientNameList.Sort();
        }
        private void GetMaterials()
        {
            SqlStock getSqlStock = new SqlStock();
            ObservableCollection<StockItem> stocksObs = getSqlStock.GetStockList();
            foreach (StockItem stock in stocksObs)
            {
                stockItems.Add(stock.Name);
                stockDict.Add(stock.Name, stock);
            }
        }

        // set existing job values
        private void SetExistingJob(Job job)
        {
            // set ID
            lblJobID.Content = job.JobID.ToString();
            // set Client ID
            comboClientID.Text = job.ClientID;
            // set Client Name
            comboClientName.Text = job.ClientName;
            // set Description
            txtDescription.Text = job.Description;
            // set DateIn
            dateStart.Text = job.DateIn.ToString();
            // set DateOut
            dateEnd.Text = job.DateOut.ToString();
            // set StockItems
            usedMaterials.UsedStockItemsObservable = job.UsedStockObservable();
            // set HourRate
            txtRate.Text = job.HourRate.ToString();
            // set CompletionTime
            txtTimeLimit.Text = job.CompletionTime.ToString();
            // set TimeLimit
            txtCompTime.Text = job.TimeRemaining.ToString();
            // set LabourCost
            txtTotalLabour.Text = job.LabourCost.ToString();
            // set JobVat
            txtTotalVat.Text = job.JobVat.ToString();
            // set JobTotalIncVat
            txtTotalIncVat.Text = job.JobTotalIncVat.ToString();
            // set JobTotalExlVat
            txtTotalExcVat.Text = job.JobTotalExcVat.ToString();
        }

        // get focus
        private void DataGrid_CellGotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);

                Control control = GetFirstChildByType<Control>(e.OriginalSource as DataGridCell);
                if (control != null)
                {
                    control.Focus();
                }
            }
        }
        private T GetFirstChildByType<T>(DependencyObject prop) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(prop); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild((prop), i) as DependencyObject;
                if (child == null)
                    continue;

                T castedProp = child as T;
                if (castedProp != null)
                    return castedProp;

                castedProp = GetFirstChildByType<T>(child);

                if (castedProp != null)
                    return castedProp;
            }
            return null;
        }
        // Filter Client ID
        private void Generic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            List<string> source = GetComboBoxSource(box.Name);
            string filterParm = box.Text + e.Text;
            FilterComboBox(box, source, filterParm);
            e.Handled=true;
        }
        private void Generic_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            List<string> source = GetComboBoxSource(box.Name);
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                box.ItemsSource = source;
            }
        }
        private void Generic_BackSpaceFilter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                ComboBox box = sender as ComboBox;
                List<string> source = GetComboBoxSource(box.Name);
                FilterComboBox(box, source, box.Text);
            }
        }
        private List<string> GetComboBoxSource(string name)
        {
            if(name == "comboClientName")
            {
                return ClientNameList;
            }
            else if(name == "comboClientID")
            {
                return ClientIDList;
            }
            else if(name== "datagridComboColumn")
            {
                return stockItems;
            }
            return new List<string>();
        }
        private void FilterComboBox(ComboBox box, List<string> toFilter, string filterParm)
        {
            //string filterParm = box.Text + e.Text;
            List<string> filteredItems = toFilter.FindAll(x => x.ToLower().Contains(filterParm.ToLower()));
            box.ItemsSource = filteredItems;
            if (String.IsNullOrWhiteSpace(filterParm))
            {
                comboClientID.ItemsSource = toFilter;
            }
            box.IsDropDownOpen = true;
            box.SelectedIndex = -1;
            box.Text = filterParm;

            var cmbTextBox = (TextBox)box.Template.FindName("PART_EditableTextBox", box);
            cmbTextBox.CaretIndex = filterParm.Length;
            //e.Handled = true;
        }
        //Open Dropdown for combobox
        private void Generic_OpenDropdown(object sender, MouseButtonEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (!box.IsDropDownOpen)
            {
                box.IsDropDownOpen = true;
            }
        }
        // actions triggerd from buton
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }
        private void BtnAddJob_Click(object sender, RoutedEventArgs e)
        {
            result = SetJob();
            //result = TestJob();
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }
        // return job
        private Job TestJob()
        {
            Job job = new Job();
            job.JobID = 111;
            job.ClientID = "k1";
            job.ClientName = "Koos Els";
            job.Description = "Test description \n 2nd Line";
            job.DateIn = new DateTime(2019,5,1);
            job.DateOut = new DateTime(2019, 6, 1);
            job.StockItems = new List<UsedStockItem>();
            job.TimeRemaining = 120;
            job.Completed = true;
            job.HourRate = 200;
            job.CompletionTime = 200;
            job.LabourCost = 500;
            job.JobTotalExcVat = 1000;
            job.JobVat = 50;
            job.JobTotalIncVat = 1050;

            return job;
        }
        private Job SetJob()
        {
            Job job = new Job();
            job.JobID = Convert.ToInt32(lblJobID.Content);
            job.ClientID = comboClientID.Text;
            job.ClientName = comboClientName.Text;
            job.Description = txtDescription.Text;
            job.DateIn = dateStart.SelectedDate.Value.Date;
            job.DateOut = dateEnd.SelectedDate.Value.Date;
            
            List<UsedStockItem> stockItems = new List<UsedStockItem>();
            foreach(UsedStockItem item in usedMaterials.UsedStockItemsObservable)
            {
                stockItems.Add(item);
            }
            job.StockItems = stockItems;

            job.TimeRemaining = Convert.ToDecimal(txtTimeLimit.Text);
            job.Completed = false;
            job.HourRate = Convert.ToDecimal(txtRate.Text);
            job.CompletionTime = Convert.ToDecimal(txtCompTime.Text);
            job.LabourCost = Convert.ToDecimal(txtTotalLabour.Text);
            job.JobTotalExcVat = Convert.ToDecimal(txtTotalExcVat.Text);
            job.JobVat = Convert.ToDecimal(txtTotalVat.Text);
            job.JobTotalIncVat = Convert.ToDecimal(txtTotalIncVat.Text);

            return job;
        }
    }
}
