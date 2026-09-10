using Association.Domain.Associations;
using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;
namespace Association.Domain.Members;

public class Member : BaseEntity<MemberID>
{
    public AssociationID AssociationId { get; private set; }
    public UserID UserId { get; private set; }

    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    public MemberRole Role { get; private set; }

    public DateTime? DeletedAt { get; private set; }


    private Member() { }


    private Member(MemberID id, AssociationID associationId, UserID userId, Name fullName, Email email, MemberRole role) : base(id)
    {
        AssociationId = associationId;
        UserId = userId;
        FullName = fullName;
        Email = email;
        Role = role;
    }


    internal static Member Create(AssociationID associationId, UserID userId, Name fullName, Email email, MemberRole role)
    {
        return new Member(
            new MemberID(Guid.NewGuid()),
            associationId,
            userId,
            fullName,
            email,
            role);
    }
    internal void ChangeRole(MemberRole role)
    {
        if (DeletedAt != null)
        {
            throw new DomainException(
                "Styrelsemedlemmen är borttagen.");
        }

        Role = role;
    }


    internal void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }
}




