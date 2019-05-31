using System;
using System.Collections.Generic;
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
using JobInvoice.Models;
using JobInvoice.Windows;

namespace JobInvoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNewJob_Click(object sender, RoutedEventArgs e)
        {
            NewJob newJob = new NewJob();
            if (newJob.ShowDialog() == true)
            {
                var result = newJob.result;
            }
            //newJob.Show();
        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            NewClient newClient = new NewClient();
            //newClient.Show();
        }

        private void btnManageStock_Click(object sender, RoutedEventArgs e)
        {
            ManageStock manageStock = new ManageStock();
            //manageStock.Show();
        }

        // search box functions
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.IsDropDownOpen = true;
        }

        
    }
}
