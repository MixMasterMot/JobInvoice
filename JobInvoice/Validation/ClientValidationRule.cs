using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using JobInvoice.Models;

namespace JobInvoice.Validation
{
    public class ClientValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Client client = (Client)(value as BindingGroup).Items[0];
            if (string.IsNullOrWhiteSpace(client.ClientID))
            {
                return new ValidationResult(false, "Please enter a unique Client ID");
            }
            if (client.ClientID.Contains(" "))
            {
                return new ValidationResult(false, "Client ID can not contain a white space");
            }
            else if (string.IsNullOrWhiteSpace(client.FirstName))
            {
                return new ValidationResult(false, "Please enter a First Name");
            }
            return ValidationResult.ValidResult;
        }
    }
}
