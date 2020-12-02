Feature: GetById Endpoint
  Tests to validate getbyid endpoint
  
  @dev @ci @prod
  Scenario: Invalid Id. Should return a 404 not found
    When I make a call to get an Id that does not exists
    Then I should get a 404 status
    And I should see a message indicating the car could not be found

  @dev
  Scenario: Car exists i can get by id
    Given A car exists
    When I make a call to get the car by id
    Then I should get a 200 status
    And I should see the car is returned

  @ci
  Scenario: Car exists i can get by id
    Given A cars exists with a mocked manufacturer
    When I make a call to get the car by id
    Then I should get a 200 status
    And I should see the car is returned

  @prod
  Scenario: Car exists i can get by id
    Given I have found an existing car
    When I make a call to get the car by id
    Then I should get a 200 status
    And I should see the car is returned   