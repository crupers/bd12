using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bd12.Validations
{
    public class ValidLogin : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new System.Windows.Controls.ValidationResult(false, "Ввод логина обязателен");
            }

            if (input.Length < 5)
            {
                return new System.Windows.Controls.ValidationResult(false, "Логин должен состоять минимум из 5 символов!");
            }
            return System.Windows.Controls.ValidationResult.ValidResult;
        }

    }
}
