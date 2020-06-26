using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using System;
using ProjectBDDRegsitrationProcess.Environment;
using OpenQA.Selenium.Interactions;

namespace ProjectBDDRegsitrationProcess.PageObjects
{
    public class CreateAccountPage
    {
        Testdatagenerator datagenerator = GeneralHooks.GetContext();
        public IWebDriver driver;
        public CreateAccountPage()
        {
            var driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public By SignInLink = By.XPath("//a[@class='login']");
        public By CreateAccountWidget = By.XPath("//form[@id='create-account_form']");
        public By AccountCreationForm = By.XPath("//form[@id='account-creation_form']");
        public By SearchPanel = By.XPath("//form[@class='search search-normal unselectable']");

        [FindsBy(How = How.XPath, Using = "//a[@class='login']")]
        public IWebElement SignIn { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email_create']")]
        public IWebElement SignInEmailInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//form[@id='create-account_form']//span[1]")]
        public IWebElement CreateAccountButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='id_gender1']")]
        public IWebElement SelectGenderCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='customer_firstname']")]
        public IWebElement EnterFirstName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='customer_lastname']")]
        public IWebElement EnterLastName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email']")]
        public IWebElement SignInEmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='passwd']")]
        public IWebElement EnterPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='days']")]
        public IWebElement SelectDOBDays { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='months']")]
        public IWebElement SelectDOBMonth { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='years']")]
        public IWebElement SelectDOBYear { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='firstname']")]
        public IWebElement EnterAddressFirstName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='lastname']")]
        public IWebElement EnterAddressLastName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='address1']")]
        public IWebElement EnterAddressLine1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='address2']")]
        public IWebElement EnterAddressLine2 { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='city']")]
        public IWebElement EnterCity { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='id_state']")]
        public IWebElement SelectState { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='postcode']")]
        public IWebElement EnterPostCode { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='id_country']")]
        public IWebElement SelectCountry { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='phone_mobile']")]
        public IWebElement EnterMobileNo { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='alias']")]
        public IWebElement InputAliasEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Register')]")]
        public IWebElement RegisterButton { get; set; }


        public void Waitforelementvisible(By elementLocator, int timeout = 1)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(timeout));
                wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }


        public void Selectdropdownelement(IWebElement webElement, string value)
        {
            try
            {
                var selectElement = new SelectElement(webElement);
                selectElement.SelectByValue(value);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element'" + webElement + "' was not found in current context page.");
                throw;
            }
        }

        public void ClickAndInputIntoTextBox (IWebElement webElement, string value)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(webElement).Click().SendKeys(Keys.Delete).SendKeys(value).Build().Perform();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element'" + webElement + "' was not found in current context page.");
                throw;
            }
        }

        public void ClickSignIn()
        {
            SignIn.Click();
            Waitforelementvisible(CreateAccountWidget, 1);
        }

        public void EnterNewEmailAddressToRegister()
        {
            var regemailaddress = datagenerator.GenEmailaddress();
            ClickAndInputIntoTextBox(SignInEmailInput, regemailaddress);
            CreateAccountButton.Click();
            Waitforelementvisible(AccountCreationForm, 1);
        }

        public void EnterPersonalInfo()
        {
            Waitforelementvisible(AccountCreationForm, 1);
            SelectGenderCheckbox.Click();
           var FirstName = datagenerator.GenFirstName();
           var LastName = datagenerator.GenLastName();
            ClickAndInputIntoTextBox(EnterFirstName, FirstName);
            ClickAndInputIntoTextBox(EnterLastName, LastName);
            ClickAndInputIntoTextBox(EnterPassword, datagenerator.DevuserSignInPassword);
            Selectdropdownelement(SelectDOBDays, "12");
            Selectdropdownelement(SelectDOBMonth,"12");
            Selectdropdownelement(SelectDOBYear, "2000");

        }

        public void EnterAdddressDetails()
        {
            var CheckFirstName = driver.FindElement(By.Id("firstname")).GetAttribute("value");
            var CheckLastName = driver.FindElement(By.Id("lastname")).GetAttribute("value");
            Assert.AreEqual(CheckFirstName, datagenerator.RandFirstName);
            Assert.AreEqual(CheckLastName, datagenerator.RandLastName);

            ClickAndInputIntoTextBox(EnterAddressLine1, datagenerator.GenAddressLine1());
            ClickAndInputIntoTextBox(EnterAddressLine2, datagenerator.GenAddressLine2());

            ClickAndInputIntoTextBox(EnterCity, datagenerator.GenCity());
            Selectdropdownelement(SelectState, "1");
            ClickAndInputIntoTextBox(EnterPostCode, datagenerator.Genpostcode());
            Selectdropdownelement(SelectCountry, "21");
            ClickAndInputIntoTextBox(EnterMobileNo, datagenerator.GenMobilenumber());
            InputAliasEmail.SendKeys("AliasEmail@test.com");
            RegisterButton.Click();
        }

        public void Navigattohome()
        {
            Waitforelementvisible(SearchPanel, 1);
        }

    }
}
