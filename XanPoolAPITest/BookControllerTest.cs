using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XanpoolAPI.Controllers;
using XanpoolAPI.Models;
using XanpoolAPI.Services.Interfaces;

namespace XanPoolAPITest
{
    [TestFixture]
    public class BookControllerTest
    {
        private List<Book> bookList;

        [SetUp]
        public void Setup()
        {
            bookList = new List<Book>
            {
                new Book
                {
                    Id = "bk123",
                    Title = "Rich Dad Poor Dad",
                    Description = "Book about investments"
                },
                new Book
                {
                    Id = "bk124",
                    Title = "Intelligent investor",
                    Description = "How to invest on stock smartly."
                }
            };
        }


        [Test]
        public async Task Get_BookServiceMockedWithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(repo => repo.Get())
                .Returns(bookList);

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookService.Object, mockLogger.Object);

            // Act
            var okResult = controller.Get().Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.IsAssignableFrom<List<Book>>(okResult.Value);
            Assert.AreEqual(bookList, okResult.Value);

        }
        
        [Test]
        public async Task Get_BookServiceMockedWithThrowingException_ReturnsInternalServerError()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(repo => repo.Get())
                .Throws(new Exception());

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookService.Object, mockLogger.Object);

            // Act
            var internalServerErrorResult = controller.Get().Result as ObjectResult;

            // Assert
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);

        }

        [Test]
        public async Task GetById_BookServiceMockedWithValidIdExistOnMockData_ReturnsOkResult()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(x => x.Get("bk124")).Returns(bookList.Find( x => x.Id == "bk124"));

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookService.Object, mockLogger.Object);

            // Act
            var okResult = controller.Get("bk124").Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.IsAssignableFrom<Book>(okResult.Value);

        }

        [Test]
        public async Task GetById_BookServiceMockedWithReturnNull_ReturnsNotFoundResult()
        {


            // Arrange
            Book bookNullable = null;
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(x => x.Get("bk124")).Returns(bookNullable);

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookService.Object, mockLogger.Object);

            // Act
            var notFoundResult = controller.Get("bk124").Result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

        }

        [Test]
        public async Task GetById_BookServiceMockedWithThrowingException_ReturnsInternalServerError()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(repo => repo.Get("bk124"))
                .Throws(new Exception());

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookService.Object, mockLogger.Object);

            // Act
            var internalServerErrorResult = controller.Get("bk124").Result as ObjectResult;

            // Assert
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);

        }

    }
}
