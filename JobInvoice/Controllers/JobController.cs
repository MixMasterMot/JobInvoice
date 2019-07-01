using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.Models;
using JobInvoice.SQLFunctions;

namespace JobInvoice.Controllers
{
    public class JobController
    {
        public void UpdateJob(Job job)
        {
            // update used stock
            SqlUsedStock sqlUsedStock = new SqlUsedStock();
            sqlUsedStock.DeletItems(job.JobID);
            sqlUsedStock.InsertItems(job.StockItems);
            // update job
            SqlJob sqlJob = new SqlJob();
            sqlJob.UpdateJob(job);
        }

        public void DeleteJob(Job job)
        {
            // update used stock
            SqlUsedStock sqlUsedStock = new SqlUsedStock();
            sqlUsedStock.DeletItems(job.JobID);
            // update job
            SqlJob sqlJob = new SqlJob();
            sqlJob.DeleteJob(job.JobID);
        }

        public Job AddNewJob(Job job)
        {
            // update job
            SqlJob sqlJob = new SqlJob();
            int jobID = sqlJob.AddNewJob(job);
            job.JobID = jobID;
            job.SetStockItemJobID();
            // update used stock
            SqlUsedStock sqlUsedStock = new SqlUsedStock();
            sqlUsedStock.InsertItems(job.StockItems);
            return job;
        }
    }
}
