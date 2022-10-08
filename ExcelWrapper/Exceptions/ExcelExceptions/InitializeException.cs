using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWrapper.Exceptions.ExcelExceptions
{
    internal class InitializeException : ExcelExceptionBase
    {
        public InitializeException(string message) : base(message) { }
       
    }
}
