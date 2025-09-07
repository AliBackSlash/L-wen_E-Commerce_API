namespace Löwen.Domain.Enums;

public enum PaymentStatus
{
    Pending = 0,    
    Processing, 
    Completed,  
    Failed,     
    Cancelled,  
    Refunded,   
    Disputed    
}
