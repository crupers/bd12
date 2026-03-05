using bd12.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bd12.Validations
{
    public class ValidEmail : ValidationRule
    {
        public int? CurrentUserId { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var email = (value ?? "").ToString().Trim();
            if (string.IsNullOrEmpty(email))
                return new ValidationResult(false, "Email обязателен");
            if (email.StartsWith("@"))
                return new ValidationResult(false, "Email не может начинаться с '@'");

            if (!email.Contains("@"))
                return new ValidationResult(false, "Email должен содержать символ '@'");

            var parts = email.Split('@');


         



           
            try
            {
                var service = new UserService();

                if (!service.IsEmailUnique(email))

                    return new ValidationResult(false, "Email уже используется");

            }
            catch { }

            return ValidationResult.ValidResult;
        }
    }
}