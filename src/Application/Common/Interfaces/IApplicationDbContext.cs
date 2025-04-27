using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Log> Logs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
