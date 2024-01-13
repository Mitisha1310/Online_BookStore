Feature: User Registration

  Scenario: Successful Registration
    Given the user is on the registration page
    When the user enters valid registration details
    And clicks the register button
    Then the user should be redirected to the welcome page
    And a welcome message should be displayed

  Scenario: Registration with Existing Email
    Given the user is on the registration page
    When the user enters an already registered email
    And clicks the register button
    Then an error message should be displayed indicating the email is already in use
    And the user should remain on the registration page
