using FluentValidation;
using Löwen.Application.Abstractions.IServices.IdentityServices;
using Löwen.Application.Behaviors;
using Löwen.Domain.Abstractions.IServices;
using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.JWT;
using Löwen.Domain.ConfigurationClasses.Pagination;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Infrastructure.EFCore.Context;
using Löwen.Infrastructure.EFCore.IdentityUser;
using Löwen.Infrastructure.Services.EmailServices;
using Löwen.Infrastructure.Services.IdentityServices;
using Löwen.Presentation.API.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
      cfg.RegisterServicesFromAssembly(typeof(IAppUserService).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(IAppUserService).Assembly);

// Pipeline Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));




// services.AddScoped(typeof(IBasRepository<,>) , typeof(BasRepository<,>));
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<StaticFilesSettings>(
builder.Configuration.GetSection("StaticFilesSettings"));

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

builder.Services.Configure<PaginationSettings>(builder.Configuration.GetSection("PaginationSettings"));
builder.Services.AddScoped<IFileService, FileService>();

#region Configer JWT Bearer
var JWTValues = builder.Configuration.GetSection("JWT").Get<JWT>();
builder.Services.AddSingleton(JWTValues);
builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(ops =>
{
    ops.RequireHttpsMetadata = false;
    ops.SaveToken = false;
    ops.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidIssuer = JWTValues!.Issuer,
        ValidateAudience = true,
        ValidAudience = JWTValues!.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTValues!.SigningKey))
    };
});

#endregion

#region API versioning
builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    option.ApiVersionReader = new MediaTypeApiVersionReader("v");
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));

    var scop = app.Services.CreateAsyncScope();
    using var dbcontext = scop.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
