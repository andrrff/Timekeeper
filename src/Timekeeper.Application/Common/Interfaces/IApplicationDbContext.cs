using Timekeeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Timekeeper.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Activity> Activities { get; }

    DbSet<Timesheet> Timesheets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}