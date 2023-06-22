using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagment.App.Service.Interface
{
    public interface IValidation
    {
        void CheckPersonalNumberFormat(string personalNumber);
    }
}
