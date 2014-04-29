Feature: Check fresheness

Scenario: Select first Top Box Office movie
  Given I am on the Movie List Screen
  Then I wait and wait
  Then I scroll to Top Box Office
  Then I wait
  Then I touch list item number 1 of Top Box Office section
  Then I wait
  Then I should see "MovieDetailsScreen"
  Then I should see "freshIndicator"
  Then I should not see "rottenIndicator"
  And take picture


