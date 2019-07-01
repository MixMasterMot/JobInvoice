using Invoicer.Models;
using Invoicer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.Models;
using Microsoft.Win32;

namespace JobInvoice.PDFWriter
{
    public class CreatePDF
    {
        public decimal vatAmount = 0;
        public decimal total = 0;
        public bool Create(Job job, Company company, Client client)
        {
            (bool pass, string FileName) = SaveDial();
            if (!pass)
            {
                return false;
            }
            new InvoicerApi(SizeOption.A4, OrientationOption.Portrait, "R")
                .TextColor("#CC0000")
                .BackColor("#FFD6CC")
                .BillingDate(job.DateOut)
                .Reference(job.JobID.ToString())
                .Image(company.Logo, 215, 27)
                .Company(GetCompAddress(company))
                .Client(GetClientAddress(client))
                .Items(GetItems(job))
                .Totals(GetTotals(job))
                .Details(GetDetails(company))
                .Save(FileName);
            return true;
        }
       
        private (bool, string) SaveDial()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                string name = saveFileDialog.FileName;
                return (true, name);
            }
            return (false, "");
        }
        private Address GetCompAddress(Company comp)
        {
            return Address.Make("FROM",
                    new string[] {comp.Name, comp.Street +" " + comp.StreetNumber,
                    comp.Suburb, comp.City, comp.Country, comp.PostalCode},
                    comp.Name);
        }
        private Address GetClientAddress(Client client)
        {
            return Address.Make("BILLING TO",
                new string[] {client.DisplayName, client.Street +" " + client.StreetNumber,
                    client.Suburb, client.City, client.Country, client.PostalCode});
        }

        private List<ItemRow> GetItems(Job job)
        {
            List<ItemRow> rows = new List<ItemRow>();
            rows.Add(GetLabourCost(job));
            rows.AddRange(GetStockItems(job.StockItems));
            return rows;
        }
        private ItemRow GetLabourCost(Job job)
        {
            return ItemRow.Make("Labour","",job.CompletionTime,0,job.HourRate,job.LabourCost);
        }
        private List<ItemRow> GetStockItems(List<UsedStockItem> StockItems)
        {
            List<ItemRow> rows = new List<ItemRow>();
            foreach(UsedStockItem item in StockItems)
            {
                total = total + item.Price;
                vatAmount = vatAmount + item.GetVatAmount();
                decimal vat = 0;
                if (item.Vat)
                {
                    vat = 15;
                }
                decimal amount = item.Height * item.Width;
                rows.Add(ItemRow.Make(item.Name, "", amount, vat, item.UnitPrice, item.Price));
            }
            return rows;
        }
        private List<TotalRow> GetTotals(Job job)
        {
            List<TotalRow> rows = new List<TotalRow>();
            rows.Add(TotalRow.Make("Total exc Vat", total));
            rows.Add(TotalRow.Make("Total Vat", vatAmount));
            rows.Add(TotalRow.Make("Total", total + vatAmount));
            return rows;
        }
        private List<DetailRow> GetDetails(Company company)
        {
            List<DetailRow> rows = new List<DetailRow>();
            rows.Add(DetailRow.Make(
                    "Payment Information",
                    company.BankName,
                    company.BankNumber,
                    company.BankType,
                    company.BankBranchCode,
                    company.Message));
            return rows;
        }

    }
}
