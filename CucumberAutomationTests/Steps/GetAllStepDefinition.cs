using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Car;
using CucumberAutomationTests.Models.Manufacturer;
using Newtonsoft.Json;
using Xunit;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/GetAll.feature")]
    public class GetAllStepDefinition : CommonStepDefinition
    {
        private readonly HttpClient _httpClient;

        public GetAllStepDefinition()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"Several cars exists")]
        public async Task GivenSeveralCarsExists()
        {
            var manufacturer = (Manufacturer)GetObject(KeyNameHelpers.CreatedCarKeyString);

            //var cars = new ArrayList()

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CreatedCarKeyString)}/car");

            if (!result.IsSuccessStatusCode)
            {
                throw new CarCouldNotBeCreatedException("Error Creating Car");
            }

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.ExistingManufacturerKeyString, createdCar);
        }
    }
}
