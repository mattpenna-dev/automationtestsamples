Feature: GetByID
Validate that a Car object can be retrieved from the Car Service using the ID for the object. 

@CI @GetCarByID
Scenario: The Car Service will return the requested Car when trying to retrieve it by using the 
ID for that Car
Given that I have an ID JSON body of the request for a Car object that exist in the Car Service
And the Manufaturer Service has been mocked out 
When I make a Get request to the Car Service 
Then I should get a 200 OK response 
And I should get the requested Car object in the JSON body of the response. 

@CI @CarIDDoesNotExist
Scenario: The Car Service will return an error when trying to retrieve a Car using an ID that does not exist
Given that the I have an ID in the JSON body of the request for a Car that does not exist in the Car Service
And the Manufaturer Service has been mocked out 
When I make a Get request to the Car Service 
Then I should get a 400 error response 

