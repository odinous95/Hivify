using Complaints.Application.Commands.UpdateComplaintStatus;
using FluentValidation;

namespace Complaints.Application.Validations;

public sealed class UpdateComplaintStatusCommandValidator
    : AbstractValidator<UpdateComplaintStatusCommand>
{
    public UpdateComplaintStatusCommandValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Ogiltig status.");
    }
}