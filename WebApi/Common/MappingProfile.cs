using AutoMapper;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookById.GetBookByIdQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,GetBookByIdModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src =>((GenreEnum) src.GenreId).ToString()));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src =>((GenreEnum) src.GenreId).ToString()));
        }
    }
}