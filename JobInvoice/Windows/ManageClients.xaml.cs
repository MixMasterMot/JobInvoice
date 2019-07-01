using JobInvoice.Controllers;
using JobInvoice.Models;
using JobInvoice.SQLFunctions;
using JobInvoice.ViewModels;
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

namespace JobInvoice.Windows
{
    /// <summary>
    /// Interaction logic for ManageClients.xaml
    /// </summary>
    public partial class ManageClients : Window
    {
        private AllClientsViewModel allClientsView = new AllClientsViewModel();
        private AllClientsViewModel allClientsViewCompare = new AllClientsViewModel();
        public ManageClients()
        {
            InitializeComponent();
            allClientsView.ClientListObservable = GetClients();
            allClientsViewCompare.ClientListObservable = GetClients();
            datagridClients.ItemsSource = allClientsView.ClientListObservable;
        }

        private ObservableCollection<Client> GetClients()
        {
            SqlClient sqlclient = new SqlClient();
            return sqlclient.GetClientList();
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlClient sqlClient = new SqlClient();
            sqlClient.UpdateAndInsertClients(allClientsView.ClientListObservable);
            CompareClientsList comp = new CompareClientsList();
            List<string> deletedID = comp.Compare(allClientsViewCompare.ClientListObservable, allClientsView.ClientListObservable);
            sqlClient.DeletItems(deletedID);
            MessageBox.Show("Clients Updated");
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatagridClients_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel) return;
            if((string)e.Column.Header == "Client ID")
            {
                string testID = ((TextBox)e.EditingElement).Text;
                if (!CheckUniqueClientID(testID))
                {
                    MessageBox.Show("Please enter a unique Client ID");
                    (sender as DataGrid).CancelEdit(DataGridEditingUnit.Cell);
                }
            }
        }

        private bool CheckUniqueClientID(string testID)
        {
            foreach(Client client in allClientsView.ClientListObservable)
            {
                if (client.ClientID == testID)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
