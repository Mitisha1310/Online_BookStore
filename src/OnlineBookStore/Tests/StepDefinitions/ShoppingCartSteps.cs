using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    [Binding]
    public class ShoppingCartSteps
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

        [Given(@"the user is on the product page")]
        public void GivenTheUserIsOnTheProductPage()
        {
            // Navigate to the product page
            driver.Navigate().GoToUrl("https://yourapp.com/products");
            // Additional setup steps can be added as needed
        }

        [When(@"the user adds a book with title ""(.*)"" to the shopping cart")]
        public void WhenTheUserAddsABookToTheShoppingCart(string bookTitle)
        {
            // Locate the book on the product page and add it to the shopping cart
            IWebElement addToCartButton = driver.FindElement(By.XPath($"//div[@title='{bookTitle}']/button[@class='add-to-cart']"));
            addToCartButton.Click();
        }

        [When(@"adds a music CD with title ""(.*)"" to the shopping cart")]
        public void WhenAddsAMusicCDToTheShoppingCart(string cdTitle)
        {
            // Locate the CD on the product page and add it to the shopping cart
            IWebElement addToCartButton = driver.FindElement(By.XPath($"//div[@title='{cdTitle}']/button[@class='add-to-cart']"));
            addToCartButton.Click();
        }

        [Then(@"the shopping cart should display both items")]
        public void ThenTheShoppingCartShouldDisplayBothItems()
        {
            // Verify that both items are displayed in the shopping cart
            // Additional assertions can be added based on the actual structure of your shopping cart
            Assert.IsTrue(driver.PageSource.Contains("The Great Gatsby"));
            Assert.IsTrue(driver.PageSource.Contains("Abbey Road"));
        }

        [Then(@"the total price in the cart should be correct")]
        public void ThenTheTotalPriceInTheCartShouldBeCorrect()
        {
            // Verify that the total price is correct based on the added items
            // Additional assertions can be added based on the actual structure of your shopping cart
            Assert.IsTrue(driver.PageSource.Contains("$50.00"));
        }

        // Implement steps for the other scenarios (removing items, updating quantities, emptying the cart) as needed
    }
}




using System.Text;
using BookShop.Mvc.Logic;
using BookShop.Mvc.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace BookShop.UnitTests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void GetShoppingCart_EmptySession_EmptyShoppingCart()
        {
            var shoppingCartLogic = new ShoppingCartLogic(new Mock<IDatabaseContext>().Object);

            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny)).Returns(false);


            var shoppingCart = shoppingCartLogic.GetShoppingCart(sessionMock.Object);

            shoppingCart.Count.Should().Be(0);
        }

        [Fact]
        public void GetShoppingCart_StoredInSession()
        {
            var shoppingCartLogic = new ShoppingCartLogic(new Mock<IDatabaseContext>().Object);

            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny)).Returns(false);


            var shoppingCart = shoppingCartLogic.GetShoppingCart(sessionMock.Object);

            sessionMock.Verify(m => m.Set("CART", It.IsAny<byte[]>()), Times.Once);
        }

        delegate void GobbleCallback(string key, out byte[] data);

        [Fact]
        public void GetShoppingCart_AlreadySavedShoppingCart_IsReturned()
        {
            var cart = new ShoppingCart();
            cart.AddLineItem(new OrderLine()
            {
                BookId = 1,
                Quantity = 1,
                OrderId = 1,
                Book = new Book()
                {
                    Id = 1,
                    Price = 1m
                }
            });
            var serializedCart = JsonConvert.SerializeObject(cart);


            var shoppingCartLogic = new ShoppingCartLogic(new Mock<IDatabaseContext>().Object);

            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
                .Callback(new GobbleCallback((string key, out byte[] data) =>
                {
                    data = Encoding.UTF8.GetBytes(serializedCart);
                })).Returns(true);


            var shoppingCart = shoppingCartLogic.GetShoppingCart(sessionMock.Object);

            shoppingCart.Should().BeEquivalentTo(cart);
        }
    }
}