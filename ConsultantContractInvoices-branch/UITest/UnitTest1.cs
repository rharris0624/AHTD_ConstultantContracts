using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Linq;
//using UserAuthorization.Data.Dto.Response;
using ConsultantContracts.Infrastructure.Service;
using ConsultantContractsInternal.Models;
using System.Collections.Specialized;
using Rhino.Mocks;
using Rhino.Mocks.Impl.Invocation;
using Rhino.Mocks.Impl.InvocationSpecifications;
using System.Security.Principal;

namespace SeleniumTests
{
	
	[TestClass]
	public class CreateTest
	{
		private IWebDriver driver;
		private StringBuilder verificationErrors;
		private string baseURL;
		private bool acceptNextAlert = true;
		private ConsultantContractsEntities _consultantContextEntities;
		private HttpRequestBase request;

		[TestInitialize]
		public void SetupTest()
		{
			////driver = new InternetExplorerDriver(@"C:\iedriver\");
			//driver = new FirefoxDriver();
			//baseURL = "http://localhost:61349";
			//verificationErrors = new StringBuilder();
			HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null))
				{
					User = new WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent())
				};
//            System.Security.Principal.IPrincipal principal = new System.Security.Principal.WindowsPrincipal();
//            YourControllerToBeTestedController = GetYourToBeTestedController();
		}

		[TestCleanup]
		public void TeardownTest()
		{
			try
			{
				//driver.Quit();
				_consultantContextEntities.Dispose();
			}
			catch (Exception)
			{
				// Ignore errors if unable to close the browser
			}
			//Assert.IsTrue(string.IsNullOrEmpty(verificationErrors.ToString()));
		}

		[TestMethod]
		public void GetUserProvDataTest()
		{
			var userProvInfoProvider = new UserProvInfo();
			var result = userProvInfoProvider.GetUserSecurity("rh41200");
			Assert.IsTrue(result != null && result.Length > 0);
			Assert.IsTrue(result != null && result.Length > 0 && result[1].Resource != null && result[1].Resource.ResourceId != null);
		}

		[TestMethod]
		public void GetContractPermissionsTest()
		{
			var availableDivs = ConsultantContractsInternal.Utilities.UserProvHelpers.AvailableDivisions(_consultantContextEntities, "rh41200");
			var response = ConsultantContractsInternal.Utilities.UserProvHelpers.GetContractPermissions(_consultantContextEntities, "rh41200", availableDivs);
			Assert.IsNotNull(response);
		}
		[TestMethod]
		public void GetUserAuthorizationDataTest()
		{
			var response = ConsultantContractsInternal.Utilities.UserProvHelpers.GetUserSecurity();
			Assert.IsTrue(response != null);
		}

		[TestMethod]
		public void TheCreateTest()
		{
			driver.Navigate().GoToUrl(baseURL + "/ConsultantContracts/Contracts/Create");
			driver.FindElement(By.Id("JobNo")).SendKeys("012014");
			for (int second = 0; ; second++)
			{
				if (second >= 60) Assert.Fail("timeout");
				try
				{
					if (driver.FindElements(By.CssSelector(".ui-autocomplete")).Any(e => e.Displayed)) break;
				}
				catch (Exception)
				{ }
				Thread.Sleep(1000);
			}
			driver.FindElement(By.Id("JobNo")).SendKeys(Keys.Enter);
			// ERROR: Caught exception [ERROR: Unsupported command [keyDown | id=JobNo | \13]]
			driver.FindElement(By.Id("Consultant")).SendKeys("010733400");
			for (int second = 0; ; second++)
			{
				if (second >= 60) Assert.Fail("timeout");
				try
				{
					if (driver.FindElements(By.CssSelector(".ui-autocomplete")).Any(e => e.Displayed)) break;
				}
				catch (Exception)
				{ }
				Thread.Sleep(1000);
			}
			driver.FindElement(By.Id("Consultant")).SendKeys(Keys.Enter);
			// ERROR: Caught exception [ERROR: Unsupported command [keyDown | id=Consultant | \13]]
			new SelectElement(driver.FindElement(By.Id("AgreementType"))).SelectByText("City/County Agreement");
			new SelectElement(driver.FindElement(By.Id("ResponsibleDivision"))).SelectByText("Env.");
			driver.FindElement(By.Id("ContractCeiling")).Clear();
			driver.FindElement(By.Id("ContractCeiling")).SendKeys("100000");
			driver.FindElement(By.Id("T1SvcsCeiling")).Clear();
			driver.FindElement(By.Id("T1SvcsCeiling")).SendKeys("90000");
			driver.FindElement(By.Id("T2SvcsCeiling")).Clear();
			driver.FindElement(By.Id("T2SvcsCeiling")).SendKeys("10000");
			driver.FindElement(By.Id("Multiplier")).Clear();
			driver.FindElement(By.Id("Multiplier")).SendKeys("1");
			// ERROR: Caught exception [ERROR: Unsupported command [removeSelection | //section[@id='content']/div[2]/select | label=Design]]
			// ERROR: Caught exception [ERROR: Unsupported command [addSelection | //section[@id='content']/div[2]/select | label=Construction]]
			driver.FindElement(By.XPath("(//option[@value=''])[7]")).Click();
			driver.FindElement(By.XPath("(//option[@value=''])[7]")).Click();
			driver.FindElement(By.LinkText("+Add")).Click();
			driver.FindElement(By.LinkText("+Add")).Click();
			driver.FindElement(By.CssSelector("td > input[type=\"text\"]")).Clear();
			driver.FindElement(By.CssSelector("td > input[type=\"text\"]")).SendKeys("thing1");
			driver.FindElement(By.XPath("(//input[@type='text'])[18]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[18]")).SendKeys("5");
			driver.FindElement(By.XPath("(//input[@type='text'])[19]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[19]")).SendKeys("10");
			driver.FindElement(By.XPath("(//input[@type='text'])[20]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[20]")).SendKeys("thing2");
			driver.FindElement(By.XPath("(//input[@type='text'])[21]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[21]")).SendKeys("1");
			driver.FindElement(By.XPath("(//input[@type='text'])[22]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[22]")).SendKeys("15");
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[2]")).Click();
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[2]")).Click();
			driver.FindElement(By.XPath("(//input[@type='text'])[23]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[23]")).SendKeys("thing3");
			driver.FindElement(By.XPath("(//input[@type='text'])[24]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[24]")).SendKeys("45");
			driver.FindElement(By.XPath("(//input[@type='text'])[25]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[25]")).SendKeys("50");
			driver.FindElement(By.XPath("(//input[@type='text'])[26]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[26]")).SendKeys("thing4");
			driver.FindElement(By.XPath("(//input[@type='text'])[27]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[27]")).SendKeys("50");
			driver.FindElement(By.XPath("(//input[@type='text'])[28]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[28]")).SendKeys("100");
			driver.FindElement(By.XPath("(//input[@type='text'])[29]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[29]")).SendKeys("1");
			driver.FindElement(By.Id("selectSubCon")).SendKeys("710389310");
			for (int second = 0; ; second++)
			{
				if (second >= 60) Assert.Fail("timeout");
				try
				{
					if (driver.FindElements(By.CssSelector(".ui-autocomplete")).Any(e => e.Displayed)) break;
				}
				catch (Exception)
				{ }
				Thread.Sleep(1000);
			}
			driver.FindElement(By.Id("selectSubCon")).SendKeys(Keys.Enter);
			// ERROR: Caught exception [ERROR: Unsupported command [keyDown | id=selectSubCon | \13]]
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[3]/td[2]/input")).Clear();
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[3]/td[2]/input")).SendKeys("50000");
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[4]/td[2]/input")).Clear();
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[4]/td[2]/input")).SendKeys("45000");
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[6]/td[2]/input")).Clear();
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[6]/td[2]/input")).SendKeys("5000");
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[11]/td[2]/input")).Clear();
			driver.FindElement(By.XPath("//section[@id='content']/div[7]/div[2]/div/table/tbody/tr[11]/td[2]/input")).SendKeys("1");
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[3]")).Click();
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[3]")).Click();
			driver.FindElement(By.XPath("(//input[@type='text'])[31]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[31]")).SendKeys("fov");
			driver.FindElement(By.XPath("(//input[@type='text'])[32]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[32]")).SendKeys("1");
			driver.FindElement(By.XPath("(//input[@type='text'])[33]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[33]")).SendKeys("2");
			driver.FindElement(By.XPath("(//input[@type='text'])[34]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[34]")).SendKeys("vovm");
			driver.FindElement(By.XPath("(//input[@type='text'])[35]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[35]")).SendKeys("3");
			driver.FindElement(By.XPath("(//input[@type='text'])[36]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[36]")).SendKeys("4");
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[4]")).Click();
			driver.FindElement(By.XPath("(//a[contains(text(),'+Add')])[4]")).Click();
			driver.FindElement(By.XPath("(//input[@type='text'])[37]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[37]")).SendKeys("vowmmo");
			driver.FindElement(By.XPath("(//input[@type='text'])[38]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[38]")).SendKeys("5");
			driver.FindElement(By.XPath("(//input[@type='text'])[39]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[39]")).SendKeys("6");
			driver.FindElement(By.XPath("(//input[@type='text'])[40]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[40]")).SendKeys("nana");
			driver.FindElement(By.XPath("(//input[@type='text'])[41]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[41]")).SendKeys("7");
			driver.FindElement(By.XPath("(//input[@type='text'])[42]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[42]")).SendKeys("8");
			driver.FindElement(By.CssSelector("textarea")).Clear();
			driver.FindElement(By.CssSelector("textarea")).SendKeys("Automated Test");
			driver.FindElement(By.CssSelector("#content > input[type=\"button\"]")).Click();
			driver.FindElement(By.XPath("(//input[@type='text'])[29]")).Clear();
			driver.FindElement(By.XPath("(//input[@type='text'])[29]")).SendKeys("1");
		}
		private bool IsElementPresent(By by)
		{
			try
			{
				driver.FindElement(by);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		private bool IsAlertPresent()
		{
			try
			{
				driver.SwitchTo().Alert();
				return true;
			}
			catch (NoAlertPresentException)
			{
				return false;
			}
		}

		private string CloseAlertAndGetItsText()
		{
			try
			{
				IAlert alert = driver.SwitchTo().Alert();
				string alertText = alert.Text;
				if (acceptNextAlert)
				{
					alert.Accept();
				}
				else
				{
					alert.Dismiss();
				}
				return alertText;
			}
			finally
			{
				acceptNextAlert = true;
			}
		}
	}
}
