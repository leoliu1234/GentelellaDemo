using Gentelella.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gentelella.Common.Utilities
{
    public class Cache : ICache
    {
        public void Add<T>(string key, T source)
        {
            HttpContext.Current.Session[key] = source;
        }

        public T Get<T>(string key)
        {
            var value = HttpContext.Current.Session[key];
            if (value == null)
            {
                // TODO:
                // throw exception
                if (IsNumberic<T>())
                {
                    return (T)Convert.ChangeType(-1, typeof(T));
                }

                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        private bool IsNumberic<T>()
        {
            return (typeof(T) == typeof(Int64) ||
                    typeof(T) == typeof(Int32) ||
                    typeof(T) == typeof(Decimal));
        }
    }
}
