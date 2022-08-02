using CCTPAPP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CCTPAPP.Resources
{
    public class ApiServices
    {
        protected string ApiBaseUrl = "https://cef4-2806-10be-8-1d99-a0e0-6cdf-ce35-5266.ngrok.io/Service/";
        //protected string ApiBaseUrl = "https://pagalaescuela.mx/Service/";

        public enum ResponseType
        {
            Object,
            Objects,
            List
        }

        public ApiServices(string controller)
        {
            this.ApiBaseUrl += controller + "/";
        }

        public async Task<Response> POST<T>(string action, ResponseType responseType, Dictionary<string, string> parameters = null)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(this.ApiBaseUrl);

                string content = "";

                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        if (content == string.Empty)
                        {
                            content = "\"" + item.Key + "\":\"" + item.Value + "\"";
                        }
                        else
                        {
                            content += ",\"" + item.Key + "\":\"" + item.Value + "\"";
                        }
                    }
                }

                var response = await client.PostAsync(action,
                    new StringContent(
                    "{" + content + "}",
                    Encoding.UTF8, "application/json"));

                var jsonResult = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(jsonResult);
                string jResult = jObject.ToString();

                if (responseType == ResponseType.Object)
                {
                    var objectResult = JsonConvert.DeserializeObject<Response>(jResult);
                    var resultObject = new object();
                    if (objectResult.IsSuccessValue != true)
                    {
                        resultObject = JsonConvert.DeserializeObject<T>(objectResult.Result.ToString());
                    }


                    if (objectResult.IsSuccessValue != false)
                    {
                        return new Response
                        {
                            IsSuccess = objectResult.IsSuccess,
                            Message = objectResult.Message,
                            Result = null
                        };
                    }
                    else
                    {
                        return new Response()
                        {
                            IsSuccess = objectResult.IsSuccess,
                            Message = objectResult.Message,
                            Result = resultObject
                        };
                    }


                }
                else if (responseType == ResponseType.Objects)
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }
                else if (responseType == ResponseType.List)
                {
                    var objectResult = JsonConvert.DeserializeObject<Response>(jResult);
                    var resultListObject = new List<T>();
                    if (objectResult.IsSuccess)
                        resultListObject = JsonConvert.DeserializeObject<List<T>>(objectResult.Result.ToString());


                    if (objectResult.IsSuccessValue)
                    {
                        return new Response()
                        {
                            IsSuccess = objectResult.IsSuccess,
                            Message = objectResult.Message,
                            Result = resultListObject
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = objectResult.IsSuccess,
                            Message = objectResult.Message,
                            Result = null
                        };
                    }

                }
                else
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GET<T>(string action, ResponseType responseType, Dictionary<string, string> arguments = null)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(this.ApiBaseUrl);

                string urlArguments = string.Empty;

                if (arguments != null)
                {
                    foreach (var arg in arguments)
                    {
                        if (urlArguments == string.Empty)
                            urlArguments = arg.Value == string.Empty ? string.Empty : $"?{arg.Key}={arg.Value}";
                        else
                            urlArguments += arg.Value == string.Empty ? string.Empty : $"&{arg.Key}={arg.Value}";
                    }
                }

                var response = await client.GetAsync(action + urlArguments);
                var jsonResult = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(jsonResult);
                string jResult = jObject.ToString();

                Response result = new Response()
                {
                    IsSuccess = false
                };

                if (responseType == ResponseType.Object)
                {
                    var objectResult = JsonConvert.DeserializeObject<Response>(jResult);

                    result.IsSuccess = objectResult.IsSuccess;
                    result.Message = objectResult.Message;

                    if (objectResult.IsSuccess)
                        result.Result = JsonConvert.DeserializeObject<T>(objectResult.Result.ToString());
                }
                else
                {
                    var objectResult = JsonConvert.DeserializeObject<Response>(jResult);

                    result.IsSuccess = objectResult.IsSuccess;
                    result.Message = objectResult.Message;

                    if (objectResult.IsSuccess)
                        result.Result = JsonConvert.DeserializeObject<List<T>>(objectResult.Result.ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
