using Association.Application.Contracts;
using Association.Domain.Associations;
using Microsoft.EntityFrameworkCore;

namespace Association.Infrastructure.Persistence;

public sealed class AssociationRepo : IAssociationRepo
{
    private readonly AssociationDbContext _dbContext;

    public AssociationRepo(AssociationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AssociationEntity?> GetByIdAsync(
        AssociationID associationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Associations
            .Include(a => a.StaffMembers)
            .FirstOrDefaultAsync(
                a => a.Id == associationId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Associations
            .Include(a => a.StaffMembers)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        AssociationEntity AssociationEntity,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Associations.AddAsync(
            AssociationEntity,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}




