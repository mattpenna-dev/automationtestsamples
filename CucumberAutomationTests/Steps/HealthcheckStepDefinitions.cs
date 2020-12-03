using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/Healthcheck.feature")]
    public sealed class HealthcheckStepDefinition : CommonStepDefinitions
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private HttpResponseMessage _httpResponseMessage;

        [When(@"When I make a Get Car Service Health check to the Car Service")]
        public async Task CallToTheHealthCheckEndpoint()
        {
            _httpResponseMessage = await _httpClient.GetAsync("https://ci-car-service.mattpenna.dev/actuator/health");
            AddObject("HttpResponse", _httpResponseMessage);
        }
    }
}
}
