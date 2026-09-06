using FluentValidation;

namespace Complaints.Application.Commands.CreateComplaint;

public sealed class CreateComplaintCommandValidator : AbstractValidator<CreateComplaintCommand>
{
    public CreateComplaintCommandValidator()
    {

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel är obligatorisk.")
            .MaximumLength(200).WithMessage("Titeln får vara max 200 tecken.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Beskrivning är obligatorisk.")
            .MaximumLength(2000).WithMessage("Beskrivningen får vara max 2000 tecken.");

        RuleFor(x => x.ImageUrl)
            .Must(url => string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Ogiltig bild-URL.");
    }
}