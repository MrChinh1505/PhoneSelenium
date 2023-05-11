using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace MobilePhone_Selenium
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void TestInitialize()
        {
            webDriver = new ChromeDriver();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            if (webDriver != null)
            {
                webDriver.Quit();
            }
        }

        [DataTestMethod]
        [DataRow("A001", "password123", "https://localhost:7296/")]
        [DataRow("A002", "password456", "https://localhost:7296/")]
        [DataRow("A003", "password789", "https://localhost:7296/")]
        [DataRow("wrong", "wrong", "https://localhost:7296/Account/Auth")]
        [DataRow("test", "test", "https://localhost:7296/Account/Auth")]
        public void TestLogin(string name, string pwd, string expected)
        {

            By username = By.XPath("/html/body/div[1]/main/div/form/div[1]/input");
            By password = By.XPath("/html/body/div[1]/main/div/form/div[2]/input");
            By submit = By.XPath("/html/body/div[1]/main/div/form/div[3]/button");

            webDriver.Navigate().GoToUrl("https://localhost:7296/");
            Thread.Sleep(1000);
            webDriver.Navigate().GoToUrl("https://localhost:7296/Account/Auth");
            webDriver.FindElement(username).Clear();
            webDriver.FindElement(username).SendKeys(name);
            webDriver.FindElement(password).Clear();
            webDriver.FindElement(password).SendKeys(pwd);
            Thread.Sleep(1000);
            webDriver.FindElement(submit).Click();
            //Thread.Sleep(6000);
            Thread.Sleep(1000);
            Assert.AreEqual(expected, webDriver.Url);

            //Thread.Sleep(1000);
            webDriver.Quit();
        }


        [TestMethod]
        public void TestSuccess()
        {
            By username = By.XPath("/html/body/div[1]/main/div/form/div[1]/input");
            By password = By.XPath("/html/body/div[1]/main/div/form/div[2]/input");
            By submit = By.XPath("/html/body/div[1]/main/div/form/div[3]/button");

            webDriver.Navigate().GoToUrl("https://localhost:7296/");
            Thread.Sleep(1000);
            webDriver.Navigate().GoToUrl("https://localhost:7296/Account/Auth");
            webDriver.FindElement(username).Clear();
            webDriver.FindElement(password).Clear();
            Thread.Sleep(1000);
            webDriver.FindElement(username).SendKeys("A001");
            webDriver.FindElement(password).SendKeys("password123");
            Thread.Sleep(1000);
            webDriver.FindElement(submit).Click();
            webDriver.FindElement(By.XPath("/html/body/div[1]/main/section/div/div/div[1]/div/div/div/button")).Click();
            Thread.Sleep(1000);
            webDriver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
            webDriver.Navigate().GoToUrl("https://localhost:7296/Cart");
            Thread.Sleep(1000);
            webDriver.FindElement(By.XPath("/html/body/div[1]/main/div/div[1]/table/tfoot/tr/td/a[1]")).Click();
            Thread.Sleep(1000);
            webDriver.FindElement(By.XPath("/html/body/div[1]/main/form/div/div[2]/div/div[5]/button")).Click();
            Thread.Sleep(1000);

            Assert.AreEqual("https://localhost:7296/Order/Success", webDriver.Url);

        }

        [TestMethod]
        public void TestFail()
        {
            webDriver.Navigate().GoToUrl("https://localhost:7296");
            webDriver.FindElement(By.XPath("/html/body/div[1]/main/section/div/div/div[1]/div/div/div/button")).Click();
            Thread.Sleep(1000);
            webDriver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
            webDriver.Navigate().GoToUrl("https://localhost:7296/Cart");

            Assert.AreEqual("https://localhost:7296/Account/Auth", webDriver.Url);

        }
    }
}
