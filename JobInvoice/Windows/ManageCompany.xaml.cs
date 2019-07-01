using System;
using System.Collections.Generic;
using System.IO;
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
using JobInvoice.SQLFunctions;
using Microsoft.Win32;

namespace JobInvoice.Windows
{
    /// <summary>
    /// Interaction logic for ManageCompany.xaml
    /// </summary>
    public partial class ManageCompany : Window
    {
        private int ID = -1;
        public ManageCompany()
        {
            InitializeComponent();
            LoadInfo();
        }

        private void LoadInfo()
        {
            SqlCompany sqlCompany = new SqlCompany();
            List<Company> comps = sqlCompany.GetCompany();
            if (comps.Count > 0)
            {
                Company comp = comps[0];
                txtName.Text = comp.Name;
                txtNumber.Text = comp.Number;
                txtEmail.Text = comp.Email;
                lblLogo.Content = comp.Logo;
                txtStreetName.Text = comp.Street;
                txtStreetNumber.Text = comp.StreetNumber;
                txtSuburb.Text = comp.Suburb;
                txtCity.Text = comp.City;
                ID = comp.ID;
                txtBankName.Text = comp.BankName;
                txtBankAcc.Text = comp.BankNumber;
                txtBankAccType.Text = comp.BankType;
                txtBankCode.Text = comp.BankBranchCode;
                txtMessage.Text = comp.Message;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlCompany sqlCompany = new SqlCompany();
            if (ID == -1)
            {
                sqlCompany.InsertCompany(getComp());
            }
            else
            {
                sqlCompany.UpdateCompany(getComp());
            }
            MessageBox.Show("Info Updated");
            this.Close();
        }

        private Company getComp()
        {
            Company comp = new Company();
            comp.Name = txtName.Text;
            comp.Number = txtNumber.Text;
            comp.Email = txtEmail.Text;
            comp.Logo = (string)lblLogo.Content;
            comp.Street = txtStreetName.Text;
            comp.StreetNumber= txtStreetNumber.Text;
            comp.Suburb = txtSuburb.Text;
            comp.City = txtCity.Text;
            comp.ID = ID;
            comp.BankName = txtBankName.Text;
            comp.BankNumber = txtBankAcc.Text;
            comp.BankType = txtBankAccType.Text;
            comp.BankBranchCode = txtBankCode.Text;
            comp.Message = txtMessage.Text;
            return comp;
        }

        private void BtnGetLogo_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = GetImage();
            if (file == null) return;
            lblLogo.Content = file.FullName;
        }

        public FileInfo GetImage()
        {
            FileInfo fileInfo = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.JPEG) | *.jpg; *.jpeg; *.JPEG";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    fileInfo = new FileInfo(filename);
                }
            }
            else
            {
                return null;
            }
            return fileInfo;
        }
    }
}
