Feature: GetAll
Validate that all the Car objects can be retrieved from the Car Service. 

@CI @Dev GetAlltheCars 
Scenario: The Car Service returns all the Cars when making Get All Cars request
Given that the Manufacturer service has been mocked out
When I make a Get request to the Car service 
Then I will receive a 200 ok response 
And I should get a all the Cars from the service in the response body 

@CI @Dev GetAllCarsInternalServiceError
Scenario: When the Manufacturer has an Internal Service Error then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Get All Cars request to the Car Service 
Then I should get a 500 error response. 

@CI @Dev GetAllCarsManufacturerServiceDown
Scenario: When the Manufacturer service is down then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Get All Cars request to the Car Service 
Then I should get a 500 error response. 

@SmokeTest GetAllCars 
Scenario: The Car Service returns all the Cars when making Get All Cars request
Given that the Manufacturer service exists
When I make a Get request to the Car service 
Then I will receive a 200 ok response 
And I should get a all the Cars from the service in the response body 