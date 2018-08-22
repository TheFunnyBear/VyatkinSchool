Feature: Gallery
       Show gallery page and check how it work

@galleryFunctionalTest
Scenario: Add image button not exist if user not logged in
		Given Navigate to main page
		And Main page is shown
		When Click on Gallery button in navigation bar
		Then can't add image to gallery

@galleryFunctionalTest
Scenario: Add image button exist if user logged in
		Given Navigate to main page
		And Main page is shown
		And Login as administrator
		And Click on News button in navigation bar
		And Main page is shown
		When Click on Gallery button in navigation bar
		Then can add image to gallery
