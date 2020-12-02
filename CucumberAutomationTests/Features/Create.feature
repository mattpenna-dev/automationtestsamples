Feature: Create
Validate that a Car object can be created from the Car Service 

@CI @Dev CreateCar 
Scenario: A car is created when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service
Then I should get a 200 OK response
And the body in the response will return the new Car

@CI @Dev CreateCarCarType
Scenario: A car is created with a carType when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a carType
Then I should get a 200 OK response
And the body in the response will return the new Car with a carType

@CI @Dev CreateCarDescription
Scenario: A car is created with a description when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a description
Then I should get a 200 OK response
And the body in the response will return the new Car with a description

@CI @Dev CreateCarmanufaturerId
Scenario: A car is created with a manufaturerId when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a manufaturerId
Then I should get a 200 OK response
And the body in the response will return the new Car with a manufaturerId

@CI @Dev CreateCarname
Scenario: A car is created with a name when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a name
Then I should get a 200 OK response
And the body in the response will return the new Car with a name

@CI @Dev ConflictResponse
Scenario: A duplicate car will return an error from the Car Service
Given that the Car that I am attempting to create already exists 
And the Manufacturer Service has been mocked out
When I make a Post request to the Car Service 
Then I should get a 404 concurrency error 

@CI @Dev InternalServiceError
Scenario: When the Manufacturer has an Internal Service Error then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Post request to the Car Service 
Then I should get a 500 error response. 

@CI @Dev ManufacturerServiceDown
Scenario: When the Manufacturer service is down then the Car Service will return an error
Given the the Manurfacture service is mocked to return an Internal Service Error 
When I make a Post request to the Car Service 
Then I should get a 500 error response. 











