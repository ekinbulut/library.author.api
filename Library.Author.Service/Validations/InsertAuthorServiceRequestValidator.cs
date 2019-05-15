using FluentValidation;
using Library.Author.Service.Requests;

namespace Library.Author.Service.Validations
{
    public class InsertAuthorServiceRequestValidator : AbstractValidator<InsertAuthorServiceRequest>
    {
        public InsertAuthorServiceRequestValidator()
        {
            RuleFor(p => p.Author).SetValidator(new AuthorModelValidator());
        }
    }
}