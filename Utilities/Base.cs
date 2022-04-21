using System;
using System.Configuration;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnitFramework.Utilities
{
    public class Base
    {
        protected ExtentReports extent;
        protected ExtentTest test;
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host","Local");
            extent.AddSystemInfo("Environment", ConfigurationManager.AppSettings["environment"]);
            htmlReporter.Config.Theme = Theme.Dark;
        }

        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            InitBrowser(ConfigurationManager.AppSettings["browser"]);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            //driver.Value.Url = "https://www." + ConfigurationManager.AppSettings["environment"] + "rest of the URL";
            driver.Url = ConfigurationManager.AppSettings["urlWebApp"];
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                default:
                    throw new NotFoundException("The Webdriver is not available");
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime dTime = DateTime.Now;
            string fileName = "Screenshot_" + dTime.ToString("h_mm_ss") + ".jpg";

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed", captureScreenShot(driver, fileName));
                test.Log(Status.Fail,"Test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Pass", captureScreenShot(driver, fileName));
            }

            driver.Quit();
            extent.Flush();
        }
    }
}
