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
            result = SetJob();
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }

        private Job SetJob()
        {
            Job job = new Job();
            job.JobID = lblJobID.Content.ToString();
            job.ClientID = comboClientID.Text;
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
