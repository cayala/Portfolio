using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SET3
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        
        [TestMethod]
        public void TestMethod1()
        {
            //write actual test
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("");

            Func<string, IWebDriver> selectChildFrame = (x) => driver.SwitchTo().Frame(driver.FindElement(By.XPath(x)));
            Action<string> click = (string x) => driver.FindElement(By.XPath(x)).Click();
            Action<string> fElement = (string x) => driver.FindElement(By.XPath(x));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

            //Step into Main Frame

            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            selectChildFrame.Invoke("//frame/frame");
            selectChildFrame.Invoke("//div[contains(@id, 'tab_sheets_container']/iframe[contains(@id, 'iframeSearchPart']");

        }
    }
}
