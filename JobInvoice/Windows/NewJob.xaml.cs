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
    public partial class NewJob : Window
    {
        // Constants
        AllUsedStockItemsViewModel usedItems = new AllUsedStockItemsViewModel();

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

        public NewJob()
        {
            this.Resources.Add("stockItems", stockItems);
            InitializeComponent();
            GetClients();
            GetMaterials();
            comboClientID.ItemsSource = ClientIDList;
            comboClientName.ItemsSource = ClientNameList;
            datagridMaterials.ItemsSource = usedItems.UsedStockItemsObservable;
            //datagridComboColumn.ItemsSource = stockItems;
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
        private void ComboClientID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterComboBox(comboClientID, ClientIDList, e);
        }
        private void ComboClientID_DropDownOpened(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboClientID.Text))
            {
                comboClientID.ItemsSource = ClientIDList;
            }
        }
        private void ComboClientName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterComboBox(comboClientName, ClientNameList, e);
        }
        private void ComboClientName_DropDownOpened(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboClientName.Text))
            {
                comboClientName.ItemsSource = ClientNameList;
            }
        }
        private void FilterComboBox(ComboBox box, List<string> toFilter, TextCompositionEventArgs e)
        {
            string filterParm = box.Text + e.Text;
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
            e.Handled = true;
        }

        // actions triggerd from buton
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }
        private void BtnAddJob_Click(object sender, RoutedEventArgs e)
        {
            //result = SetJob();
            result = TestJob();
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }
        
        // return job
        private Job TestJob()
        {
            Job job = new Job();
            job.JobID = "111";
            job.ClientID = "k1";
            job.ClientName = "Koos Els";
            job.Description = "Test description \n 2nd Line";
            job.DateIn = new DateTime(2019,5,1);
            job.DateOut = new DateTime(2019, 6, 1);
            job.StockItems = new List<StockItem>();
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
            job.JobID = lblJobID.Content.ToString();
            job.ClientID = comboClientID.Text;
            job.ClientName = comboClientName.Text;
            job.Description = txtDescription.Text;
            job.DateIn = dateStart.SelectedDate.Value.Date;
            job.DateOut = dateEnd.SelectedDate.Value.Date;
            job.StockItems = new List<StockItem>();
            job.TimeRemaining = Convert.ToInt32(txtTimeLimit.Text);
            job.Completed = false;
            job.HourRate = Convert.ToDecimal(txtRate.Text);
            job.CompletionTime = Convert.ToDecimal(txtTime.Text);
            job.LabourCost = Convert.ToDecimal(txtTotalLabour.Text);
            job.JobTotalExcVat = Convert.ToDecimal(txtTotalExcVat.Text);
            job.JobVat = Convert.ToDecimal(txtTotalVat.Text);
            job.JobTotalIncVat = Convert.ToDecimal(txtTotalIncVat.Text);

            return job;
        }

    }
}
