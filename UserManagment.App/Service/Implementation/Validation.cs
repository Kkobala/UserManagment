using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserManagment.App.Service.Interface;

namespace UserManagment.App.Service.Implementation
{
    public class Validation: IValidation
    {
        public void CheckPersonalNumberFormat(string personalNumber)
        {
            if (Regex.IsMatch(personalNumber, @"[A-Za-z]") || Regex.IsMatch(personalNumber, @"^(?=.*[\W_]).+$"))
            {
                throw new Exception("Private Number must contain only numbers");
            }

            if (!Regex.IsMatch(personalNumber, @"^(?=.*[0-9]).+$") || personalNumber.Length != 11)
            {
                throw new Exception("Invalid Private number format");
            }
        }
    }
}
