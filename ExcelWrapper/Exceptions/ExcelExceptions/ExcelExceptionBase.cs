using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWrapper.Exceptions.ExcelExceptions
{
    public class ExcelExceptionBase : Exception
    {
        public ExcelExceptionBase(string message) : base(message) { }
        
    }
}
