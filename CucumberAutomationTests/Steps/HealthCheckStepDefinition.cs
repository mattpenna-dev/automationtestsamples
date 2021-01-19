using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/HealthCheck.feature")]
    public sealed class HealthCheckStepDefinition : CommonStepDefinition
    {
        private readonly HttpClient _httpClient = new HttpClient();

        [When(@"I make a call to the health check endpoint")]
        public async Task WhenIMakeACallToTheHealthCheckEndpoint()
        {
            // var response = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/actuator/health");
            var response = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/actuator/health");
            AddObject(KeyNameHelpers.HttpResponseString, response);
        }
    }
}