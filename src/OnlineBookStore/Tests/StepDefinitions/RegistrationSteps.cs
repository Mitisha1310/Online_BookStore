using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    [Binding]
    public class RegistrationSteps
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

        [Given(@"the user is on the registration page")]
        public void GivenTheUserIsOnTheRegistrationPage()
        {
            // Navigate to the registration page
            driver.Navigate().GoToUrl("https://yourapp.com/register");
            // Additional setup steps can be added as needed
        }

        [When(@"the user enters valid registration details")]
        public void WhenTheUserEntersValidRegistrationDetails()
        {
            // Locate and fill in the registration form with valid details
            IWebElement firstNameInput = driver.FindElement(By.Id("first-name"));
            IWebElement lastNameInput = driver.FindElement(By.Id("last-name"));
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));

            firstNameInput.SendKeys("John");
            lastNameInput.SendKeys("Doe");
            emailInput.SendKeys("john.doe@example.com");
            passwordInput.SendKeys("Password123");
        }

        [When(@"clicks the register button")]
        public void WhenClicksTheRegisterButton()
        {
            // Locate and click the register button
            IWebElement registerButton = driver.FindElement(By.Id("register-button"));
            registerButton.Click();
        }

        [Then(@"the user should be redirected to the welcome page")]
        public void ThenTheUserShouldBeRedirectedToTheWelcomePage()
        {
            // Verify that the current URL is the welcome page URL
            Assert.AreEqual("https://yourapp.com/welcome", driver.Url);
        }

        [Then(@"a welcome message should be displayed")]
        public void ThenAWelcomeMessageShouldBeDisplayed()
        {
            // Verify that a welcome message is displayed on the welcome page
            IWebElement welcomeMessage = driver.FindElement(By.ClassName("welcome-message"));
            Assert.IsTrue(welcomeMessage.Displayed, "Expected welcome message is not displayed");
        }

        [When(@"the user enters an already registered email")]
        public void WhenTheUserEntersAnAlreadyRegisteredEmail()
        {
            // Locate and fill in the registration form with an already registered email
            IWebElement firstNameInput = driver.FindElement(By.Id("first-name"));
            IWebElement lastNameInput = driver.FindElement(By.Id("last-name"));
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));

            firstNameInput.SendKeys("Jane");
            lastNameInput.SendKeys("Doe");
            emailInput.SendKeys("existing.user@example.com");
            passwordInput.SendKeys("Password123");
        }

        [Then(@"an error message should be displayed indicating the email is already in use")]
        public void ThenAnErrorMessageShouldBeDisplayedIndicatingTheEmailIsAlreadyInUse()
        {
            // Verify that an error message is displayed on the registration page
            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message"));
            Assert.IsTrue(errorMessage.Displayed, "Expected error message is not displayed");
        }

        [Then(@"the user should remain on the registration page")]
        public void ThenTheUserShouldRemainOnTheRegistrationPage()
        {
            // Verify that the current URL is still the registration page URL
            Assert.AreEqual("https://yourapp.com/register", driver.Url);
        }
    }
}
