using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Login
{
    [TestClass]
    public class LoginSteps
    {
        private IWebDriver driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Set up your WebDriver here (e.g., ChromeDriver)
            driver = new ChromeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Clean up resources after the scenario
            driver.Quit();
        }

        [Given(@"the user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl("https://automationbookstore.dev/");
            // Additional setup steps can be added as needed
        }

        [When(@"the user enters valid username ""(.*)"" and valid password ""(.*)""")]
        public void WhenTheUserEntersValidUsernameAndPassword(string username, string password)
        {
            // Locate the username and password input fields and enter the values
            IWebElement usernameInput = driver.FindElement(By.Id("username"));
            usernameInput.SendKeys(username);

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password);
        }

        [When(@"clicks the login button")]
        public void WhenClicksTheLoginButton()
        {
            // Locate and click the login button
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();
        }

        [Then(@"the user should be redirected to the dashboard")]
        public void ThenTheUserShouldBeRedirectedToTheDashboard()
        {
            // Verify that the current URL is the dashboard URL
            Assert.AreEqual("https://yourapp.com/dashboard", driver.Url);
        }

        [Then(@"the user profile should be displayed with the username ""(.*)""")]
        public void ThenTheUserProfileShouldBeDisplayedWithTheUsername(string expectedUsername)
        {
            // Verify that the user profile contains the expected username
            IWebElement userProfile = driver.FindElement(By.ClassName("user-profile"));
            string actualUsername = userProfile.FindElement(By.ClassName("username")).Text;

            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Then(@"an error message should be displayed indicating invalid credentials")]
        public void ThenAnErrorMessageShouldBeDisplayedIndicatingInvalidCredentials()
        {
            // Verify that an error message is displayed on the login page
            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message"));
            Assert.IsTrue(errorMessage.Displayed, "Expected error message is not displayed");
        }

        [Then(@"the user should remain on the login page")]
        public void ThenTheUserShouldRemainOnTheLoginPage()
        {
            // Verify that the current URL is still the login page URL
            Assert.AreEqual("https://yourapp.com/login", driver.Url);
        }
    }
}
