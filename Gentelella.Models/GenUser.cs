using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Models
{
    public class GenUser
    {
        public long Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}
