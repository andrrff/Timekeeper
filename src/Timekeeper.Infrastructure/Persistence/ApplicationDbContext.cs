using MediatR;
using Timekeeper.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Timekeeper.Infrastructure.Identity;
using Timekeeper.Application.Common.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Timekeeper.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace Timekeeper.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(DbContextOptions options,
                                IOptions<OperationalStoreOptions> operationalStoreOptions,
                                IMediator mediator,
                                AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
                                : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Timesheet> Timesheets => Set<Timesheet>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this, cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }
}