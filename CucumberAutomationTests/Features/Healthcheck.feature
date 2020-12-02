Feature: Healthcheck
Validate that a Car Service returns a 200 OK response after making a healthcheck 

@CI @Dev @Healthcheck 
Scenario: The Car servivce turns a 200 OK response after making a healthcheck
Given that the Manufacturer service is mocked out 
When I make a Get Car Service Health check to the Car Service 
Then a 200 Ok response will be returned

@SmokeTest @Healthcheck 
Scenario: The Car servivce turns a 200 OK response after making a healthcheck
Given that the Manufacturer exists
When I make a Get Car Service Health check to the Car Service 
Then a 200 Ok response will be returned