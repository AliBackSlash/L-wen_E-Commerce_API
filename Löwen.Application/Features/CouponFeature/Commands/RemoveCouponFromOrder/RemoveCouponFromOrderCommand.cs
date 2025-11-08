using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Cart;
using Löwen.Domain.Layer_Dtos.Order;

namespace Löwen.Application.Features.CouponFeature.Commands.RemoveCouponFromOrder;
public record RemoveCouponFromOrderCommand(string CouponCode, string OrderId) : ICommand;
