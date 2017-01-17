using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Common.Exception
{
    public class CommonException : System.Exception
    {
        public string Message { get; set; }

        public long ExceptionCode { get; set; }

        public CommonException(ExceptionCode exceptionCode)
        {
            this.Message = exceptionCode.Message;
            this.ExceptionCode = exceptionCode.Code;
        }
    }
}
