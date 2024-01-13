Feature: User Login

  Scenario: Successful Login
    Given the user is on the login page
    When the user enters valid username "john.doe" and valid password "Password123"
    And clicks the login button
    Then the user should be redirected to the dashboard
    And the user profile should be displayed with the username "john.doe"

  Scenario: Invalid Login Attempt
    Given the user is on the login page
    When the user enters invalid username "invaliduser" and invalid password "wrongpassword"
    And clicks the login button
    Then an error message should be displayed indicating invalid credentials
    And the user should remain on the login page
