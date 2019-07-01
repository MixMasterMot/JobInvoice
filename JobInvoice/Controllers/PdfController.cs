using JobInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInvoice.SQLFunctions;
using JobInvoice.PDFWriter;

namespace JobInvoice.Controllers
{
    public class PdfController
    {
        public (bool,string) GeneratePDF(Job job)
        {
            CreatePDF createPDF = new CreatePDF();
            SqlClient sqlClient = new SqlClient();
            Client client = sqlClient.GetClient(job.ClientID);
            SqlCompany sqlCompany = new SqlCompany();
            List<Company> compInfo = sqlCompany.GetCompany();
            if (compInfo.Count >= 0)
            {
                return (false, "Company info not entered!!!!!!!!!!!!");
            }
            Company company = compInfo[0];
            return (createPDF.Create(job, company, client), null);
        }
    }
}
