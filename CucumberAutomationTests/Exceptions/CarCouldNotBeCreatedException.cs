using System;

namespace CucumberAutomationTests.Exceptions
{
    public class CarCouldNotBeCreatedException : Exception
    {
        public CarCouldNotBeCreatedException(string message) : base(message)
        {
            
        }
    }
}