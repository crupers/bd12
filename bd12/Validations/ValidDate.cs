using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bd12.Validations
{
    public class ValidDate : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Дата обязательна");

            DateTime selectedDate;

          
            if (value is DateTime date)
            {
                selectedDate = date;
            }
            else if (value is string strDate && DateTime.TryParse(strDate, out date))
            {
                selectedDate = date;
            }
            else
            {
                return new ValidationResult(false, "Некорректный формат даты");
            }

           
            DateTime currentDate = DateTime.Today;

            if (selectedDate.Date < currentDate)
            {
                return new ValidationResult(false, $"Дата создания не может быть ранее {currentDate:dd.MM.yyyy}");
            }

         
           

            return ValidationResult.ValidResult;
        }
    }
}