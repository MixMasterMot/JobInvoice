using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoice.SQLFunctions
{
    class SqlConString
    {
        public string MasterConnectionString {
            get;
        } = ConfigurationManager.ConnectionStrings["JobInvoice.Properties.Settings.DatabaseConnectionString"].ConnectionString;

    }
}
