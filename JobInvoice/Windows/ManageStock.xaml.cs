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
using System.Windows.Shapes;
using JobInvoice.SQLFunctions;
using JobInvoice.Models;
using JobInvoice.ViewModels;
using System.Collections.ObjectModel;
using JobInvoice.Controllers;

namespace JobInvoice.Windows
{
    /// <summary>
    /// Interaction logic for ManageStock.xaml
    /// </summary>
    public partial class ManageStock : Window
    {
        private AllStockViewModel StockItemsView = new AllStockViewModel();
        private AllStockViewModel StockItemsViewCompare = new AllStockViewModel();
        public ManageStock()
        {
            InitializeComponent();
            StockItemsView.StockListObservable = GetStock();
            StockItemsViewCompare.StockListObservable = GetStock();
            datagridStock.ItemsSource = StockItemsView.StockListObservable;
        }

        private ObservableCollection<StockItem> GetStock()
        {
            SqlStock getSqlStock = new SqlStock();
            return getSqlStock.GetStockList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlStock getSqlStock = new SqlStock();
            getSqlStock.UpdateAndInsertStockItems(StockItemsView.StockListObservable);
            CompareStockItemList stockItemList = new CompareStockItemList();
            List<int> deletedItems = stockItemList.Compare(StockItemsViewCompare.StockListObservable, StockItemsView.StockListObservable);
            getSqlStock.DeletItems(deletedItems);
            MessageBox.Show("Stock Updated");
            this.Close();
        }

        
    }
}
