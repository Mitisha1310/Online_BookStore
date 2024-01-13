using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    [Binding]
    public class SearchBooksSteps
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

        [Given(@"the user is on the online book store website")]
        public void GivenTheUserIsOnTheOnlineBookStoreWebsite()
        {
            // Navigate to the online book store website
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            // Additional setup steps can be added as needed
        }

        [When(@"the user searches for a book with title ""(.*)""")]
        public void WhenTheUserSearchesForABookWithTitle(string bookTitle)
        {
            // Locate the search bar and enter the book title
            IWebElement searchInput = driver.FindElement(By.Id("search"));
            searchInput.SendKeys(bookTitle);

            // Click on the search button
            IWebElement searchButton = driver.FindElement(By.Id("search-button"));
            searchButton.Click();
        }

        [Then(@"the search results should include books related to ""(.*)""")]
        public void ThenTheSearchResultsShouldIncludeBooksRelatedTo(string expectedBookTitle)
        {
            // Perform assertions on the search results
            IWebElement firstSearchResult = driver.FindElement(By.XPath("//div[@class='search-result'][1]"));
            string actualBookTitle = firstSearchResult.FindElement(By.ClassName("title")).Text;

            Assert.IsTrue(actualBookTitle.Contains(expectedBookTitle), "Search results do not contain the expected book title");
        }
    }
}



using BookShop.AcceptanceTests.Drivers;
using TechTalk.SpecFlow;

namespace BookShop.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class BookSteps
    {
        private readonly IBookDetailsDriver _driver;
        private readonly DatabaseDriver _databaseDriver;

        public BookSteps(IBookDetailsDriver driver, DatabaseDriver databaseDriver)
        {
            _driver = driver;
            _databaseDriver = databaseDriver;
        }

        [Given(@"the following books")]
        public void GivenTheFollowingBooks(Table givenBooks)
        {
            _databaseDriver.AddToDatabase(givenBooks);
        }

        [When(@"I open the details of '(.*)'")]
        public void WhenIOpenTheDetailsOfBook(string bookTitle)
        {
            _driver.OpenBookDetails(bookTitle);
        }

        [Then(@"the book details should show")]
        public void ThenTheBookDetailsShouldShow(Table expectedBookDetails)
        {
            _driver.ShowsBookDetails(expectedBookDetails);
        }
    }
}