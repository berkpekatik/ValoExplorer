using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValoExplorer.Models
{
    public class RiotLockFileModel
    {
        public int Pid { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Protocol { get; set; }
        public string Name { get; set; }
    }
}
