using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messeging;
using Complaints.Application.Contracts;
using Complaints.Domain;
using FluentValidation;
using SharedKernel.ValuesObjects;
using UserMgmt.Application.Contracts;

namespace Complaints.Application.Commands.CreateComplaint;

public sealed class CreateComplaintCommandHandler : ICommandHandler<CreateComplaintCommand, Guid>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IValidator<CreateComplaintCommand> _validator;

    public CreateComplaintCommandHandler(
        IComplaintRepo complaintRepository,
        IUserDirectory userRepository,
        ICurrentUser currentUser,
        IValidator<CreateComplaintCommand> validator)
    {
        _complaintRepository = complaintRepository;
        _currentUser = currentUser;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateComplaintCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        if (!_currentUser.IsAuthenticated || _currentUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = _currentUser.UserId;

        var complaint = Complaint.Create(
            new UserID(userId),
            new AssociationID(command.AssociationId),
            new Title(command.Title),
            new Description(command.Description),
            command.ImageUrl);

        await _complaintRepository.CreateComplaintAsync(complaint, cancellationToken);

        return complaint.Id.Value;
    }
}