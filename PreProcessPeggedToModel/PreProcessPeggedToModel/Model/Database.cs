using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessPeggedToModel.Model
{
    public class Database
    {
        public string Catalog { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
