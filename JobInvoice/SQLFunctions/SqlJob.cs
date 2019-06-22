using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using JobInvoice.Models;

namespace JobInvoice.SQLFunctions
{
    class SqlJob : SqlConString
    {
        public ObservableCollection<Job> GetJobs()
        {
            ObservableCollection<Job> items = new ObservableCollection<Job>();
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Select * from UsedStock";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Job job = new Job();
                        job.JobID = (int)reader.GetValue(0);
                        job.ClientID = (string)reader.GetValue(1);
                        job.Description = (string)reader.GetValue(2);
                        job.DateIn = (DateTime)reader.GetValue(3);
                        job.DateOut = (DateTime)reader.GetValue(4);
                        job.TimeRemaining = (int)reader.GetValue(5);
                        job.Completed = (bool)reader.GetValue(6);
                        job.HourRate = (decimal)reader.GetValue(7);
                        job.CompletionTime = (decimal)reader.GetValue(8);
                        job.LabourCost = (decimal)reader.GetValue(8);
                        job.JobTotalExcVat = (decimal)reader.GetValue(10);
                        job.JobVat = (decimal)reader.GetValue(11);
                        job.JobTotalIncVat = (decimal)reader.GetValue(12);
                        items.Add(job);
                    }
                }
            }
            return items;
        }

        public void UpdateJob(Job job)
        {
            List<Job> jobs = new List<Job>();
            jobs.Add(job);
            UpdateJob(jobs);
        }
        public void UpdateJob(List<Job> jobs)
        {
            string query = "Update UsedStock set ClientID = @clientID, Description = @description, DateIn=@dateIn, " +
                "DateOut=@dateOut, TimeRemaining=@timeRemaining, Completed=@completed, HourRate=@hourRate, " +
                "CompletionTime=@completionTime, LabourCost=@labourCost, JobTotalExcVat=@totExcVat, JobVat=@jobVat, " +
                "JobTotalIncVat=@jobTotalIncVat where JobID = @id";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (Job item in jobs)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", item.JobID);
                    command.Parameters.AddWithValue("@clientID", item.ClientID);
                    command.Parameters.AddWithValue("@description", item.Description);
                    command.Parameters.AddWithValue("@dateIn", item.DateIn);
                    command.Parameters.AddWithValue("@dateOut", item.DateOut);
                    command.Parameters.AddWithValue("@timeRemaining", item.TimeRemaining);
                    command.Parameters.AddWithValue("@completed", item.Completed);
                    command.Parameters.AddWithValue("@hourRate", item.HourRate);
                    command.Parameters.AddWithValue("@completionTime", item.CompletionTime);
                    command.Parameters.AddWithValue("@labourCost", item.LabourCost);
                    command.Parameters.AddWithValue("@totExcVat", item.JobTotalExcVat);
                    command.Parameters.AddWithValue("@jobVat", item.JobVat);
                    command.Parameters.AddWithValue("@jobTotalIncVat", item.JobTotalIncVat);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeleteJob(int jobId)
        {
            List<int> deleteItems = new List<int>();
            deleteItems.Add(jobId);
            DeleteJob(deleteItems);
        }
        public void DeleteJob(List<Job> jobs)
        {
            List<int> deleteItems = new List<int>();
            foreach(Job job in jobs)
            {
                deleteItems.Add(job.JobID);
            }
            DeleteJob(deleteItems);
        }
        public void DeleteJob(List<int> jobs)
        {
            string query = "Delete from UsedStock where ItemID=@id";
            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                foreach (int id in jobs)
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
