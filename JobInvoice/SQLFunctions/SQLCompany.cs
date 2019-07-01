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
    class SqlCompany : SqlConString
    {
        public void InsertCompany(Company company)
        {
            List<Company> companies = new List<Company>();
            companies.Add(company);
            InsertCompany(companies);
        }
        public void InsertCompany(List<Company> companies)
        {
            string query = "INSERT INTO Company values(@Street, @StreetNumber, @Suburb, @City, " +
                "@Country, @PostalCode, @Name, @Number, @Email, @Logo, @BankName, @BankNumber, " +
                "@BankType, @BankBranchCode, @Message)";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (Company item in companies)
                {
                    command.Parameters.Clear();
                    //command.Parameters.AddWithValue("@ID", item.ID);
                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@StreetNumber", item.StreetNumber);
                    command.Parameters.AddWithValue("@Suburb", item.Suburb);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@Country", item.Country);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Number", item.Number);
                    command.Parameters.AddWithValue("@Email", item.Email);
                    command.Parameters.AddWithValue("@Logo", item.Logo);
                    command.Parameters.AddWithValue("@BankName", item.BankName);
                    command.Parameters.AddWithValue("@BankNumber", item.BankNumber);
                    command.Parameters.AddWithValue("@BankType", item.BankType);
                    command.Parameters.AddWithValue("@BankBranchCode", item.BankBranchCode);
                    command.Parameters.AddWithValue("@Message", item.Message);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void UpdateCompany(Company company)
        {
            List<Company> companies = new List<Company>();
            companies.Add(company);
            UpdateCompany(companies);
        }
        public void UpdateCompany(List<Company> companies)
        {
            string query = "Update Company set Street = @Street, StreetNumber = @StreetNumber, " +
                "Suburb = @Suburb, City = @City, Country = @Country, PostalCode = @PostalCode, Name = @Name, " +
                "Number = @Number, Email = @Email, Logo = @Logo, BankName = @BankName, BankNumber = @BankNumber, " +
                "BankType = @BankType, BankBranchCode = @BankBranchCode, Message = @Message where ID = @ID ";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (Company item in companies)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID", item.ID);
                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@StreetNumber", item.StreetNumber);
                    command.Parameters.AddWithValue("@Suburb", item.Suburb);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@Country", item.Country);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Number", item.Number);
                    command.Parameters.AddWithValue("@Email", item.Email);
                    command.Parameters.AddWithValue("@Logo", item.Logo);
                    command.Parameters.AddWithValue("@BankName", item.BankName);
                    command.Parameters.AddWithValue("@BankNumber", item.BankNumber);
                    command.Parameters.AddWithValue("@BankType", item.BankType);
                    command.Parameters.AddWithValue("@BankBranchCode", item.BankBranchCode);
                    command.Parameters.AddWithValue("@Message", item.Message);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Company> GetCompany()
        {
            List<Company> items = new List<Company>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from Company";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company comp = new Company();
                        comp.ID = (int)reader.GetValue(0);
                        comp.Street = (string)reader.GetValue(1);
                        comp.StreetNumber = (string)reader.GetValue(2);
                        comp.Suburb = (string)reader.GetValue(3);
                        comp.City = (string)reader.GetValue(4);
                        comp.Country = (string)reader.GetValue(5);
                        comp.PostalCode = (string)reader.GetValue(6);
                        comp.Name = (string)reader.GetValue(7);
                        comp.Number = (string)reader.GetValue(8);
                        comp.Email = (string)reader.GetValue(9);
                        comp.Logo = (string)reader.GetValue(10);
                        comp.BankName = (string)reader.GetValue(11);
                        comp.BankNumber = (string)reader.GetValue(12);
                        comp.BankType = (string)reader.GetValue(13);
                        comp.BankBranchCode = (string)reader.GetValue(14);
                        items.Add(comp);
                    }
                }
            }
            return items;
        }
    }
}
