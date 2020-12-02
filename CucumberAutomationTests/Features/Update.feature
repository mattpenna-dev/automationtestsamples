Feature: Update
Validate that a Car object can be updated from the Car Service 

@CI @Update Car
Scenario: The Car Service will return a successful response when updating a Car
Given that the Manufacturer service has been mocked out
When I make a PUT request
Then I should recieve a 200 OK response

@CI @Dev UpdateCarCarType
Scenario: A car is updated with a carType when making a update call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Patch update car request to the Car Service with a carType
Then I should get a 200 OK response
And the body in the response will return the updated Car with a carType

@CI @Dev UpdateCarDescription
Scenario: A car is updated with a description when making a update call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Patch update car request to the Car Service with a description
Then I should get a 200 OK response
And the body in the response will return the updated Car with a description

@CI @Dev CreateCarmanufaturerId
Scenario: A car is updated with a manufaturerId when making a update call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Patch update create car request to the Car Service with a manufaturerId
Then I should get a 200 OK response
And the body in the response will return the updated Car with a manufaturerId

@CI @Dev CreateCarname
Scenario: A car is updated with a name when making a update call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Patch update car request to the Car Service with a name
Then I should get a 200 OK response
And the body in the response will return the updated Car with a name

@CI @Dev UpdateConflictResponse
Scenario: A duplicate car will return an error from the Car Service
Given that the Car that I am attempting to create already exists 
And the Manufacturer Service has been mocked out
When I make a Patch update car request to the Car Service 
Then I should get a 404 concurrency error 

@CI @Dev UpdateInternalServiceError
Scenario: When the Manufacturer has an Internal Service Error then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Patch update car to the Car Service 
Then I should get a 500 error response. 

@CI @Dev UpdateManufacturerServiceDown
Scenario: When the Manufacturer service is down then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Patch update car request to the Car Service 
Then I should get a 500 error response. 