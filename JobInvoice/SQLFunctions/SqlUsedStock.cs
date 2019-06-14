using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.Models;

namespace JobInvoice.SQLFunctions
{
    class SqlUsedStock : SqlConString
    {
        public ObservableCollection<UsedStockItem> GetUsedStockItems(int JobID)
        {
            ObservableCollection<UsedStockItem> items = new ObservableCollection<UsedStockItem>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from UsedStock Where JobID = @id";
                command.Parameters.AddWithValue("@id", JobID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsedStockItem stockItem = new UsedStockItem();
                        stockItem.JobID = (int)reader.GetValue(0);
                        stockItem.Name = (string)reader.GetValue(1);
                        stockItem.Height = (int)reader.GetValue(2);
                        stockItem.Width = (int)reader.GetValue(3);
                        stockItem.Vat = (bool)reader.GetValue(4);
                        stockItem.Price = (decimal)reader.GetValue(5);
                        stockItem.modified = false;
                        items.Add(stockItem);
                    }
                }
            }
            return items;
        }

        public List<UsedStockItem> GetUsedStockItemsList(int JobID)
        {
            List<UsedStockItem> items = new List<UsedStockItem>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from UsedStock Where JobID = @id";
                command.Parameters.AddWithValue("@id", JobID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsedStockItem stockItem = new UsedStockItem();
                        stockItem.JobID = (int)reader.GetValue(0);
                        stockItem.Name = (string)reader.GetValue(1);
                        stockItem.Height = (int)reader.GetValue(2);
                        stockItem.Width = (int)reader.GetValue(3);
                        stockItem.Vat = (bool)reader.GetValue(4);
                        stockItem.Price = (decimal)reader.GetValue(5);
                        stockItem.modified = false;
                        items.Add(stockItem);
                    }
                }
            }
            return items;
        }

        public void DeletItems(List<int> deletedItems)
        {
            string query = "Delete from Clients where ClientID=@id";
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
    }
}
