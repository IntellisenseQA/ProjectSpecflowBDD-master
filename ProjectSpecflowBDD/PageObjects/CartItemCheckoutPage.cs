using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProjectBDDRegsitrationProcess.Environment;
using SeleniumExtras.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace ProjectBDDRegsitrationProcess.PageObjects
{
    public class CartItemCheckoutPage
    {
        Testdatagenerator datagenerator = GeneralHooks.GetContext();
        public IWebDriver driver;
        public CartItemCheckoutPage()
        {
            var driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public By Porductdisplayed = By.XPath("//a[@class='product-name'][contains(text(),'Faded Short Sleeve T-shirts')]");
        public By ProceedToCheckoutOverlay = By.XPath("//span[contains(text(),'Proceed to checkout')]");
        public By ProductQuantity = By.XPath("//span[@id='summary_products_quantity'][contains(text(),'1 Product')]");

        [FindsBy(How = How.XPath, Using = "//a[@class='sf-with-ul'][contains(text(),'Women')]")]
        public IWebElement MainTabWomen { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Add to cart')]")]
        public IWebElement AddCartItem { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Proceed to checkout')]")]
        public IWebElement StartProceedToCheckoutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='button btn btn-default standard-checkout button-medium']//span[contains(text(),'Proceed to checkout')]")]
        public IWebElement SummaryScreenProceedToCheckoutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@name='processAddress']//span[contains(text(),'Proceed to checkout')]")]
        public IWebElement AddressScreenProceedToCheckoutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@name='processCarrier']//span[contains(text(),'Proceed to checkout')]")]
        public IWebElement ShippingScreenProceedToCheckoutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='cgv']")]
        public IWebElement TOSCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='bankwire']")]
        public IWebElement PayByBankWireButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'I confirm my order')]")]
        public IWebElement IConfirmMyOrderButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email_create']")]
        public IWebElement SignInEmailInput { get; set; }

        public void ValidateLoggedInUser()
        {
            var expectedfirstname = datagenerator.RandFirstName;
            var expectedlastname = datagenerator.RandLastName;
            var actualLoggedInUserName = driver.FindElement(By.XPath("//a[@class='account']")).GetAttribute("textContent");
            Assert.AreEqual(actualLoggedInUserName, "" + expectedfirstname + " " + expectedlastname + "");
        }

        public void ValidateErrorMessagesonRegistration(Table table)
        {
            var elementList = driver.FindElements(By.XPath("//div[@class='alert alert-danger']//ol//li//b"));
            int i = 0;
            foreach (var element in elementList)
                {
                var expected = table.Rows[i]["errormessage"];
                var errormessage = element.GetAttribute("textContent");
                Assert.AreEqual(errormessage, expected);
                i += 1;
                }

        }

        public void MaintabWomen(Table table)
        {
            MainTabWomen.Click();
            CreateAccountPage signInPage = new CreateAccountPage();
            WebDriverWait waitForCartItem = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            waitForCartItem.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='product-name'][contains(text(),'" + table.Rows[0]["Item"] + "')]")));
        }

        public void AddItemtoCart(Table table)
        {
            CreateAccountPage signInPage = new CreateAccountPage();
            var CartItemName = driver.FindElement(By.XPath("//a[contains(text(),'"+table.Rows[0]["Item"]+"')]"));
            CartItemName.Click();
            var actualItemPrice = driver.FindElement(By.XPath("//span[@id='our_price_display']")).GetAttribute("textContent");
            var expectedItemprice = table.Rows[0]["ItemPrice"];
            Assert.AreEqual(expectedItemprice, actualItemPrice);
            AddCartItem.Click();
            signInPage.Waitforelementvisible(ProceedToCheckoutOverlay, 1);
        }

        public void ProceedtoCheckout()
        {
            CreateAccountPage signInPage = new CreateAccountPage();
            StartProceedToCheckoutButton.Click();
            signInPage.Waitforelementvisible(ProductQuantity, 1);
            SummaryScreenProceedToCheckoutButton.Click();
        }

        public void StartCheckoutProcess(Table table)
        {
            CreateAccountPage signInPage = new CreateAccountPage();
            var Item = driver.FindElement(By.XPath("//a[contains(text(),'" + table.Rows[0]["Item"] + "')]")).Displayed;
            var actualItemPrice = driver.FindElement(By.XPath("//td[@id='total_product']")).GetAttribute("textContent");
            var expectedItemprice = table.Rows[0]["ItemPrice"];
            Assert.AreEqual(expectedItemprice, actualItemPrice);
            SummaryScreenProceedToCheckoutButton.Click();

        }

        public void AddressScreenCheckout()
        {
            AddressScreenProceedToCheckoutButton.Click();
        }

        public void AgreeTOTOS()
        {
            TOSCheckbox.Click();
            ShippingScreenProceedToCheckoutButton.Click();
        }

        public void ConfirmPayment()
        {
            PayByBankWireButton.Click();
            IConfirmMyOrderButton.Click();
        }

        public void OrderConfirmation(Table table)
        {
            var actualPrice = driver.FindElement(By.XPath("//div[@class='box']//span[@class='price']//strong")).GetAttribute("textContent");
            var expectedprice = table.Rows[0]["TotalPrice"];
            Assert.AreEqual(expectedprice, actualPrice);
        }

    }
}
