Feature: Create
Validate that a Car object can be created from the Car Service 

@CI CreateCar
Scenario: When making a POST request to the Car Service then a new Car object is created
Given that the Manufacturer service has been mocked out 
When I make a Post request to the Car Service
Then I should get a 200 OK response
And the body in the response will return the new Car object 

@CI Missingfields  
Scenario: When making a Post request to the Car Service without the required information then the 
request will fail and the Car object will not be created
Given that the Car object is missing necessary information in the body of the request 
And the Manufacturer Service has been mocked out
When I make a Post request to Car Service 
Then I should get a 400 error response

@CI ConflictResponse
Scenario: When making a Post request to Car Service to create a Car that already exist then the request
will return a an error and the Car will not be created
Given that the Car that I am attempting to create already exists 
And the Manufacturer Service has been mocked out
When I make a Post request to the Car Service 
Then I should get a 404 concurrency error 