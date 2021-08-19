using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XanpoolAPI.Models;
using XanpoolAPI.Services.Interfaces;

namespace XanpoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(
            IBookService bookService,
            ILogger<BookController> logger
        )
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                var books = _bookService.Get();
                _logger.LogInformation("Fetching all the books from the storage");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when fetch all books : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetBook/{id}")]
        public ActionResult<Book> Get(string id)
        {
            try
            {
                var book = _bookService.Get(id);

                if (book == null)
                {
                    return NotFound($"Item not found for {id}");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching the item : {ex}");
                return StatusCode(500, "Internal server error");
            }
                
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            try
            {
                _bookService.Create(book);

                return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong on creating book : {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Book bookIn)
        {
            try
            {
                var book = _bookService.Get(id);

                if (book == null)
                {
                    return NotFound();
                }

                _bookService.Update(id, bookIn);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating book : {ex}");
                return StatusCode(500, "Internal server error");
            }
                
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var book = _bookService.Get(id);

                if (book == null)
                {
                    return NotFound();
                }

                _bookService.Remove(book);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating book : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
