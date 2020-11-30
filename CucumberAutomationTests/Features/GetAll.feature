Feature: GetAll
Validate that all the Car objects can be retrieved from the Car Service. 

@CI GetAlltheCars 
Scenario: The Car Service returns all the Cars when making Get request
Given that the Manufacturer service has been mocked out
When I make a Get request to the Car service 
Then I will receive a 200 ok response 
And I should get a all the Cars from the service in the response body 