namespace Löwen.Domain.Enums;

/// <summary>
/// Represents the different types of messages or notifications
/// that can be sent to customers or administrators.
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Confirmation message after an order is created.
    /// </summary>
    OrderConfirmation = 0,

    /// <summary>
    /// Message to confirm email address after registration.
    /// </summary>
    EmailConfirmation,

    /// <summary>
    /// Message to confirm password change.
    /// </summary>
    ChangePassword,

    /// <summary>
    /// Update notification about the shipping process.
    /// </summary>
    ShippingUpdate,

    /// <summary>
    /// Notification that the order has been delivered.
    /// </summary>
    DeliveryNotification,

    /// <summary>
    /// Reminder to complete a pending payment.
    /// </summary>
    PaymentReminder,

    /// <summary>
    /// Promotional message about offers, discounts, or coupons.
    /// </summary>
    Promotion,

    /// <summary>
    /// Security or account-related alerts (e.g., suspicious login attempt).
    /// </summary>
    AccountAlert,

    /// <summary>
    /// Response from customer support team to a customer inquiry.
    /// </summary>
    SupportResponse
}
