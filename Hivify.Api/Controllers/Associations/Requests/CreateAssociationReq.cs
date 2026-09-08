using FluentValidation;
namespace Hivify.Api.Controllers.Associations.Requests
{
    public sealed record CreateAssociationReq(
    string Name);



    public sealed class CreateAssociationReqValidator : AbstractValidator<CreateAssociationReq>
    {
        public CreateAssociationReqValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

}

