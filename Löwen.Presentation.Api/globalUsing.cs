global using FluentValidation;
global using Löwen.Application.Behaviors;

//admin
global using Löwen.Application.Features.AdminFeature.Commands.Category.AddCategory;
global using Löwen.Application.Features.AdminFeature.Commands.Category.RemoveCategory;
global using Löwen.Application.Features.AdminFeature.Commands.Category.UpdateCategory;
global using Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;
global using Löwen.Application.Features.AdminFeature.Commands.Product.AddProductImages;
global using Löwen.Application.Features.AdminFeature.Commands.Product.AddProductVariant;
global using Löwen.Application.Features.AdminFeature.Commands.Product.DeleteProductImages;
global using Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;
global using Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProductVariant;
global using Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;
global using Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProductVariant;
global using Löwen.Application.Features.AdminFeature.Commands.Tag.UpdateTag;
global using Löwen.Application.Features.AdminFeature.Queries.GetUsers;
global using Löwen.Application.Features.AdminFeatures.Commands.ActivateMarkedAsDeleted;
global using Löwen.Application.Features.AdminFeatures.Commands.MarkAsDeleted;
global using Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;

//root admin
global using Löwen.Application.Features.RootAdminFeatures.Commands.ActivateMarkedAsDeleted;
global using Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;
global using Löwen.Application.Features.RootAdminFeatures.Commands.AssignRole;
global using Löwen.Application.Features.RootAdminFeatures.Commands.MarkAsDeleted;
global using Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;
global using Löwen.Application.Features.RootAdminFeatures.Commands.RemoveRoleFromUser;
global using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;
global using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
global using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdmins;

//Auth
global using Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;
global using Löwen.Application.Features.AuthFeature.Commands.LoginCommand;
global using Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;
global using Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;
global using Löwen.Presentation.API.Controllers.v1.AuthController.Models;
global using Microsoft.AspNetCore.Authorization;

//Cart
global using Löwen.Application.Features.CartFeature.Commands.AddToCart;
global using Löwen.Application.Features.CartFeature.Commands.RemoveFromCartItem;
global using Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;
global using Löwen.Application.Features.CartFeature.Queries.GetCartByUser;
global using Löwen.Presentation.Api.Controllers.v1.CartController.Models;

//Coupon
global using Löwen.Application.Features.CouponFeature.Commands.AddCoupon;
global using Löwen.Application.Features.CouponFeature.Commands.ApplyCouponToOrder;
global using Löwen.Application.Features.CouponFeature.Commands.RemoveCoupon;
global using Löwen.Application.Features.CouponFeature.Commands.RemoveCouponFromOrder;
global using Löwen.Application.Features.CouponFeature.Commands.UpdateCoupon;
global using Löwen.Application.Features.CouponFeature.Queries;
global using Löwen.Application.Features.CouponFeature.Queries.GetAllCoupons;
global using Löwen.Application.Features.CouponFeature.Queries.GetCouponByCode;
global using Löwen.Application.Features.CouponFeature.Queries.GetCouponById;
global using Löwen.Presentation.Api.Controllers.v1.CouponController.Models;

//Users
global using Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;
global using Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;
global using Löwen.Application.Features.UserFeature.Commands.Love.AddLove;
global using Löwen.Application.Features.UserFeature.Commands.Love.RemoveLove;
global using Löwen.Application.Features.UserFeature.Commands.UserInfoOper.ChangePassword;
global using Löwen.Application.Features.UserFeature.Commands.UserInfoOper.UpdateUserInfo;
global using Löwen.Application.Features.UserFeature.Commands.WishlistOper.AddToWishlist;
global using Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveFromWishlist;
global using Löwen.Application.Features.UserFeature.Queries.GetUserByEmail;
global using Löwen.Application.Features.UserFeature.Queries.GetUserById;
global using Löwen.Domain.ConfigurationClasses.ApiSettings;
global using Löwen.Domain.ConfigurationClasses.JWT;
global using Löwen.Domain.ConfigurationClasses.Pagination;
global using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
global using Löwen.Domain.Enums;
global using Löwen.Domain.ErrorHandleClasses;
global using Löwen.Domain.Layer_Dtos.Delivery;
global using Löwen.Domain.Layer_Dtos.Product;
global using Löwen.Domain.Pagination;
global using Löwen.Infrastructure.EFCore.Context;
global using Löwen.Infrastructure.EFCore.IdentityUser;
global using Löwen.Infrastructure.Services.EmailServices;
global using Löwen.Infrastructure.Services.IdentityServices;
global using Löwen.Presentation.Api.Controllers.v1.AdminController.Models.CategoryModels;
global using Löwen.Presentation.Api.Controllers.v1.AdminController.Models.DeliveryOrder;
global using Löwen.Presentation.Api.Controllers.v1.AdminController.Models.ProductModels;
global using Löwen.Presentation.Api.Controllers.v1.RootAdminController.Models;
global using Löwen.Presentation.Api.Controllers.v1.UsersController.Models;
global using Löwen.Presentation.API.Extensions;
global using Löwen.Presentation.API.Services;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.Security.Claims;
global using System.Text;
