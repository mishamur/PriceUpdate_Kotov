using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWrapper.Exceptions.ExcelExceptions
{
    public class SaveFileException : ExcelExceptionBase
    {
        public SaveFileException(string message) : base(message) { }
        
    }
}
