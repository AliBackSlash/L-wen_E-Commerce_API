namespace Löwen.Domain.Enums;

public enum PaymentMethod
{
    CashOnDelivery = 0,
    CreditCard,
    DebitCard,
    PayPal,
    Stripe,
    BankTransfer,
    MobileWallet,
    Fawry,
    InstaPay, 
    Other = 99
}
