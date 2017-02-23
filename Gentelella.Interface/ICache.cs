using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Interface
{
    public interface ICache
    {
        void Add<T>(string key, T source);

        T Get<T>(string key);
    }
}
