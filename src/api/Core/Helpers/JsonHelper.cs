using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Portal.Apis.Core.Helpers
{
    public class JsonHelper
    {
        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
        }
    }
}