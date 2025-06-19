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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers(); 

// =====================================================================
// Employee Bounded Context Registrations
// =====================================================================
// Inventory
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemBookingRepository, ItemBookingRepository>();
builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();

builder.Services.AddScoped<ItemCommandService>();
builder.Services.AddScoped<ItemQueryService>();

// Employees
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

builder.Services.AddScoped<EmployeeCommandService>(); 
builder.Services.AddScoped<EmployeeQueryService>();   

// =====================================================================
// Member Bounded Context Registrations
// =====================================================================
// Members
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>(); // This looks like a domain service, not app service. Make sure it's used correctly.

builder.Services.AddScoped<MemberCommandService>(); 
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

builder.Services.AddScoped<MembershipPaymentCommandService>();
builder.Services.AddScoped<MembershipPaymentQueryService>();
builder.Services.AddScoped<SupplyPurchaseCommandService>();
builder.Services.AddScoped<SupplyPurchaseQueryService>();
builder.Services.AddScoped<PurchaseDetailQueryService>();
builder.Services.AddScoped<PurchaseDetailCommandService>();
builder.Services.AddScoped<SalaryPaymentCommandService>();
builder.Services.AddScoped<SalaryPaymentQueryService>();
    
builder.Services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
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
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); 
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();