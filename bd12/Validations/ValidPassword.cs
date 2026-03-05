using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bd12.Validations
{
    public class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var password = (value ?? "").ToString();

        
            if (string.IsNullOrEmpty(password))
                return new ValidationResult(false, "Пароль обязателен");

          
            if (password.Length < 8)
                return new ValidationResult(false, "Пароль должен содержать не менее 8 символов");

        
            if (!password.Any(char.IsDigit))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну цифру");

           
            if (!password.Any(char.IsLower))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну строчную букву");

           
            if (!password.Any(char.IsUpper))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну заглавную букву");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return new ValidationResult(false, "Пароль должен содержать хотя бы один специальный символ");
            if (password.Contains(" "))
                return new ValidationResult(false, "Пароль не должен содержать пробелов");

            return ValidationResult.ValidResult;
        }
    }
}