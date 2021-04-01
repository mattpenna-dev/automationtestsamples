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
    [FeatureFile("./Features/CreateSomeCar.feature")]
    public sealed class CreateStepDefinition : CommonStepDefinition, IDisposable
    {
        private readonly HttpClient _httpClient;

        public CreateStepDefinition()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"A manufacturer exists")]
        public async Task GivenManufacturerExists()
        {
            await CreateManufacturerAsync();
        }

        [Given(@"A mocked manufacturer exists")]
        public async Task GivenAMockedManufacturerExists()
        {
            var mockedManufactuer = new Manufacturer
            {
                id = Guid.NewGuid().ToString(),
                name = $"{Guid.NewGuid()}-Teslas",
                cars = new List<string>(),
                createdBy = "",
                updatedBy = "",
            };
            
            var wiremockClient = new WiremockClient(GetConfigValue(KeyNameHelpers.WireMockUrlKeyString));
            await wiremockClient.MockEndpointAsync(new Mappings
            {
                request = new Request
                {
                    method = "GET",
                    url = $"/manufacturer/{mockedManufactuer.id}"
                },
                response = new Response
                {
                    status = 200,
                    jsonBody = mockedManufactuer,
                    headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
                }
            });
            
            await wiremockClient.MockEndpointAsync(new Mappings
            {
                request = new Request
                {
                    method = "PATCH",
                    url = $"/manufacturer/"
                },
                response = new Response
                {
                    status = 200,
                    jsonBody = mockedManufactuer,
                    headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
                }
            });
            
            AddObject(KeyNameHelpers.ExistingManufacturerKeyString, mockedManufactuer);
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
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car/", httpContent);
            
            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }

        [When(@"I make a call to create a car with empty description")]
        public async Task WhenIMakeACallToCreateACarWithEmptyDescription()
        {
            var manufacturer = (Manufacturer) GetObject(KeyNameHelpers.ExistingManufacturerKeyString);

            var car = new Car
            {
                carType = "COMPACT",
                manufacturerId = manufacturer.id,
                name = "ModelS"
            };
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);
            
            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }
        
        [When(@"I make a call to create a car with non-existent manufacturer")]
        public async Task WhenIMakeACallToCreateCarWithNonExistentManufacturer()
        {
            var car = new Car
            {
                carType = "Tesla",
                description = "This is a Model S",
                manufacturerId = Guid.NewGuid() + "-non-existent",
                name = "ModelS"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);

        }

        [When(@"When I make a call to create a car with non null id")]
        public async Task CreateCarWithNullID()
        {
            var car = new Car
            {
                id = Guid.NewGuid().ToString(),
                carType = "Honda",
                description = "This is a Hond",
                manufacturerId = Guid.NewGuid() + "-non-existent",
                name = "Accord"

            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);

        }

        [When(@"I make a call to create a car with a null CarType")]
        public async Task NullCarType()
        {
            var car = new Car
            {
                description = "This is a Ford",
                manufacturerId = Guid.NewGuid() + "-non-existent",
                name = "Mustang"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }

        [When(@"I make a call to create a car with invalid CarType")]
        public async Task InvalidCarType()
        {
            var car = new Car
            {
                carType = "Sedan",
                description = "This is an awesome Hyundai",
                manufacturerId = Guid.NewGuid() + "-non-existent",
                name = "Sonata"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }

        [When(@"I make a call to create a car with manufacturer id null")]
        public async Task ManufacturerIDNull()
        {
            var car = new Car
            {
                carType = "Truck",
                description = "This is a cool truck",
                name = "Ford "
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(car));
            var result = await _httpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car", httpContent);

            var responseText = await result.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseText);
            AddObject(KeyNameHelpers.HttpResponseString, result);
            AddObject(KeyNameHelpers.CreatedCarKeyString, createdCar);
        }
        
        [And(@"I should see the car was created")]
        public async Task IShouldSeeTheCarWasCreated()
        {
            var createdCar = (Car) GetObject(KeyNameHelpers.CreatedCarKeyString);
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car/{createdCar.id}");
            
            if (!result.IsSuccessStatusCode)
            {
                throw new CouldNotFindCar($"Error Finding Car with id: {createdCar.id}.");
            }
            
            var responseText = await result.Content.ReadAsStringAsync();
            var actualCar = JsonConvert.DeserializeObject<Car>(responseText);
            
            Assert.NotNull(actualCar);
            Assert.NotNull(actualCar.createdOn);
            Assert.NotNull(actualCar.updatedOn);
            Assert.Equal(createdCar.name, actualCar.name);
            Assert.Equal(createdCar.id, actualCar.id);
            Assert.Equal(createdCar.carType, actualCar.carType);
            Assert.Equal(createdCar.description, actualCar.description);
            Assert.Equal(createdCar.manufacturerId, actualCar.manufacturerId);
        }

        [And(@"I should see the manufacturer has been updated with the new car")]
        public async Task IShouldSeeTheManufactuererHasBeenUpdatedWithNewCar()
        {
            var manufacturer = (Manufacturer) GetObject(KeyNameHelpers.ExistingManufacturerKeyString);
            var createdCar = (Car) GetObject(KeyNameHelpers.CreatedCarKeyString);
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.MaunfacturerServiceKeyString)}/manufacturer/{manufacturer.id}");
            
            if (!result.IsSuccessStatusCode)
            {
                throw new CouldNotFindManufacturer($"Error Finding Car with id: {manufacturer.id}.");
            }
            
            var responseText = await result.Content.ReadAsStringAsync();
            var actualManufacturer = JsonConvert.DeserializeObject<Manufacturer>(responseText);

            var isCarFound = false;
            foreach (var car in actualManufacturer.cars
                .Where(car => string.Equals(car, createdCar.id)))
            {
                isCarFound = true;
            }
            
            Assert.True(isCarFound);
        }

        [And(@"I should see the car was not created")]
        public async Task IShouldSeeTheCarWasNotCreated()
        {
            var httpResponseMessage = (HttpResponseMessage) GetObject(KeyNameHelpers.HttpResponseString);
            var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<List<ErrorResponse>>(responseText);

            var car = (Car) GetObject(KeyNameHelpers.CreatedCarKeyString);
            Assert.Equal("ManufacturerNotFound", errorResponse[0].code);
            Assert.Equal($"Manufacturer with id: {car.id} not found", errorResponse[0].message);
            
            var createdCar = (Car)GetObject(KeyNameHelpers.CreatedCarKeyString);
            var result = await _httpClient.GetAsync($"{GetConfigValue(KeyNameHelpers.CarServiceKeyString)}/car/{createdCar.id}");
            
            Assert.Equal(404, (int) result.StatusCode);
        }

        [And(@"I should get an error message indicating id is a system generated field")]
        public async Task ErrorMessageIDisSystemGenerated()
        {
            var httpResponseMessage = (HttpResponseMessage)GetObject(KeyNameHelpers.HttpResponseString);
            var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<List<ErrorResponse>>(responseText);

            Assert.Equal("Id is a system generated value", errorResponse[0].message);
            Assert.Equal("Null", errorResponse[0].code);
        }

        [And(@"I should get an error message indicating car type cannot be null")]
        public async Task NullCarTypeError()
        {
            var httpResponseMessage = (HttpResponseMessage)GetObject(KeyNameHelpers.HttpResponseString);
            var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<List<ErrorResponse>>(responseText);

            Assert.Equal("CarType cannot be null", errorResponse[0].message);
            Assert.Equal("NotNull", errorResponse[0].code);
        }

        [And(@"I should get an error message indicating manufacturer id cannot be null")]
        public async Task ManufacturerIDNullError()
        {
            var httpResponseMessage = (HttpResponseMessage)GetObject(KeyNameHelpers.HttpResponseString);
            var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<List<ErrorResponse>>(responseText);

            Assert.Equal("Manufacturer Id cannot be null", errorResponse[0].message);
            Assert.Equal("NotNull", errorResponse[0].code);
        }

        public void Dispose()
        {
            var wiremockClient = new WiremockClient(GetConfigValue(KeyNameHelpers.WireMockUrlKeyString));
            var resetTask = wiremockClient.ResetMappingsAsync();
            resetTask.Wait();
        }
    }
}