using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.Models
{
    public class Client
    {
        public string ClientID { get; set; }
        public string Address { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Number { get; set; } = "";
        public string Email { get; set; } = "";
        public string DisplayName { get; set; } = "";
        private void SetDisplayname()
        {
            DisplayName = FirstName + " " + Surname;
        }
    }
}
