using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NUnitFramework.PageObject
{
    public class LoginPage
    {
        protected IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement username;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "login-button")]
        private IWebElement loginButton;



        public void setUserName(string userName)
        {
            username.SendKeys(userName);
        }

        public void setPassword(string passw)
        {
            password.SendKeys(passw);
        }

        public ProductsPage loginSuccessfully()
        {
            loginButton.Click();
            return new ProductsPage(driver);
        }
    }
}
