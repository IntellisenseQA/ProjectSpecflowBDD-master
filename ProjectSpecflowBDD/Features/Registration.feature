Feature: Validate Registration Process
	In order to create the customer account
	As a user
	I want to be able to complete registration

@web
Scenario: Validate New User Registration Before Checkout
	Given user is on My Store HomePage
	When user enters new emailaddress
	And user submit create account page 
	And user adds the product to cart
	| Item                        | ItemPrice |
	| Faded Short Sleeve T-shirts | $16.51    |
	And user proceed to checkout
	And user proceed on Billing Address page
	And user agree to terms of service
	Then user should see order summary
	And user should see order confirmation
	| TotalPrice |
	| $19.25     |
	#Then user should see My Account page

@web
Scenario: Validate New User Registration After Checkout
	Given user is on My Store HomePage
	When user adds the product to cart
	| Item                        | ItemPrice |
	| Faded Short Sleeve T-shirts | $16.51    |
	And user proceed to checkout 
	And user enters new emailaddress
	And user submit create account page 
	And user checkout Item Summary page
	| Item                        | ItemPrice |
	| Faded Short Sleeve T-shirts | $16.51    |
	And user proceed on Billing Address page
	And user agree to terms of service
	Then user should see order summary
	And user should see order confirmation
	| TotalPrice |
	| $19.25     |
@web
Scenario: Validate error messages for Registration page
	Given user is on My Store HomePage
	When user enters new emailaddress
	And user submit incomplete Create AccountPage
	Then user should see errormessage
	| errormessage |
	| lastname     |
	| firstname    |
	| passwd       |
	| address1     |
	| city         |

