using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Car;
using CucumberAutomationTests.Models.Manufacturer;
using Newtonsoft.Json;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/Create.feature")]
    public class CreateStepDefinition : CommonStepDefinition
    {
        private readonly HttpClient _httpClient;

        public CreateStepDefinition()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"A manufacturer exists")]
        public async Task GivenManufacturerExists()
        {
            var manufacturer = new Manufacturer
            {
                name = $"{Guid.NewGuid()}-Teslas"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(manufacturer));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.MaunfacturerServiceKeyString)}/manufacturer", httpContent);
            
            if (!result.IsSuccessStatusCode)
            {
                throw new ManufacturerCouldNotBeCreatedException("Error Creating Manufacturer.");
            }
            
            var responseText = await result.Content.ReadAsStringAsync();
            var createdManufacturer = JsonConvert.DeserializeObject<Manufacturer>(responseText);
            AddObject(KeyNameHelpers.ExistingManufacturerKeyString, createdManufacturer);
        }

        [When(@"I make a call to create a car")]
        public async Task WhenIMakeACallToCreateCar()
        {
            var manufacturer = (Manufacturer) GetObject(KeyNameHelpers.ExistingManufacturerKeyString);

            var car = new Car
            {
                carType = "COMPACT",
                description = "This is an ModelS",
                manufacturerId = manufacturer.id,
                name = "ModelS"
            };
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);
            
            if (!result.IsSuccessStatusCode)
            {
                throw new CarCouldNotBeCreatedException("Error Creating Car.");
            }
            
            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }

        [And(@"I should see the car was created")]
        public async Task IShouldSeeTheCarWasCreated()
        {
            
        }

        [And(@"I should see the manufacturer has been updated with the new car")]
        public async Task IShouldSeeTheManufactuererHasBeenUpdatedWithNewCar()
        {
            
        }
    }
}