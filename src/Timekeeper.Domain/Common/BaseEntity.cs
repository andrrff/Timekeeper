using Timekeeper.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timekeeper.Domain.Common;

public abstract class BaseEntity : IBaseEntity
{
    /// <summary>
    /// Identificador único
    /// </summary>
    /// <value></value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Eventos de domínio
    /// </summary>
    /// <returns></returns>
    private readonly List<IBaseEvent> _domainEvents = new();

    /// <summary>
    /// Eventos de domínio
    /// </summary>
    /// <returns></returns>
    [NotMapped]
    public IReadOnlyCollection<IBaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddDomainEvent(IBaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IBaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj is BaseEntity other)
        {
            return Id == other.Id;
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}