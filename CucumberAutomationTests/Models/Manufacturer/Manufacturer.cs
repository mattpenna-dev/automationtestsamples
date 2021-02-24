using System.Collections.Generic;

namespace CucumberAutomationTests.Models.Manufacturer
{
    public class Manufacturer
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> cars { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string updatedOn { get; set; }
        public string updatedBy { get; set; }
    }
}