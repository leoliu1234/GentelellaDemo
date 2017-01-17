using Gentelella.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Interface
{
    public interface IUserLogic
    {
        bool Login(PosCredential credentail);

        bool Register(PosCredential credential);
    }
}
