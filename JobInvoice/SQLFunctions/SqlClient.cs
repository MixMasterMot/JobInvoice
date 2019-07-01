using JobInvoice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.SQLFunctions
{
    class SqlClient : SqlConString
    {
        public void UpdateAndInsertClients(Client client)
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            clients.Add(client);
            UpdateAndInsertClients(clients);
        }
        public void UpdateAndInsertClients(ObservableCollection<Client> clients)
        {
            List<Client> UpdateStock = new List<Client>();
            List<Client> NewStock = new List<Client>();

            foreach (Client client in clients)
            {
                if (client.modified == true && client.NewEntery == true)
                {
                    NewStock.Add(client);
                }
                else if (client.modified == true)
                {
                    UpdateStock.Add(client);
                }
            }
            UpdateClients(UpdateStock);
            InsertClients(NewStock);
        }
        public ObservableCollection<Client> GetClientList()
        {
            ObservableCollection<Client> items = new ObservableCollection<Client>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from Clients";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.ClientID = (string)reader.GetValue(0);
                        client.Street = (string)reader.GetValue(1);
                        client.StreetNumber = (string)reader.GetValue(2);
                        client.Suburb = (string)reader.GetValue(3);
                        client.City = (string)reader.GetValue(4);
                        client.Country = (string)reader.GetValue(5);
                        client.PostalCode = (string)reader.GetValue(6);
                        client.IDNumber = (string)reader.GetValue(7);
                        client.FirstName = (string)reader.GetValue(8);
                        client.Surname = (string)reader.GetValue(9);
                        client.Number = (string)reader.GetValue(10);
                        client.Email = (string)reader.GetValue(11);
                        client.modified = false;
                        client.NewEntery = false;
                        items.Add(client);
                    }
                }
            }
            return items;
        }

        public Client GetClient(string clientID)
        {
            Client client = new Client();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from Clients where ClientID = @ClientID";
                command.Parameters.AddWithValue("@ClientID", clientID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        client.ClientID = (string)reader.GetValue(0);
                        client.Street = (string)reader.GetValue(1);
                        client.StreetNumber = (string)reader.GetValue(2);
                        client.Suburb = (string)reader.GetValue(3);
                        client.City = (string)reader.GetValue(4);
                        client.Country = (string)reader.GetValue(5);
                        client.PostalCode = (string)reader.GetValue(6);
                        client.IDNumber = (string)reader.GetValue(7);
                        client.FirstName = (string)reader.GetValue(8);
                        client.Surname = (string)reader.GetValue(9);
                        client.Number = (string)reader.GetValue(10);
                        client.Email = (string)reader.GetValue(11);
                        client.modified = false;
                        client.NewEntery = false;
                    }
                }
            }
            return client;
        }

        public void DeletItems(List<string> deletedItems)
        {
            string query = "Delete from Clients where ClientID=@id";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (string id in deletedItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void InsertClients(List<Client> stockItems)
        {
            string query = "INSERT INTO Clients values(@ClientID, @Street, @StreetNumber, @Suburb, @City, @Country, @PostalCode, " +
                "@IDNumber, @FirstName, @Surname, @Number, @Email)";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (Client item in stockItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ClientID", item.ClientID);

                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@StreetNumber", item.StreetNumber);
                    command.Parameters.AddWithValue("@Suburb", item.Suburb);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@Country", item.Country);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                    command.Parameters.AddWithValue("@IDNumber", item.IDNumber);

                    command.Parameters.AddWithValue("@FirstName", item.FirstName);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@Number", item.Number);
                    command.Parameters.AddWithValue("@Email", item.Email);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void UpdateClients(List<Client> stockItems)
        {
            string query = "Update Clients set ClientID = @ClientID, Street = @Street, StreetNumber = @StreetNumber, " +
                "Suburb = @Suburb, City = @City, Country = @Country, PostalCode = @PostalCode, IDNumber = @IDNumber" +
                "FirstName = @FirstName, Surname = @Surname, Number = @Number, Email = @Email " +
                "where ClientID = @ClientID ";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (Client item in stockItems)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ClientID", item.ClientID);
                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@StreetNumber", item.StreetNumber);
                    command.Parameters.AddWithValue("@Suburb", item.Suburb);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@Country", item.Country);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                    command.Parameters.AddWithValue("@IDNumber", item.IDNumber);
                    command.Parameters.AddWithValue("@FirstName", item.FirstName);
                    command.Parameters.AddWithValue("@Surname", item.Surname);
                    command.Parameters.AddWithValue("@Number", item.Number);
                    command.Parameters.AddWithValue("@Email", item.Email);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
