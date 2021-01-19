using System.Collections.Generic;

namespace CucumberAutomationTests.Models.Wiremock
{
    public class Response
    {
        public int status { get; set; }
        public string body { get; set; }
        public Dictionary<string, string> headers { get; set; }
    }
}