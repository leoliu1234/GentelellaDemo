using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Common.Exception
{
    public class ExceptionCode
    {
        public long Code { get; set; }
        public string Message { get; set; }

        public object Object { get; set; }

        public ExceptionCode(long code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }

    public static class ExceptionCodes
    {
        public static ExceptionCode CanNotFoundUser
        {
            get
            {
                return new ExceptionCode(-101, "Can't found the User");
            }
        }

        public static ExceptionCode UserExisted
        {
            get
            {
                return new ExceptionCode(-102, "The user have been exists");
            }
        }
    }
}
