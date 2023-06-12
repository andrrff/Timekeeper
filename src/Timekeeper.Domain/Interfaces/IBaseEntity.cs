namespace Timekeeper.Domain.Interfaces;

public interface IBaseEntity
{
    /// <summary>
    /// Identificador único
    /// </summary>
    /// <value></value>
    Guid Id { get; }

    /// <summary>
    /// Eventos de domínio
    /// </summary>
    /// <value></value>
    IReadOnlyCollection<IBaseEvent> DomainEvents { get; }

    void AddDomainEvent(IBaseEvent domainEvent);

    void RemoveDomainEvent(IBaseEvent domainEvent);

    void ClearDomainEvents();

    bool Equals(object? obj);

    int GetHashCode();
}