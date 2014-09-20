using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap17.Entities.Examples;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Books_identity_is_its_ISBN()
        {
            var isbnForThisBook = "9781118714706";
            var isbn = new ISBN(isbnForThisBook);

            var book = new Book(isbn);

            Assert.AreEqual(isbn, book.ISBN);
            Assert.AreEqual(isbnForThisBook, book.Id);
        }
    }
}
