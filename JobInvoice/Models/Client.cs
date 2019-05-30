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
        public string SurName { get; set; } = "";
        public string DisplayName { get; set; } = "";

        private void SetDisplayname()
        {
            DisplayName = FirstName + " " + SurName;
        }
    }
}
