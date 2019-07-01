using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Street { get; set; } = "";
        public string StreetNumber { get; set; } = "";
        public string Suburb { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string Name { get; set; } = "";
        public string Number { get; set; } = "";
        public string Email { get; set; } = "";
        public string Logo { get; set; } = "";
        public string BankName { get; set; } = "";
        public string BankNumber { get; set; } = "";
        public string BankType { get; set; } = "";
        public string BankBranchCode { get; set; } = "";
        public string Message { get; set; } = "";

    }
}
