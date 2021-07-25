using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValoExplorer.Models
{
    public class RequestBodyx
    {
        public string ReqName { get; set; }
        public Request ReqBody { get; set; }
    }
    public class RiotApiReaderModel
    {
        public string Name { get; set; }
        public List<RequestBodyx> Body { get; set; }
    }
    public class PathsApi
    {
        [JsonProperty("basePath")]
        public string BasePath { get; set; }

        [JsonProperty("consumes")]
        public List<string> Consumes { get; set; }

        [JsonProperty("definitions")]
        public object Definitions { get; set; }

        [JsonProperty("info")]
        public object Info { get; set; }

        [JsonProperty("paths")]
        public JObject Paths { get; set; }

        [JsonProperty("produces")]
        public List<string> Produces { get; set; }

        [JsonProperty("swagger")]
        public string Swagger { get; set; }
    }


    public partial class Request
    {
        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }

        [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class Parameter
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("in")]
        public string In { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("required")]
        public bool ParameterRequired { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Enum { get; set; }

        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public string Format { get; set; }
    }


    public class RequestTypeModel
    {
        [JsonProperty("get")]
        public Request Get { get; set; }

        [JsonProperty("post")]
        public Request Post { get; set; }

        [JsonProperty("put")]
        public Request Put { get; set; }

        [JsonProperty("patch")]
        public Request Patch { get; set; }

        [JsonProperty("delete")]
        public Request Delete { get; set; }

        [JsonProperty("copy")]
        public Request Copy { get; set; }

        [JsonProperty("head")]
        public Request Head { get; set; }

        [JsonProperty("options")]
        public Request Options { get; set; }

        [JsonProperty("link")]
        public Request Link { get; set; }

        [JsonProperty("unlink")]
        public Request Unlink { get; set; }

        [JsonProperty("purge")]
        public Request Purge { get; set; }

        [JsonProperty("lock")]
        public Request Lock { get; set; }

        [JsonProperty("propfind")]
        public Request Propfind { get; set; }

        [JsonProperty("view")]
        public Request View { get; set; }

    }
}
