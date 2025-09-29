using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using Löwen.Infrastructure.Services.EntityServices;

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
builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<ILoveProductUserService, LoveProductUserService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

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
