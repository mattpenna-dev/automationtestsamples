Feature: Update
Validate that a Car object can be updated from the Car Service 

@CI @Update Car
Scenario: The Car Service will return a successful response when updating a Car
Given that the Manufacturer service has been mocked out
When I make a PUT request
Then I should recieve a 200 OK response