using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}