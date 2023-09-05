using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Operations
{
    public abstract class ApiOperationsBase
    {
        protected static readonly string _port = "7093";
        //protected static readonly string _port = "5000";
        protected static readonly string _version = "1";
        protected static readonly string _baseApiUrl = $"https://localhost:{_port}/api/{_version}/";

        protected static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected async Task<HttpResponseMessage> PostAsync(string requestUri, string jsonContent, string jwtAccessToken = null)
        {
            HttpResponseMessage response = null;

            try
            {
                using var httpClient = CreateClient(jwtAccessToken);

                StringContent content = new(jsonContent, Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync(requestUri, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReasonPhrase = ex.Message
                };
            }

            return response;
        }

        private static HttpClient CreateClient(string jwtAccessToken = null)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseApiUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtAccessToken != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtAccessToken);

            return client;
        }
    }
}
