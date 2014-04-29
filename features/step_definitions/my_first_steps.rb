Given /^I am on the Movie List Screen$/ do
  element_exists("view marked:'BoxOfficeScreen'")
  sleep(STEP_PAUSE)
end

Then /^I scroll to Top Box Office$/ do
  scroll_to_cell({:row=>0,:section=>1})
  sleep(STEP_PAUSE)
end

Then /^I touch list item number (\d+) of Top Box Office section$/ do |index|
   index = index.to_i
   screenshot_and_raise "Index should be positive (was: #{index})" if (index<=0)
   touch("tableView tableViewCell indexPath:#{index-1},1")
   sleep(STEP_PAUSE)
end
