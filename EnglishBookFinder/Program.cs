using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EnglishBookFinder
{
    internal class Program
    {
        private static readonly BookSource[] BookSources =
        {
            new BookSource("B2i", "https://english-e-reader.net/level/intermediate-plus"),
            new BookSource("B2+", "https://english-e-reader.net/level/upper-intermediate"),
            new BookSource("C1", "https://english-e-reader.net/level/advanced")
        };

        private static void Main(string[] args)
        {
            var driver = CreateWebDriver();
            var bookList = new List<Book>();
            foreach (var bookSource in BookSources)
            {
                Console.WriteLine($"Loading source '{bookSource.FriendlyName}'");
                driver.Url = bookSource.Url;
                driver.Navigate();

                var bookElements = driver.FindElements(By.ClassName("book-container"));
                var aTags = bookElements.Select(x => x.FindElement(By.TagName("a"))).ToList();

                foreach (var bookLink in aTags)
                {
                    var bookName = bookLink.FindElement(By.TagName("h4")).Text;
                    var bookUrl = bookLink.GetAttribute("href");
                    bookList.Add(new Book(bookName, bookUrl, bookSource));
                }

                Console.WriteLine($"{aTags.Count} books found in '{bookSource.FriendlyName}'");
            }

            foreach (var book in bookList)
            {
                Console.WriteLine($"Gathering {book.Name}...");
                driver.Url = book.Url;
                driver.Navigate();

                var text = driver.FindElement(
                    By.ClassName("panel-title")).Text;
                book.TotalWords = Convert.ToInt32(text.Split(':')[3]);
            }

            Console.WriteLine("--------------------------------------------");
            bookList = bookList.OrderBy(x => x.TotalWords).ToList();
            for (var i = 0; i < bookList.Count; i++)
            {
                var x = bookList[i];
                Console.WriteLine($"{i + 1}. \t {x.Source.FriendlyName} | {x.Name}: {x.TotalWords}");
            }

            Console.ReadKey();
        }

        private static IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver("driver", options);
        }
    }
}