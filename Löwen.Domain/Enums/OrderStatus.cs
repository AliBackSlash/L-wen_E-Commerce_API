namespace Löwen.Domain.Enums;

/// <summary>
/// Represents the different statuses of an order throughout its lifecycle.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order has been placed and is awaiting processing (e.g., payment confirmation).
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Order is currently being prepared (e.g., picking and packing).
    /// </summary>
    Processing,

    /// <summary>
    /// Order has been shipped to the customer.
    /// </summary>
    Shipped,

    /// <summary>
    /// Order has been successfully delivered to the customer.
    /// </summary>
    Delivered,

    /// <summary>
    /// Order has been cancelled by either the customer or admin.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Customer returned the order after delivery.
    /// </summary>
    Returned,

    /// <summary>
    /// Customer has been refunded (usually after a return or cancellation).
    /// </summary>
    Refunded,

    /// <summary>
    /// Order is temporarily on hold (e.g., awaiting stock, payment issue).
    /// </summary>
    OnHold,

    /// <summary>
    /// Order processing failed (e.g., payment error, system issue).
    /// </summary>
    Failed
}
