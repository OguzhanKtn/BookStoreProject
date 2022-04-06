using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBookById;
using static WebApi.BookOperations.GetBookById.GetBookByIdQuery;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery getBook = new GetBookByIdQuery(_context,_mapper);
            GetBookByIdModel result;
            GetBookByIdValidator validator = new GetBookByIdValidator();
            try
            {
                getBook.BookId = id;
                validator.ValidateAndThrow(getBook);
                result = getBook.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            try
            {
                command.Model = newBook;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookValidator validator = new UpdateBookValidator();
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            DeleteBookValidator validator = new DeleteBookValidator();
            try
            {
                command.BookId = id;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}