using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    public abstract class CommonStepDefinition : Feature
    {
        private Dictionary<string, object> _objects = new Dictionary<string, object>();
        
        [Then(@"I should get an (.*) status")]
        public void ThenIShouldGetAnStatus(int statusCode)
        {
            var httpResponseMessage = (HttpResponseMessage) GetObject(KeyNameHelpers.HttpResponseString);
            Assert.Equal(statusCode, (int) httpResponseMessage.StatusCode);
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
    }
}