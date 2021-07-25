using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ValoExplorer.Models;

namespace ValoExplorer.Services
{
    public class RiotApiService
    {
        private static RiotLockFileModel RiotLockFile;
        private static string api = @"/swagger/v2/swagger.json";
        RestClient client;
        public void StartSetting()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            using (var fs = new FileStream(local + @"\Riot Games\Riot Client\Config\lockfile", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                RiotLockFileParse(sr.ReadLine());
                sr.Close();
                fs.Close();
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
             (se, cert, chain, sslerror) =>
             {
                 return true;
             };
        }
        private void RiotLockFileParse(string data)
        {
            var split = data.Split(':');
            RiotLockFile = new RiotLockFileModel
            {
                Name = split[0],
                Pid = int.Parse(split[1]),
                Port = int.Parse(split[2]),
                Password = split[3],
                Protocol = split[4],
            };
        }
        private string DownloadData()
        {
            client = new RestClient($"{RiotLockFile.Protocol}://127.0.0.1:{RiotLockFile.Port}{api}");
            client.Timeout = -1;
            client.Authenticator = new HttpBasicAuthenticator("riot", RiotLockFile.Password);

            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            return response.Content;
        }

        public List<RiotApiReaderModel> GetApi()
        {
            var data = JsonConvert.DeserializeObject<PathsApi>(DownloadData());
            var model = new List<RiotApiReaderModel>();
            foreach (var item in data.Paths.Children())
            {
                var realBody = new List<RequestBodyx>();
                var req = JsonConvert.DeserializeObject<RequestTypeModel>(item.First.ToString());
                realBody.Add(new RequestBodyx { 
                ReqName = "GET",
                ReqBody = req.Get,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "POST",
                ReqBody = req.Post,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "PUT",
                ReqBody = req.Put,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "PATCH",
                ReqBody = req.Patch,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "DELETE",
                ReqBody = req.Delete,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "COPY",
                ReqBody = req.Copy,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "HEAD",
                ReqBody = req.Head,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "OPTIONS",
                ReqBody = req.Options,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "LINK",
                ReqBody = req.Link,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "UNLINK",
                ReqBody = req.Unlink,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "PURGE",
                ReqBody = req.Purge,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "LOCK",
                ReqBody = req.Lock,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "PROPFIND",
                ReqBody = req.Propfind,
                });
                realBody.Add(new RequestBodyx { 
                ReqName = "VIEW",
                ReqBody = req.View,
                });
                realBody = realBody.Where(x => x.ReqBody != null).ToList();
                model.Add(new RiotApiReaderModel
                {
                    Body = realBody,
                    Name = item.Path.Replace("'", "").Replace("[", "").Replace("]", "")
                });
            }
            return model;
        }
        public partial class LoginModel
        {
            [JsonProperty("persist")]
            public bool Persist { get; set; }

            [JsonProperty("phase")]
            public string Phase { get; set; }
        }
        public bool ConfirmLogin()
        {
            StartSetting();
            client = new RestClient($@"{RiotLockFile.Protocol}://127.0.0.1:{RiotLockFile.Port}/riot-login/v1/status");
            client.Timeout = -1;
            client.Authenticator = new HttpBasicAuthenticator("riot", RiotLockFile.Password);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<LoginModel>(response.Content).Phase == "logged_in" ? true : false;
        }
    }
}
