namespace Löwen.Domain.Enums;

/// <summary>
/// Represents the type of action performed in the system.
/// Useful for auditing and logging purposes.
/// </summary>
public enum ActionType
{
    /// <summary>
    /// A new record was created (e.g., new product, new order).
    /// </summary>
    Create = 0,

    /// <summary>
    /// An existing record was updated (e.g., update address, change order status).
    /// </summary>
    Update,

    /// <summary>
    /// A record was deleted (e.g., remove product, cancel user account).
    /// </summary>
    Delete,
}
