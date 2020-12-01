Feature: Create
Validate that a Car object can be created from the Car Service 

@CI CreateCarCarType
Scenario: A car is created with a carType when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a carType
Then I should get a 200 OK response
And the body in the response will return the new Car with a carType

@Dev CreateCarCarType
Scenario:  A car is created with a CarType when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a CarType
Then I should get a 200 OK response
And the body in the response will return the new Car with a CarType

@CI CreateCarDescription
Scenario: A car is created with a description when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a description
Then I should get a 200 OK response
And the body in the response will return the new Car with a description

@Dev CreateCarDescription
Scenario:  A car is created with a description when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a descprtion
Then I should get a 200 OK response
And the body in the response will return the new Car with a description

@CI CreateCarmanufaturerId
Scenario: A car is created with a manufaturerId when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a manufaturerId
Then I should get a 200 OK response
And the body in the response will return the new Car with a manufaturerId

@Dev CreateCarmanufaturerId
Scenario:  A car is created with a manufaturerId when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a manufaturerId
Then I should get a 200 OK response
And the body in the response will return the new Car with a manufaturerId

@CI CreateCarname
Scenario: A car is created with a name when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a name
Then I should get a 200 OK response
And the body in the response will return the new Car with a name

@Dev CreateCarname
Scenario:  A car is created with a description when making a create call to the Car Service 
Given that the Manufacturer service has been mocked out 
When I make a Post create car request to the Car Service with a name
Then I should get a 200 OK response
And the body in the response will return the new Car with a name

@CI ConflictResponse
Scenario: A duplicate car will return an error from the Car Service
Given that the Car that I am attempting to create already exists 
And the Manufacturer Service has been mocked out
When I make a Post request to the Car Service 
Then I should get a 404 concurrency error 

@Dev ConflictResponse
Scenario: A duplicate car will return an error from the Car Service
Given that the Car that I am attempting to create already exists 
And the Manufacturer Service has been mocked out
When I make a Post request to the Car Service 
Then I should get a 404 concurrency error 