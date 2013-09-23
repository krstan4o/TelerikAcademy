using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _04.ValidatingAndBindingPerson
{
    public class AgeValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = (int)value;
            if (age < 0 && age > 100)
            {
                return new ValidationResult(false, "Invalid age.");
            }

            return new ValidationResult(true, null);
        }
    }
}
