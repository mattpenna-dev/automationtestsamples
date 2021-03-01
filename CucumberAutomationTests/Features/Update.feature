Feature: Update Endpoint
  Tests to validate update endpoint
  
  @dev
  Scenario: Car exists i can update the car
    Given A car exists
    When I make a call to update a car
    Then I should get an 200 status code
    And I should see the car was updated

  @dev @possible_bug
  Scenario: Car exists i can update the car to a new manufacturer
    Given A car exists
    When I make a call to update a car
    Then I should get an 200 status code
    And I should see the car was updated  
    And I should see the old manufacturer is updated to no longer be associated with the car
    And I should see the new manufacturer is updated to be associated with the car
    
  @ci  
  Scenario: Car exists i can update the car
    Given A cars exists with a mocked manufacturer
    When I make a call to update a car
    Then I should get an 200 status code
    And I should see the car was updated
    
  @dev @ci @prod
  Scenario: Null CarType
    When I make a call to update a car with null cartype
    Then I should get an 400 status code
    And I should get an error message indicating car type cannot be null

  @dev @ci @prod
  Scenario: Invalid CarType
    When I make a call to update a car with invalid cartype
    Then I should get an 400 status code
    And I should get an error message indicating invalid car type

  @dev @ci @prod
  Scenario: Null manufacturer id
    When I make a call to update a car with null manufacturer id
    Then I should get an 400 status code
    And I should get an error message indicating manufacturer id cannot be null

  @dev @ci @prod
  Scenario: Null name
    When I make a call to update a car with null name
    Then I should get an 400 status code
    And I should get an error message indicating name cannot be null

  @dev @ci @prod
  Scenario: Updated on not null
    When I make a call to update a car with updated on not null
    Then I should get an 400 status code
    And I should get an error message indicating UpdatedOn is a system generated field
