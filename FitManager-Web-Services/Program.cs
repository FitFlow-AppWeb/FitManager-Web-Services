using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using FitManager_Web_Services.Classes.Application.Internal.QueryServices;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Infrastructure.Repositories;
using FitManager_Web_Services.Finances.Application.Internal.CommandServices;
using FitManager_Web_Services.Finances.Application.Internal.QueryServices;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Finances.Infrastructure.Repositories;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Members.Domain.Services;
using FitManager_Web_Services.Members.Infrastructure.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Employees.Infrastructure.Repositories;
using FitManager_Web_Services.Inventory.Application.Internal.CommandServices;
using FitManager_Web_Services.Inventory.Application.Internal.QueryServices;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Inventory.Infrastructure.Repositories;
using FitManager_Web_Services.Notifications.Application.Internal.CommandServices;
using FitManager_Web_Services.Notifications.Application.Internal.QueryServices;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Notifications.Infrastructure.Repositories;
using FitManager_Web_Services.IAM.Infrastructure.Tokens;
using FitManager_Web_Services.IAM.Application.Internal.OutboundServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(x =>
{
    x.DefaultRequestCulture = new RequestCulture("en");
    x.ApplyCurrentCultureToResponseHeaders = true;
    x.SupportedCultures = new List<CultureInfo>
    {
        new("es"),
        new("es-ES"),
        new("en"),
        new("en-US")
    };
    x.SupportedUICultures = new List<CultureInfo>
    {
        new("es"),
        new("es-ES"),
        new("en"),
        new("en-US") 
    };
    
    x.RequestCultureProviders.Clear(); 
    
    x.RequestCultureProviders.Add(new QueryStringRequestCultureProvider
    {
        QueryStringKey = "locale",   
        UIQueryStringKey = "locale"  
    });
    
    x.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
    x.RequestCultureProviders.Add(new CookieRequestCultureProvider()); 
});

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization()
    .AddViewLocalization();

// Register IAM (Authentication) services
builder.Services.Configure<TokenOptions>(
    builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<FitManager_Web_Services.IAM.Application.Internal.CommandServices.UserCommandService>();
builder.Services.AddScoped<FitManager_Web_Services.IAM.Application.Internal.QueryServices.UserQueryService>();
builder.Services.AddScoped<FitManager_Web_Services.IAM.Domain.Repositories.IUserRepository, FitManager_Web_Services.IAM.Infrastructure.Repositories.UserRepository>();

// Register JWT Authentication (basado solo en Secret)
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FitManager API",
        Version = "v1",
        Description = "API for Gym Member Management"
    });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token like this: **Bearer &lt;token&gt;**"
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
            Array.Empty<string>()
        }
    });
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


// =====================================================================
// Employee Bounded Context Registrations
// =====================================================================
// Inventory
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemBookingRepository, ItemBookingRepository>();
builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();

builder.Services.AddScoped<ItemCommandService>();
builder.Services.AddScoped<ItemQueryService>();

builder.Services.AddScoped<ItemTypeCommandService>();
builder.Services.AddScoped<ItemTypeQueryService>();

// Employees
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

builder.Services.AddScoped<EmployeeCommandService>(); 
builder.Services.AddScoped<ICertificationCommandService, CertificationCommandService>();
builder.Services.AddScoped<ISpecialtyCommandService, SpecialtyCommandService>();
builder.Services.AddScoped<EmployeeQueryService>();  
builder.Services.AddScoped<ICertificationQueryService, CertificationQueryService>();
builder.Services.AddScoped<ISpecialtyQueryService, SpecialtyQueryService>();

// =====================================================================
// Member Bounded Context Registrations
// =====================================================================
// Members
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>(); 

builder.Services.AddScoped<MemberCommandService>();
builder.Services.AddScoped<IMembershipTypeCommandService, MembershipTypeCommandService>(); 
builder.Services.AddScoped<MemberQueryService>();   

// Classes
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IClassMemberRepository, ClassMemberRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<AttendanceCommandService>();
builder.Services.AddScoped<AttendanceQueryService>();
builder.Services.AddScoped<ClassCommandService>();
builder.Services.AddScoped<ClassQueryService>();

// Finances
// =====================================================================
// Finances Bounded Context Registrations
// =====================================================================
builder.Services.AddScoped<IMembershipPaymentRepository, MembershipPaymentRepository>();
builder.Services.AddScoped<ISupplyPurchaseRepository, SupplyPurchaseRepository>();
builder.Services.AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>();
builder.Services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
builder.Services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();

builder.Services.AddScoped<MembershipPaymentCommandService>();
builder.Services.AddScoped<MembershipPaymentQueryService>();
builder.Services.AddScoped<SupplyPurchaseCommandService>();
builder.Services.AddScoped<SupplyPurchaseQueryService>();
builder.Services.AddScoped<PurchaseDetailQueryService>();
builder.Services.AddScoped<PurchaseDetailCommandService>();
builder.Services.AddScoped<SalaryPaymentCommandService>();
builder.Services.AddScoped<SalaryPaymentQueryService>();
builder.Services.AddScoped<MembershipTypeQueryService>();

// =====================================================================
// >>>>>>>>>>>>>>>>> Notifications Bounded Context Registrations <<<<<<<<<<<<<<<<<
// =====================================================================

// Repositories
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IMemberNotificationRepository, MemberNotificationRepository>();
builder.Services.AddScoped<IEmployeeNotificationRepository, EmployeeNotificationRepository>();

// Application Services (Command and Query Handlers)
builder.Services.AddScoped<IMemberNotificationCommandService, MemberNotificationCommandService>();
builder.Services.AddScoped<IMemberNotificationQueryService, MemberNotificationQueryService>();
builder.Services.AddScoped<IEmployeeNotificationCommandService, EmployeeNotificationCommandService>();
builder.Services.AddScoped<IEmployeeNotificationQueryService, EmployeeNotificationQueryService>();

// =====================================================================
// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseRequestLocalization();

app.UseCors("AllowFrontendLocalhost");
app.UseRouting();
app.UseCors("AnyOrigin");
app.UseSwagger();
app.UseSwaggerUI();

// Autenticación y autorización
app.UseAuthentication(); 
app.UseAuthorization();

//app.UseMiddleware<FitManager_Web_Services.IAM.Infrastructure.Pipeline.Middleware.Components.RequestAuthorizationMiddleware>();

app.MapControllers();

app.Run();