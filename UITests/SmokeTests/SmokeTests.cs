using System.Collections.Generic;
using NUnit.Framework;
using NUnitFramework.PageObject;
using NUnitFramework.Utilities;
using OpenQA.Selenium;

namespace NUnitFramework
{
    public class SmokeTests : Base
    { 
        [Test]
        public void Test1()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName("standard_user");
            loginPage.setPassword("secret_sauce");
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        public void Test2(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName(username);
            loginPage.setPassword(password);
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCase("wrongUser", "secret_sau")]
        public void Test3(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.setUserName(username);
            loginPage.setPassword(password);
            ProductsPage productsPage = loginPage.loginSuccessfully();
            productsPage.waitForProductPageIsDisplayed();
        }

        [Test]
        [TestCaseSource("AddTestDataConfig")]
        public void Test4(string username, string password)
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
