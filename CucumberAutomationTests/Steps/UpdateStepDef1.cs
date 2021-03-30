using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CucumberAutomationTests.Clients;
using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Car;
using CucumberAutomationTests.Models.Manufacturer;
using CucumberAutomationTests.Models.Wiremock;
using Newtonsoft.Json;
using Xunit;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    //test
    [FeatureFile("./Features/Update.feature")]
    public sealed class UpdateStepDef1 : CommonStepDefinition
    {
        private readonly HttpClient _httpClient;

        public UpdateStepDef1()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"A Car exists")]
        public async Task GivenCarExists()
        {
            await UpdateCarAsync();
        }


    }
