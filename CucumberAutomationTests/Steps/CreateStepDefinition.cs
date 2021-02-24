using System;
using System.Net.Http;
using System.Threading.Tasks;
using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Manufacturer;
using Newtonsoft.Json;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/Create.feature")]
    public class CreateStepDefinition : CommonStepDefinition
    {
        private readonly HttpClient _httpClient = new HttpClient();
        
        [Given(@"A manufacturer exists")]
        public async Task GivenManufacturerExists()
        {
            var manufacturer = new Manufacturer
            {
                name = $"{Guid.NewGuid()}-Teslas"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(manufacturer));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/manufacturer", httpContent);
            
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