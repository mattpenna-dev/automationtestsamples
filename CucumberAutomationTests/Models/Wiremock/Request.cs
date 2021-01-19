using System.Collections.Generic;

namespace CucumberAutomationTests.Models.Wiremock
{
    public class Request
    {
        public string method { get; set; }
        public string url { get; set; }
        public string urlPathPattern { get; set; }
        public Dictionary<string, HeaderValue> headers { get; set; }
    }
}