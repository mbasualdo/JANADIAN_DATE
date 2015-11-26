using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Excepciones
{
    public class CancelException : ApplicationException
    {
        public CancelException(string message)
            : base(message)
        {
        }
    }
}
