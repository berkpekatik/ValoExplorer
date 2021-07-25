using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoExplorer.Models;

namespace ValoExplorer.Services
{
    public class RiotClientService
    {
        private readonly RiotApiService service = new RiotApiService();
        public bool RiotServiceIsRunning()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Riot Games\Riot Client\Config\lockfile"))
            {
                var riot = Process.GetProcessesByName("RiotClientServices").FirstOrDefault();
                if (riot != null)
                {
                    var valo = Process.GetProcessesByName("VALORANT").FirstOrDefault();
                    if (valo != null)
                    {
                        if (service.ConfirmLogin())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public List<RiotApiReaderModel> StartListing()
        {
            return service.GetApi();
        }
    }
}

