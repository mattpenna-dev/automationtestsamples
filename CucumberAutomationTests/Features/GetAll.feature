Feature: GetAll Endpoint
  Tests to validate getall endpoint
  
  @dev
  Scenario: Given cars exists, i get back cars
    Given Several cars exists
    When I make a call to get all cars
    Then I should get a 200 status
    And I should see all cars are returned in the list

  @ci
  Scenario: Given cars exists, i get back cars
    Given Several cars exists with mocked manufacturers
    When I make a call to get all cars
    Then I should get a 200 status
    And I should see all cars are returned in the list  
    
  @prod
  Scenario: When i call get all cars i get back all existing cars
    When I make a call to get all cars
    Then I should get a 200 status
    