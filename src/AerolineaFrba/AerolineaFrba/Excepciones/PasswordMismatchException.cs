using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Excepciones
{
    public class PasswordMismatchException : ApplicationException
    {
        public PasswordMismatchException(string message)
            : base(message)
        {
        }
    }
}
