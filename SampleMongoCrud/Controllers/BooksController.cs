using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SampleMongoCrud.Services;
using SampleMongoCrud.Models;

namespace SampleMongoCrud.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(bool test = false)
        {
            if (test)
            {
                _bookService = new BookServiceMock();
            }
            else
            {
                _bookService = new BookService();
            }
        }

        /// <summary>Return books in pages of 10.</summary>
        /// <param name="pageNumber">Starts in page 1</param>
		/// <response code="200">Success</response>
		[HttpGet]
        [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
        public IActionResult AllBooks(int? pageNumber = 1)
        {
            try
            {
                int? toSkip = (pageNumber - 1) * 10;
                IEnumerable<Book> response = _bookService.Get(toSkip);

                if (response.Count() == 0)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>Search a book for a given id.</summary>
        /// <param name="id">id</param>
		/// <response code="200">Success</response>
		[HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        public IActionResult BookById(string id)
        {
            try
            {
                Book found = _bookService.Get(id);

                if (found == null)
                    return NotFound();

                return Ok(found);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>Post a new book.</summary>
        /// <param name="book">New book object</param>
		/// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        public IActionResult CreateBook([FromBody] BookInfo book)
        {
            try
            {
                Book toCreate = JsonCasting.ChangeType<BookInfo, Book>(book);
                return Created("CreateBook", _bookService.Create(toCreate));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>Updates a book.</summary>
        /// <param name="update">Update book object</param>
        /// <param name="id">Id of the book to be updated</param>
		/// <response code="200">Success</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateBook(string id, [FromBody] BookInfo update)
        {
            try
            {
                Book toUpdate = _bookService.Get(id);

                if (toUpdate == null)
                    return NotFound();

                Book theUpdate = JsonCasting.ChangeType<BookInfo, Book>(update);
                theUpdate.Id = toUpdate.Id;
                _bookService.Update(id, theUpdate);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>Deletes a book.</summary>
        /// <param name="id">Id of the book to be deleted</param>
		/// <response code="200">Success</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Remove(string id)
        {
            try
            {
                Book toRemove = _bookService.Get(id);

                if (toRemove == null)
                    return NotFound();

                _bookService.Remove(toRemove.Id);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}