using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.Models;
using JobInvoice.ViewModels;

namespace JobInvoice.SQLFunctions
{
    class SqlStock : SqlConString
    {
        public ObservableCollection<StockItem> GetStockList()
        {
            ObservableCollection<StockItem> items = new ObservableCollection<StockItem>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using(SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from StockItem";
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockItem stockItem = new StockItem();
                        stockItem.StockID = (int)reader.GetValue(0);
                        stockItem.Name = (string)reader.GetValue(1);
                        stockItem.Price = (decimal)reader.GetValue(2);
                        stockItem.modified = false;
                        items.Add(stockItem);
                    }
                }
            }
            return items;
        }

        public void UpdateAndInsertStockItems(StockItem stockItem)
        {
            ObservableCollection<StockItem> stockItems = new ObservableCollection<StockItem>();
            stockItems.Add(stockItem);
            UpdateAndInsertStockItems(stockItems);
        }

        public void UpdateAndInsertStockItems(ObservableCollection<StockItem> stockItems)
        {
            List<StockItem> UpdateStock = new List<StockItem>();
            List<StockItem> NewStock = new List<StockItem>();

            foreach (StockItem item in stockItems)
            {
                if (item.modified == true && item.StockID == -1)
                {
                    NewStock.Add(item);
                }
                else if (item.modified == true)
                {
                    UpdateStock.Add(item);
                }
            }
            UpdateStockItem(UpdateStock);
            InsertStockItems(NewStock);
        }

        internal void DeletItems(List<int> deletedItems)
        {
            string query = "Delete from StockItem where ItemID=@id";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (int id in deletedItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void InsertStockItems(List<StockItem> stockItems)
        {
            string query = "INSERT INTO StockItem values(@name, @price)";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (StockItem item in stockItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void UpdateStockItem(List<StockItem> stockItems)
        {
            string query = "Update StockItem set Name = @name, Price = @price where ItemID = @id";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach(StockItem item in stockItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", item.StockID);
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        

    }
}
