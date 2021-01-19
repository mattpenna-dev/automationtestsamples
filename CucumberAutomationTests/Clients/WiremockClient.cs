using System;
using System.Net.Http;
using System.Threading.Tasks;
using CucumberAutomationTests.Models.Wiremock;
using Newtonsoft.Json;

namespace CucumberAutomationTests.Clients
{
    public class WiremockClient
    {
        private HttpClient _httpClient;

        public WiremockClient(string baseUrl)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task MockEndpointAsync(Mappings mappings)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(mappings));

            var result = await _httpClient.PostAsync("/__admin/mappings/new", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error setting wiremock endpoint.");
            }
        }
        
        public async Task ResetMappingsAsync()
        {
            var result = await _httpClient.PostAsync("/__admin/reset", null);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error setting wiremock mock endpoint.");
            }
        }

        public async Task<VerifyResponse> VerifyRequestCountAsync(VerifyRequest verifyRequest)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(verifyRequest));
            var result = await _httpClient.PostAsync("/__admin/requests/count", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error verifying mock endpoint.");
            }
            
            var responseText = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VerifyResponse>(responseText);
        }
    }
}