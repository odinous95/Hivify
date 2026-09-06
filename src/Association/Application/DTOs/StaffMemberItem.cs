using Association.Domain.Members;

namespace Association.Application.DTOs
{
    public sealed record StaffMemberItem(
        Guid Id,
        string FullName,
        string Email,
        MemberRole Role
    );
}



