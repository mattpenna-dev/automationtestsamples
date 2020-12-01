Feature: Healthcheck 
  Tests to validate Healthcheck endpoint is available
  
  @dev @ci @prod
  Scenario: Health Check returns 200 ok
    When I make a call to the health check endpoint
    Then I should get an 200 status