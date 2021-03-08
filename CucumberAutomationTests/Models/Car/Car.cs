using System.Collections;

namespace CucumberAutomationTests.Models.Car
{
    public class Car
    {
        public string carType { get; set; }
        public string description { get; set; }
        public string manufacturerId { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string updatedOn { get; set; }
        public string updatedBy { get; set; }
    }

    public class Cars
    {
        public string carType { get; set; }
        public string description { get; set; }
        public string manufacturferId { get; set; }
        public string name { get; set; }

        public override string ToString()
         {
           return carType, description, manufacturerId, name;

         }
    }
}
