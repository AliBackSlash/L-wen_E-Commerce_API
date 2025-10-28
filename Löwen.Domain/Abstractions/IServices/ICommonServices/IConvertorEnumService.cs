namespace Löwen.Infrastructure.Services.EntityServices;

public interface IConvertorEnumService
{
    string TranslateOrderStatusToArabic(OrderStatus status);
}
