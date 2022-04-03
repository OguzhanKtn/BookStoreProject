using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetBookByIdModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if(book is null)
            throw new InvalidOperationException("The book couldn't find.");
            
            GetBookByIdModel model = new GetBookByIdModel();
            model.Title = book.Title;
            model.Genre = ((GenreEnum)book.GenreId).ToString();
            model.PageCount = book.PageCount;
            model.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy");
            return model;
        }

        public class GetBookByIdModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}