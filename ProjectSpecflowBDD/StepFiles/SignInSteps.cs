using ProjectBDDRegsitrationProcess.Environment;
using ProjectBDDRegsitrationProcess.PageObjects;
using TechTalk.SpecFlow;

namespace ProjectBDDRegsitrationProcess.StepFiles
{
    [Binding]
    public sealed class SignInSteps
    {
        Testdatagenerator datagenerator = GeneralHooks.GetContext();
        CreateAccountPage createAccountPage = new CreateAccountPage();
        CartItemCheckoutPage cartItemCheckoutPage = new CartItemCheckoutPage();

        [When(@"User enter emailaddress to create a account")]
        public void WhenUserEnterToCreateAAccount(string p0)
        {
            createAccountPage.EnterPersonalInfo();
            createAccountPage.EnterAdddressDetails();
            cartItemCheckoutPage.ValidateLoggedInUser();
        }

        [Given(@"user is on My Store HomePage")]
        public void GivenUserIsOnMyStoreHomePage()
        {
            bool signInLinkDisplayed = createAccountPage.SignIn.Displayed;
        }

        [When(@"user enters new emailaddress")]
        public void WhenUserEntersNewEmailaddress()
        {
            createAccountPage.ClickSignIn();
            createAccountPage.EnterNewEmailAddressToRegister();
        }

        [When(@"user submit create account page")]
        public void WhenUserSubmitCreateAccountPage()
        {
            createAccountPage.EnterPersonalInfo();
            createAccountPage.EnterAdddressDetails();
            cartItemCheckoutPage.ValidateLoggedInUser();
        }

        [When(@"user adds the product to cart")]
        public void WhenUserAddsTheProductToCart(Table table)
        {
            cartItemCheckoutPage.MaintabWomen(table);
            cartItemCheckoutPage.AddItemtoCart(table);
        }

        [When(@"user proceed to checkout")]
        public void WhenUserProceedToCheckout()
        {
            cartItemCheckoutPage.ProceedtoCheckout();
        }

        [When(@"user checkout Item Summary page")]
        public void WhenUserCheckoutItemSummaryPage(Table table)
        {
            cartItemCheckoutPage.StartCheckoutProcess(table);
        }

        [When(@"user proceed on Billing Address page")]
        public void WhenUserProceedOnBillingAddressPage()
        {
            cartItemCheckoutPage.AddressScreenCheckout();
        }

        [When(@"user agree to terms of service")]
        public void WhenUserAgreeToTermsOfService()
        {
            cartItemCheckoutPage.AgreeTOTOS();
        }

        [Then(@"user should see order summary")]
        public void ThenUserShouldSeeOrderSummary()
        {
            cartItemCheckoutPage.ConfirmPayment();
        }

        [Then(@"user should see order confirmation")]
        public void ThenUserShouldSeeOrderConfirmation(Table table)
        {
            cartItemCheckoutPage.OrderConfirmation(table);
        }

        [When(@"user submit incomplete Create AccountPage")]
        public void WhenUserSubmitIncompleteCreateAccountPage()
        {
            createAccountPage.RegisterButton.Click();
        }

        [Then(@"user should see errormessage")]
        public void ThenUserShouldSee(Table table)
        {
            cartItemCheckoutPage.ValidateErrorMessagesonRegistration(table);
        }
    }
}
