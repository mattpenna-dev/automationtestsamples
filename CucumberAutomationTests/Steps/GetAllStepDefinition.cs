using CucumberAutomationTests.Models.Car;
using CucumberAutomationTests.Models.Manufacturer;
using System;
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

            var car = new Car
            {
                carType = "COMPACT",
                description = "compact car Honda",
                manufacturerId = manufacturer.id,
                name = "Accord"
            };

            var car2 = new Car
            {
                carType = "Sports",
                description = "Ford",
                manufacturerId = manufacturer.id,
                name = "Mustang"
            };

            var car3 = new Car
            {
                carType = "COMPACT",
                description = "compact car Honda",
                manufacturerId = manufacturer.id,
                name = "Accord"
            };


        }




    }
}
