global using Löwen.Domain.ErrorHandleClasses;
global using FluentValidation;
global using Löwen.Application.Behaviors;
global using Löwen.Domain.ConfigurationClasses.ApiSettings;
global using Löwen.Domain.ConfigurationClasses.JWT;
global using Löwen.Domain.ConfigurationClasses.Pagination;
global using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
global using Löwen.Infrastructure.EFCore.Context;
global using Löwen.Infrastructure.EFCore.IdentityUser;
global using Löwen.Infrastructure.Services.EmailServices;
global using Löwen.Infrastructure.Services.IdentityServices;
global using Löwen.Presentation.API.Services;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;


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
global using Löwen.Domain.Enums;
global using Löwen.Presentation.Api.Controllers.v1.RootAdminController.Models;
global using Löwen.Presentation.API.Extensions;


//admin

global using Löwen.Application.Features.AdminFeature.Commands.Category.AddCategory;
global using Löwen.Application.Features.AdminFeature.Commands.Category.RemoveCategory;
global using Löwen.Application.Features.AdminFeature.Commands.Category.UpdateCategory;