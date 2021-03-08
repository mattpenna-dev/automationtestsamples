using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CucumberAutomationTests.Exceptions;
using CucumberAutomationTests.Models.Manufacturer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    public abstract class CommonStepDefinition : Feature
    {
        private Dictionary<string, object> _objects = new Dictionary<string, object>();
        private IConfiguration _testConfiguration;

        public readonly HttpClient HttpClient;

        protected CommonStepDefinition()
        {
            var env = Environment.GetEnvironmentVariable("AUTOMATION_ENV");
            if (!string.IsNullOrEmpty(env))
            {
                var configFile = $"./Configurations/cucumbertestsettings-{env}.json";
                _testConfiguration = new ConfigurationBuilder().AddJsonFile(configFile, false).Build();
            }
            else
            {
                _testConfiguration = new ConfigurationBuilder().AddJsonFile("./Configurations/cucumbertestsettings.json", false).Build();
            }
        }
        
        [Then(@"I should get an (.*) status")]
        public void ThenIShouldGetAnStatus(int statusCode)
        {
            var httpResponseMessage = (HttpResponseMessage) GetObject(KeyNameHelpers.HttpResponseString);
            Assert.Equal(statusCode, (int) httpResponseMessage.StatusCode);
        }

        protected async Task<Manufacturer> CreateManufacturerAsync()
        {
            var manufacturer = new Manufacturer
            {
                name = $"{Guid.NewGuid()}-Teslas"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(manufacturer));
            var result = await HttpClient.PostAsync($"{GetConfigValue(KeyNameHelpers.MaunfacturerServiceKeyString)}/manufacturer", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new ManufacturerCouldNotBeCreatedException("Error Creating Manufacturer.");
            }

            var responseText = await result.Content.ReadAsStringAsync();
            var createdManufacturer = JsonConvert.DeserializeObject<Manufacturer>(responseText);
            AddObject(KeyNameHelpers.ExistingManufacturerKeyString, createdManufacturer);
            return createdManufacturer;
        }

        protected void AddObject(string key, object obj)
        {
            if (_objects.ContainsKey(key))
            {
                _objects[key] = obj;
                return;
            }
            
            _objects.Add(key, obj);
        }
        
        protected object GetObject(string key)
        {
            if (_objects.ContainsKey(key))
            {
                return _objects[key];
            }

            throw new Exception("Key not found in objects");
        }
        
        protected string GetConfigValue(string key)
        {
            return _testConfiguration[key];
        }
    }
}