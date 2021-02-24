using System.Threading.Tasks;
using Xunit.Gherkin.Quick;

namespace CucumberAutomationTests.Steps
{
    [FeatureFile("./Features/Create.feature")]
    public class CreateStepDefinition : CommonStepDefinition
    {
        [Given(@"A manufacturer exists")]
        public async Task GivenManufacturerExists()
        {
            
        }

        [When(@"I make a call to create a car")]
        public async Task WhenIMakeACallToCreateCar()
        {
            
        }

        [And(@"I should see the car was created")]
        public async Task IShouldSeeTheCarWasCreated()
        {
            
        }

        [And(@"I should see the manufacturer has been updated with the new car")]
        public async Task IShouldSeeTheManufactuererHasBeenUpdatedWithNewCar()
        {
            
        }
    }
}