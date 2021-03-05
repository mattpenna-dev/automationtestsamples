﻿using CucumberAutomationTests.Exceptions;
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

            IList cars = new ArrayList()
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


            var httpContent = new StringContent(JsonConvert.SerializeObject(cars));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new CarCouldNotBeCreatedException("Error Creating Car.");
            }

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCars = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.ExistingManufacturerKeyString, createdCars);
        }

        [When(@"When I make a call to get all car")]
        public async Task WhenIMakeACalltoGetAllCars()
        {
            var manufacturer = (Manufacturer)GetObject(KeyNameHelpers.ExistingManufacturerKeyString);
            var car = (Car)GetObject(KeyNameHelpers.CarServiceKeyString);

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var getCars = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, getCars);
        }



    }
}
