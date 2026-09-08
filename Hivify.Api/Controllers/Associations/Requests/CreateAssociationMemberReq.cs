using Association.Domain.Members;
using FluentValidation;
namespace Hivify.Api.Controllers.Associations.Requests
{
    public sealed record CreateAssociationMemberReq(
     Guid UserId,
     string FullName,
    string Email,
    MemberRole Role
     );


    public sealed class CreateAssociationMemberReqValidator : AbstractValidator<CreateAssociationMemberReq>
    {
        public CreateAssociationMemberReqValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}
