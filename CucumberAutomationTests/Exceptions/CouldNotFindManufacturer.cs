using System;

namespace CucumberAutomationTests.Exceptions
{
    public class CouldNotFindManufacturer : Exception
    {
        public CouldNotFindManufacturer(string message) : base(message)
        {
            
        }
    }
}