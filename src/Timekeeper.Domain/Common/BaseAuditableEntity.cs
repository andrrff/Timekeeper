using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    /// <summary>
    /// Data de criação
    /// </summary>
    /// <value></value>
    public DateTime Created { get; set; }

    /// <summary>
    /// Criado por
    /// </summary>
    /// <value></value>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Data da última modificação
    /// </summary>
    /// <value></value>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Última modificação por
    /// </summary>
    /// <value></value>
    public string? LastModifiedBy { get; set; }
}