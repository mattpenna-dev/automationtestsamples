using System;

namespace CucumberAutomationTests.Exceptions
{
    public class ManufacturerCouldNotBeCreatedException : Exception
    {
        public ManufacturerCouldNotBeCreatedException(string message) : base(message)
        {
            
        }
    }
}