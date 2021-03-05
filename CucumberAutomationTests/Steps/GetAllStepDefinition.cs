using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Car;
using CucumberAutomationTests.Models.Manufacturer;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/Create.feature")]
    public class GetAllStepDefinition : CommonStepDefinition
    {
        private readonly HttpClient _httpClient;

        public GetAllStepDefinition()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"Given Several cars exists")]
        public async Task GivenSeveralCarsExists()
        {
            var manufacturer = (Manufacturer)GetObject(KeyNameHelpers.ExistingManufacturerKeyString);
            var Car = (Car)GetObject(KeyNameHelpers.CarServiceKeyString);

            var cars = new ArrayList()
            {
                new Car()
                {
                carType = "COMPACT",
                description = "compact car Honda",
                manufacturerId = manufacturer.id,
                name = "Accord"
                },
                new Car()
                {
                carType = "Sports",
                description = "Ford",
                manufacturerId = manufacturer.id,
                name = "Mustang"
                },
                new Car()
                {
                carType = "Sports",
                description = "Ford",
                manufacturerId = manufacturer.id,
                name = "Mustang"
                }
            };

            List<string> createList = new List<string>();
     
            for (int i = 0; i <= 3; i++)
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(cars[i]));
                var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

                if (!result.IsSuccessStatusCode)
                {
                    throw new CarCouldNotBeCreatedException("Error Creating Car.");
                }

                var responseText = await result.Content.ReadAsStringAsync();
                var createdCars = JsonConvert.DeserializeObject<Car>(responseText);
                AddObject(KeyNameHelpers.ExistingManufacturerKeyString, createList);
            }
        }

        [When(@"When I make a call to get all cars")]
        public async Task WhenIMakeACalltoGetAllCars()

        {
            List<string> getList = new List<string>();
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car");

            var responseText = await result.Content.ReadAsStringAsync();
            var getCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, getList);

        } 



    }
}
