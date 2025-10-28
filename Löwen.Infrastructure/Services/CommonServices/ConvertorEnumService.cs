using Löwen.Domain.Enums;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ConvertorEnumService : IConvertorEnumService
{
    public string TranslateOrderStatusToArabic(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Pending => "قيد الانتظار",
            OrderStatus.Processing => "قيد التجهيز",
            OrderStatus.Shipped => "تم نقل طلبك إلى مندوب الشحن",
            OrderStatus.Delivered => "تم التسليم",
            OrderStatus.Cancelled => "ملغى",
            OrderStatus.Returned => "تم الإرجاع",
            OrderStatus.Refunded => "تم استرداد المبلغ",
            OrderStatus.OnHold => "معلق",
            OrderStatus.Failed => "فشل",
            _ => "غير معروف"
        };
    }
}
