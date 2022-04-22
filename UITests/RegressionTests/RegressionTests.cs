using System.Collections.Generic;
using NUnit.Framework;
using NUnitFramework.PageObject;
using NUnitFramework.Utilities;
using OpenQA.Selenium;

namespace NUnitFramework
{
    public class RegressionTests : BaseSetUp
    { 
        [Test]
        public void Test5()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName("standard_user");
            loginPage.setPassword("secret_sauce");
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        public void Test6(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName(username);
            loginPage.setPassword(password);
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        public void Test7(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName(username);
            loginPage.setPassword(password);
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCaseSource("AddTestDataConfig")]
        public void Test8(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName(username);
            loginPage.setPassword(password);
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        { 
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
        }
    }
}
