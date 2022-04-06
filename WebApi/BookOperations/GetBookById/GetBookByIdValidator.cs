using FluentValidation;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}