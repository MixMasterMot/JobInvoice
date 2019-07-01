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
    public class UsedStockItemValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            UsedStockItem item = (UsedStockItem)(value as BindingGroup).Items[0];
            if (string.IsNullOrEmpty(item.Name))
            {
                return new ValidationResult(false, "Select a valid material");
            }
            else if(item.Price < 0)
            {
                return new ValidationResult(false, "Price must be >= 0");
            }
            else if (item.Width<0)
            {
                return new ValidationResult(false, "Width must be >= 0");
            }
            else if (item.Height < 0)
            {
                return new ValidationResult(false, "Height must be >= 0");
            }
            else if (item.UnitPrice<0)
            {
                return new ValidationResult(false, "Unit Price must be >= 0");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}