Feature: Delete Endpoint
  Tests to validate delete endpoint

  @ci @dev
  Scenario: Car doesn't exists. I should get a not found exception
    When I make a call to delete a car that does not exists
    Then I should get an 400 status
    And I should get an error message that the car does not exists

  @dev @possible_bug
  Scenario: Car exists. I should get a no content message and see the car has been removed from the manufacturer
    Given A car exists
    When I make a call to delete the existing car
    Then I should get an 204 status
    And I should see the car has been deleted
    And I should see the car is no longer associated with the manufacturer

  @ci
  Scenario: Car exists. I should get a no content message and see the car has been removed from the manufacturer
    Given A car exists with a mocked manufacturer
    When I make a call to delete the existing car
    Then I should get an 204 status
    And I should see the car has been deleted

  @ci @possible_bug
  Scenario: Manufacturer service update endpoint throws internal server error
    Given A car exists
    And Manufacturer service update endpoint returns internal server error
    When I make a call to delete the existing car
    Then I should get an 500 status
    And I should see the car has not been deleted

  @ci @possible_bug @need_to_validate_wiremock_will_return_invalid_json
  Scenario: Manufacturer service update endpoint returns invalid json
    Given A car exists
    And Manufacturer service update endpoint returns invalid json
    When I make a call to delete the existing car
    Then I should get an 500 status
    And I should see the car has not been deleted
