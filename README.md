This solution is built using .NetCore SpecFlow Webdriver MsTest Framework.

Uses: .Net Core SpecFlow 3.1 Selenium WebDriver 3.1 MsTest Framwork 2.1 Specflow MsTest 3.1 Selenium chrome driver utilises Page Object Model pattern can be run using Azure or Jenkins Build pipeline

Install Visual Studio Community

Use NuGet (Project > Manage NuGet packages) to install Specflow, MSTest and Selenium:

ProjectBDDRegsitrationProcess -> Environment -> Configs - Configuration helper file
- Needed by all or most of the tests ex: `url`, `user data`, `environment specific data`
- Dev, Test, Staging, Production
- Accessible to all tests

ProjectBDDRegsitrationProcess -> Environment -> Env- Configure env for all diff env and also can specify the VM's when done on Azure / AWS

AppSettings.Json -> Add JSON Config file for Environment Configuration helps to add env specific data variables needed for tests

ProjectBDDRegsitrationProcess -> Environment -> GeneralHooks -> Configs-> GeneralHooks -> 
used for event bindings during the tests executions and also tags specfiied at test scenario level can be used in build or release pipeiline for running tests specific to smoke/functional/regression test run
Ex:
"Beforescenario()"
- Runs once before the test run 
- Setup data for the test
- Launch the browser
- Open connection to database if needed

"Afterscenario()`
- Runs once after the test
- Close the browser
- Close db connection

ProjectBDDRegsitrationProcess -> Environment -> testdatagenerator -> 
File list all the test data variables and also if any test data generation ex.fake test data username can be done using faker utility

ProjectBDDRegsitrationProcess -> specflow.json - trx file
Run the test from cmd line and test results are generated after each run

#Screenshot ProjectBDDRegsitrationProcess.StepFiles
Selenium Interactions used and Method has been added which can take screenshot and save it a s png file during test execution but commented out in the code as it needs a path to be provided where screenshot to be saved to

Assumptions: User is able to complete the registration process either by adding a Item into Cart or Register then able to perform a checkout process which cover the almost most the functional feature of the web application with actaul registration process.

Improvements: 
Framework currenlty works with chrome browser only and other browser can be added depending on the business need. 
In the Page Objects class Locaters are mostly using relative Xpath and it is best to use the ID's instead where ever possible. 
For the given scenario I recommend using table to pass the data within steps which give more flexibility in data handling where as scenario outline restrict data use for each run.
Framework can be enhanced to include the API tests also by adding a custom http test methods or tools like restsharp for API tests are data can be inlined with specflow scenario. 
