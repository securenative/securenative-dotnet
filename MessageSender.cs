using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SecureNative.SDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SecureNative.SDK
{
    public class MessageSender<T>: IMessageSender<T>
    {
        private WebClient _webClient = new WebClient();
        private string _authorization;

        public MessageSender(string authorization)
        {
            _authorization = authorization;
        }

        public string Post(string uri, T messsage)
        {

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var jsonMessage = JsonConvert.SerializeObject(messsage, serializerSettings);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Timeout = 1500;
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, _authorization);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(jsonMessage);
            }


            try
            {
                var httpResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (WebException e)
            {

                return "{}";

            }
        }

    }
}
