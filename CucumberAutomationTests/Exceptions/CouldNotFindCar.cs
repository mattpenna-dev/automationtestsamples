using System;

namespace CucumberAutomationTests.Exceptions
{
    public class CouldNotFindCar : Exception
    {
        public CouldNotFindCar(string message) : base(message)
        {
            
        }
    }
}