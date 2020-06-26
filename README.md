#Onboarding
BDD Autoamtion Framework increases and improves collaboration. It enables everyone involved in the project to easily engage with the product development cycle. And by using plain language, all are able to write behavior scenarios and helps create living documentation.

This solution is built using .NetCore SpecFlow Webdriver MsTest Framework.
Uses: .Net Core SpecFlow 3.1 Selenium WebDriver 3.1 MsTest Framwork 2.1 Specflow MsTest 3.1 Selenium chrome driver utilises Page Object Model pattern can be run using Azure or Jenkins Build pipeline

## Install and Setup
Download and Install Visual Studio 
Open downloaded Project Solution and Build the Project (Used NuGet Tools > NuGet package manager to install Specflow, MSTest and Selenium Dependencies)
Run test from Test Explorer

## FrameWork Structure and Content

# Env Configuration Helper
ProjectBDDRegsitrationProcess -> Environment -> 
Configs - Configuration helper file
- Needed by all or most of the tests ex: `url`, `user data`, `environment specific data`
- Dev, Test, Staging, Production
- Accessible to all tests
Env- Configure env for all diff env and also can specify the VM's when done on Azure / AWS
AppSettings.Json -> Add JSON Config file for Environment Configuration helps to add env specific data variables needed for tests

# Hooks
ProjectBDDRegsitrationProcess -> Environment -> GeneralHooks -> Configs-> GeneralHooks -> 
used for event bindings during the tests executions and also tags specfiied at test scenario level can be used in build or release pipeiline for running tests specific to smoke/functional/regression test run
Ex:
"Beforescenario()"
- Runs once before the test run 
- Setup data for the test
- Launch the browser
- Open connection to database if needed
"Afterscenario()"
- Runs once before the test run 
- Close the browser
- Close db connection

# TestData
ProjectBDDRegsitrationProcess -> Environment -> testdatagenerator -> 
File list all the test data variables and also test data generation - faker utility is used to mimic the real user test data and also auto generation of data during the test run helps the data integrity. 

# Locating Elements ProjectBDDRegsitrationProcess.PageObjects
Page Objects structure so tests and element locators separated. This will keep the code clean and easy to understand and maintain.

ProjectBDDRegsitrationProcess -> specflow.json - trx file
Run the test from cmd line and test results are generated after each run

# Capture Screenshot  - ProjectBDDRegsitrationProcess.StepFiles
Selenium Interactions used and Method has been added which can take screenshot and save it as png file during test execution but commented out in the code as it needs a path to be provided where screenshot to be saved which can be also be used through Hooks to take screenshot Before and After Scenario or Individual Steps.

# Assumptions: 
User is able to complete the registration process either by adding a Item into Cart or Register then able to perform a checkout process which cover the almost most the functional feature of the web application with actaul registration process.

Improvements: 
Framework currenlty works with chrome browser only and other browser can be added depending on the business need. 
In the Page Objects class Locaters are mostly using relative Xpath and it is best to use the ID's instead where ever possible. 
For the given scenario I recommend using table to pass the data within steps which give more flexibility in data handling where as scenario outline restrict data use for each run.
Framework can be enhanced to include the API tests also by adding a custom http test methods or tools like restsharp for API tests are data can be inlined with specflow scenario. 
