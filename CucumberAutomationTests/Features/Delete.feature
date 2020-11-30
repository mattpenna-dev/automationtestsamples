Feature: Delete
Validate that a Car object can be deleted from the Car Service 

@CI @DeleteCar
Scenario: The Car service returns a successful response after deleting a Car service
Given that the Manufacturer service has been mocked out
When I make a Delete request to the Car Service 
Then I should get a 200 OK response. 

@CI @AlreadyDeletedCar
Scenario: The Car Service returns an error when trying to delete a Car that has already been removed
Given that the Car being deleted has already been removed from the Car Service 
And the Manufacturer service has been mocked out
When I make a Delete request to the Car service
Then I should get a 400 error response. 
