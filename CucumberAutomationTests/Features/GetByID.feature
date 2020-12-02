Feature: GetByID
Validate that a Car object can be retrieved from the Car Service using the ID for the object. 

@CI @GetCarByID
Scenario: The Car Service will return the requested Car when trying to retrieve it by using the 
ID for that Car
Given that I have a Car ID set in the url for the Get CarByID url
And the Manufaturer Service has been mocked out 
When I make a Get request to the Car Service 
Then I should get a 200 OK response 
And I should get the requested Car object in the JSON body of the response. 

@CI @CarIDDoesNotExist
Scenario: The Car Service will return an error when trying to retrieve a Car using an ID that does not exist
Given that I have a Car ID set in the url for the Get CarByID url
And the Manufaturer Service has been mocked out 
When I make a Get request to the Car Service 
Then I should get a 404 error response 

@CI @Dev GetByIDInternalServiceError
Scenario: When the Manufacturer has an Internal Service Error then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Get Car By ID request to the Car Service 
Then I should get a 500 error response. 

@CI @Dev GetByIDManufacturerServiceDown
Scenario: When the Manufacturer service is down then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Get Car By ID request to the Car Service 
Then I should get a 500 error response. 

@SmokeTest GetCarByID 
Scenario: The Car Service returns all the Cars when making Get All Cars request
Given that the Manufacturer service exists
And a Car ID variable is set for the Get CarByID url
When I make a Get Car By ID request to the Car service
Then I will receive a 200 ok response 
And I should expected Car corresponding to the ID from the service in the response body 