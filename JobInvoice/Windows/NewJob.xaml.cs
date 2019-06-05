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
using JobInvoice.Models;

namespace JobInvoice.Windows
{
    /// <summary>
    /// Interaction logic for NewJob.xaml
    /// </summary>
    public partial class NewJob : Window
    {
        public Job result { get; set; }
        
        public NewJob()
        {
            InitializeComponent();
            result = null;
        }

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
            job.Completed = false;
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
            job.HourRate = Convert.ToDouble(txtRate.Text);
            job.CompletionTime = Convert.ToDouble(txtTime.Text);
            job.LabourCost = Convert.ToDouble(txtTotalLabour.Text);
            job.JobTotalExcVat = Convert.ToDouble(txtTotalExcVat.Text);
            job.JobVat = Convert.ToDouble(txtTotalVat.Text);
            job.JobTotalIncVat = Convert.ToDouble(txtTotalIncVat.Text);

            return job;
        }
    }
}
