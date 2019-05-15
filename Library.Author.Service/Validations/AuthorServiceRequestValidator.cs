using FluentValidation;
using Library.Author.Service.Requests;
using Library.Author.Service.ServiceModels;

namespace Library.Author.Service.Validations
{
    public class AuthorServiceRequestValidator : AbstractValidator<AuthorServiceRequest>
    {
        public AuthorServiceRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Author).SetValidator(new AuthorModelValidator());
        }
    }

    public class AuthorModelValidator : AbstractValidator<AuthorModel>
    {
        public AuthorModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Data).SetValidator(new AuthorModelMetaDataValidator());
        }
    }

    public class AuthorModelMetaDataValidator : AbstractValidator<AuthorModelMetaData>
    {
    }
}