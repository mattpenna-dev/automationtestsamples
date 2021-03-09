using System;
using System.Collections;
using System.Collections.Generic;
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
            var manufacturer = await CreateManufacturerAsync();

            var createList = new List<Car>();

            for (var i = 0; i <= 3; i++)
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(new Car
                {
                    carType = "Electric",
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

        [When(@"When I make a call to get all cars")]
        public async Task WhenIMakeACallToGetAllCars()
        {
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car");
            AddObject(KeyNameHelpers.HttpResponseString, result);
        }

        [And(@"And I should see all cars are returned in the list")]
        public async Task ReturnedAllCarsInList()
        {
            var result = (HttpResponseMessage)GetObject(KeyNameHelpers.HttpResponseString);
            var responseText = await result.Content.ReadAsStringAsync();

            var actualCars = JsonConvert.DeserializeObject<List<Car>>(responseText);
            var expectedCars = (List<Car>)GetObject(KeyNameHelpers.ExistingCarListString);

            Assert.Equal(expectedCars.Count, actualCars.Count);

            foreach (var expectedCar in expectedCars)
            {
                var isFound = false;

                foreach (var actualCar in actualCars.Where(actualCar => expectedCar.id.Equals(actualCar.id)))
                {
                    isFound = true;

                    Assert.NotNull(actualCar.createdOn);
                    Assert.NotNull(actualCar.updatedOn);
                    Assert.Equal(expectedCar.name, actualCar.name);
                    Assert.Equal(expectedCar.id, actualCar.id);
                    Assert.Equal(expectedCar.carType, actualCar.carType);
                    Assert.Equal(expectedCar.description, actualCar.description);
                    Assert.Equal(expectedCar.manufacturerId, actualCar.manufacturerId);
                }

                Assert.True(isFound);
            }
        }
    }
}

