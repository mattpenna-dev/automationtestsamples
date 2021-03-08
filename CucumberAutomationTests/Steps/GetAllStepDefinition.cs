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
            var manufacturer = await CreateManufacturerAsync;

            var createList = new List<Car>();

            for (var i = 0; i <= 3; i++)
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(new Car
                {
                    carType = "COMPACT",
                    manufacturerId = manufacturer.id,
                    name = Guid.NewGuid() + "-ModelS"
                }));
                var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car",
                    httpContent);

                if (!result.IsSuccessStatusCode)
                {
                    throw new CarCouldNotBeCreatedException("Error Creating Car.");
                }

                var responseText = await result.Content.ReadAsStringAsync();
                var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
                createList.Add(createdCar);
            }

            AddObject(KeyNameHelpers.ExistingCarListString, createList);
        }
    }
}
