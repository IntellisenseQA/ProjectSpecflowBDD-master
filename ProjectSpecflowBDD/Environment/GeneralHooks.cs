using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace ProjectBDDRegsitrationProcess.Environment
{
    [Binding]
    public class GeneralHooks
    {
        private readonly IWebDriver driver;

        public static Testdatagenerator GetContext()
        {
            return ScenarioContext.Current.Get<Testdatagenerator>("TestData");
        }
        [BeforeScenario]
        public static void BeforeScenario()
        {
            Env env = Env.Dev;

            var config = ConfigurationHelper.InitConfiguration();
            Testdatagenerator testdatagenrator = new Testdatagenerator();
            switch (env)
            {
                case Env.Local:
                    testdatagenrator.baseurl = config.GetSection("AppSettings")["localappurl"];
                    break;

                case Env.Dev:
                    testdatagenrator.baseurl = config.GetSection("AppSettings")["DevEnvWebURL"];
                    testdatagenrator.DevuserSignInPassword = config.GetSection("AppSettings")["DevEnvUserPassword"];
                    var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(1);
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl(testdatagenrator.baseurl);
                    ScenarioContext.Current.Add("currentDriver", driver);
                    break;
            }
            ScenarioContext.Current.Set<Testdatagenerator>(testdatagenrator, "TestData");
        }

        [AfterScenario("web")]
        public void RunAfterScenario()
        {
            var driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }
}
