using SharedKernel;

namespace Complaints.Domain
{
    public readonly record struct AssociationID(Guid Value) : IValue
    {
    }
}
