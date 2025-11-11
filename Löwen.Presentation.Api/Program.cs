using Löwen.Application.Common.Caching;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Infrastructure.Caching;
using Löwen.Infrastructure.Services.EntityServices;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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
      cfg.RegisterServicesFromAssembly(typeof(EntryPoint).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(EntryPoint).Assembly);

// Pipeline Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));




// services.AddScoped(typeof(IBasRepository<,>) , typeof(BasRepository<,>));
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<ILoveProductUserService, LoveProductUserService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductImges, ProductImges>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IConvertorEnumService, ConvertorEnumService>();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

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
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = JWTValues!.Issuer,
        ValidateAudience = true,
        ValidAudience = JWTValues!.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTValues!.SigningKey))
    };
});

#endregion
builder.Services.AddMemoryCache();

#region API versioning
builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    option.ApiVersionReader = new MediaTypeApiVersionReader("v");
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // v1, v1.1, ...
    options.SubstituteApiVersionInUrl = true;
});

#endregion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // XML Documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);

    // JWT Security
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter your Bearer token in the format **Bearer {token}**",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//builder.Services.AddSwaggerWithApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); // رابط الـ JSON
        c.RoutePrefix = string.Empty;
    });

    using var scope = app.Services.CreateScope();
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.Migrate();
}

else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
