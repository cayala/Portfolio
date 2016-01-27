using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SeleniumTest
{
    [TestClass]
    public class Selenium
    {
        IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            //start Browser and openURL

        }

        [TestMethod]
        public void FireFoxTest()
        {
            //write actual test
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://upg514.partsmartweb.com/scripts/EmpartISAPI.dll?MF&app=Demo&lang=EN&TF=Empartweb&loginID=mtd2014&Loginpwd=test");

            Func<string, IWebDriver> selectChildFrame = (x) => driver.SwitchTo().Frame(driver.FindElement(By.XPath(x)));
            Action<string> click = (string x) => driver.FindElement(By.XPath(x)).Click();
            Action<string> fElement = (string x) => driver.FindElement(By.XPath(x));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
            
            //Step into Main Frame
            
            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            
            //Click into parts tab
            
            click.Invoke("//td[contains(@id, 'Tab1')]");
            
            //step into search part frame and search for a part containing '2008'
            
            selectChildFrame.Invoke("//iframe[contains(@name, 'iframeSearchPart')]");
            click.Invoke("//input[contains(@name, 'partName')]");
            driver.FindElement(By.XPath("//input[contains(@name, 'partName')]")).SendKeys("2008");
            click.Invoke("//input[contains(@value, 'Search')]");
            
            //step into iframePTSrchResults and click part of 777D12008
            
            selectChildFrame.Invoke("//iframe[contains(@id, 'iframePTSrchResults')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'PartSearchResults')]");
            click.Invoke("//td[contains(text(), '777D12008')]");
            driver.SwitchTo().ParentFrame();
            selectChildFrame.Invoke("//frame[contains(@name, 'PartSearchDetails')]");
            click.Invoke("//span[contains(text(), '11A-544B004 (2009)-->Label Map MTD Gold')]");
            
            //view super session and add to pick list

            driver.SwitchTo().DefaultContent();
            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeSearchModel')]");
            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeMDSrchResults')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'ModelSearchDetails')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'IPL1361227DATA')]");
            driver.FindElement(By.XPath("//tr[contains(@id, '777S30145')]")).FindElement(By.XPath("//tr/td/a")).Click();
            driver.FindElement(By.XPath("//tr[contains(@id, '777S30145')]")).FindElement(By.XPath("//a[contains(@href, 'addToPicklist')]")).Click();

            //navigate to model
            driver.SwitchTo().DefaultContent();
            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            click.Invoke("//td[contains(@id, 'Tab0')]");

            //search a model of 2007 in MTD2014

            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeSearchModel')]");
            driver.FindElement(By.XPath("//td[contains(@class, 'oemDropDownContainer')]")).FindElement(By.XPath("//td/select")).Click();
            driver.FindElement(By.XPath("//option[contains(@value, '5010')]")).Click();
            click.Invoke("//input[contains(@name, 'modelName')]");
            driver.FindElement(By.XPath("//input[contains(@name, 'modelName')]")).SendKeys("2007");
            click.Invoke("//input[contains(@value, 'Search')]");
        }

        [TestMethod]
        public void ChromeTest() 
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://upg514.partsmartweb.com/scripts/EmpartISAPI.dll?MF&app=Demo&lang=EN&TF=Empartweb&loginID=mtd2014&Loginpwd=test");

            Func<string, IWebDriver> selectChildFrame = (x) => driver.SwitchTo().Frame(driver.FindElement(By.XPath(x)));
            Action<string> click = (string x) => driver.FindElement(By.XPath(x)).Click();
            Action<string> fElement = (string x) => driver.FindElement(By.XPath(x));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

            //Step into Main Frame

            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");

            //Click into parts tab

            click.Invoke("//td[contains(@id, 'Tab1')]");

            //step into search part frame and search for a part containing '2008'

            selectChildFrame.Invoke("//iframe[contains(@name, 'iframeSearchPart')]");
            click.Invoke("//input[contains(@name, 'partName')]");
            driver.FindElement(By.XPath("//input[contains(@name, 'partName')]")).SendKeys("2008");
            click.Invoke("//input[contains(@value, 'Search')]");

            //step into iframePTSrchResults and click part of 777D12008

            selectChildFrame.Invoke("//iframe[contains(@id, 'iframePTSrchResults')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'PartSearchResults')]");
            click.Invoke("//td[contains(text(), '777D12008')]");
            driver.SwitchTo().ParentFrame();
            selectChildFrame.Invoke("//frame[contains(@name, 'PartSearchDetails')]");
            click.Invoke("//span[contains(text(), '11A-544B004 (2009)-->Label Map MTD Gold')]");

            //view super session and add to pick list

            driver.SwitchTo().DefaultContent();
            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeSearchModel')]");
            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeMDSrchResults')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'ModelSearchDetails')]");
            selectChildFrame.Invoke("//frame[contains(@name, 'IPL1361227DATA')]");
            driver.FindElement(By.XPath("//tr[contains(@id, '777S30145')]")).FindElement(By.XPath("//tr/td/a")).Click();
            driver.FindElement(By.XPath("//tr[contains(@id, '777S30145')]")).FindElement(By.XPath("//a[contains(@href, 'addToPicklist')]")).Click();

            //navigate to model
            driver.SwitchTo().DefaultContent();
            selectChildFrame.Invoke("//frame[contains(@name, 'mainWindow')]");
            click.Invoke("//td[contains(@id, 'Tab0')]");

            //search a model of 2007 in MTD2014

            selectChildFrame.Invoke("//iframe[contains(@id, 'iframeSearchModel')]");
            driver.FindElement(By.XPath("//td[contains(@class, 'oemDropDownContainer')]")).FindElement(By.XPath("//td/select")).Click();
            driver.FindElement(By.XPath("//option[contains(@value, '5010')]")).Click();
            click.Invoke("//input[contains(@name, 'modelName')]");
            driver.FindElement(By.XPath("//input[contains(@name, 'modelName')]")).SendKeys("2007");
            click.Invoke("//input[contains(@value, 'Search')]");
        }

        [TestCleanup]
        public void CleanUp() 
        {
            //Close browser
            //driver.Quit();
        }
    }
}
