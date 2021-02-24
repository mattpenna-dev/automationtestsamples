Feature: Create Endpoint
  Tests to validate create endpoint
  
  @dev
  Scenario: Manufacturer exists I can create a new car
    Given A manufacturer exists
    When I make a call to create a car
    Then I should get an 200 status code
    And I should see the car was created
    And I should see the manufacturer has been updated with the new car

  @ci
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer exists
    When I make a call to create a car
    Then I should get an 200 status code
    And I should see the car was created

  @dev
  Scenario: Manufacturer exists i can create a new car
    Given A manufacturer exists
    When I make a call to create a car with empty description
    Then I should get an 200 status code
    And I should see the car was created
    And I should see the manufacturer has been updated with the new car

  @ci
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer exists
    When I make a call to create a car with empty description
    Then I should get an 200 status code
    And I should see the car was created

  @dev
  Scenario: Manufacturer exists i can create a new car
    Given A manufacturer exists
    When I make a call to create a car with empty createdBy
    Then I should get an 200 status code
    And I should see the car was created
    And I should see the manufacturer has been updated with the new car

  @ci
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer exists
    When I make a call to create a car with empty createdBy
    Then I should get an 200 status code
    And I should see the car was created

  @dev
  Scenario: Manufacturer exists i can create a new car
    Given A manufacturer exists
    When I make a call to create a car with empty updatedBy
    Then I should get an 200 status code
    And I should see the car was created
    And I should see the manufacturer has been updated with the new car

  @ci
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer exists
    When I make a call to create a car with empty updatedBy
    Then I should get an 200 status code
    And I should see the car was created  
    
  @ci @dev @prod 
  Scenario: Id not null returns bad request
    When I make a call to create a car with non null id
    Then I should get an 400 status code
    And I should get an error message indicating id is a system generated field

  @ci @dev @prod
  Scenario: CarType null returns bad request
    When I make a call to create a car with non null CarType
    Then I should get an 400 status code
    And I should get an error message indicating car type cannot be null

  @ci @dev @prod @possible_bug
  Scenario: CarType invalid returns bad request
    When I make a call to create a car with invalid CarType
    Then I should get an 400 status code
    And I should get an error message indicating invalid car type

  @ci @dev @prod
  Scenario: Manufacturer id null returns bad request
    When I make a call to create a car with manufacturer id null
    Then I should get an 400 status code
    And I should get an error message indicating manufacturer id cannot be null

  @ci @dev @prod
  Scenario: Name null returns bad request
    When I make a call to create a car with name null
    Then I should get an 400 status code
    And I should get an error message indicating name cannot be null

  @ci @dev @prod
  Scenario: CreatedOn not null returns bad request
    When I make a call to create a car with non null CreatedOn
    Then I should get an 400 status code
    And I should get an error message indicating CreatedOn is a system generated field

  @ci @dev @prod
  Scenario: UpdatedOn not null returns bad request
    When I make a call to create a car with non null UpdatedOn
    Then I should get an 400 status code
    And I should get an error message indicating UpdatedOn is a system generated field

  @dev
  Scenario: Manufacturer does not exists I get back a bad request
    Given A manufacturer does not exists
    When I make a call to create a car
    Then I should get an 400 status code
    And I should see the car was not created

  @ci
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer does not exists
    When I make a call to create a car
    Then I should get an 400 status code
    And I should see the car was not created

  @ci @possible_bug
  Scenario: Manufacturer exists i can create a new car
    Given A mocked manufacturer returns internal server error
    When I make a call to create a car
    Then I should get an 500 status code
    And I should see the car was not created    