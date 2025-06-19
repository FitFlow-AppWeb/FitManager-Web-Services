using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Members Context
    public DbSet<Member> Members { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<MembershipStatus> MembershipStatuses { get; set; }
    // Employees Context
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    // Classes Context
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassMember> ClassMembers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    // Inventory Context
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemBooking> ItemBookings { get; set; }
    // Fincances Context
    public DbSet<SupplyPurchase> SupplyPurchases { get; set; }
    public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    public DbSet<SalaryPayment> SalaryPayments { get; set; }
    public DbSet<MembershipPayment> MembershipPayments { get; set; }
    // Notifications Context
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<EmployeeNotification> EmployeeNotifications { get; set; }
    public DbSet<MemberNotification> MemberNotifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Members Context
        
        // member
        builder.Entity<Member>().HasKey(m => m.Id);

        builder.Entity<Member>().Property(m => m.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Member>().Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Member>().Property(m => m.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Member>().Property(m => m.Age)
            .IsRequired();

        builder.Entity<Member>().Property(m => m.Dni)
            .IsRequired();

        builder.Entity<Member>().HasIndex(m => m.Dni)
            .IsUnique();

        builder.Entity<Member>().Property(m => m.PhoneNumber)
            .IsRequired();

        builder.Entity<Member>().Property(m => m.Address)
            .HasMaxLength(200);

        builder.Entity<Member>().Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Entity<Member>().HasIndex(m => m.Email)
            .IsUnique();
        
        // membership type
        builder.Entity<MembershipType>().HasKey(mt => mt.Id);

        builder.Entity<MembershipType>().Property(mt => mt.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<MembershipType>().Property(mt => mt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<MembershipType>().Property(mt => mt.Description)
            .HasMaxLength(300);

        builder.Entity<MembershipType>().Property(mt => mt.Price)
            .IsRequired();

        builder.Entity<MembershipType>().Property(mt => mt.Duration)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<MembershipType>().Property(mt => mt.Benefits)
            .HasMaxLength(500);

        // membership status
        builder.Entity<MembershipStatus>().HasKey(ms => ms.Id);

        builder.Entity<MembershipStatus>().Property(ms => ms.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<MembershipStatus>().Property(ms => ms.StartDate)
            .IsRequired();

        builder.Entity<MembershipStatus>().Property(ms => ms.EndDate)
            .IsRequired();

        builder.Entity<MembershipStatus>().Property(ms => ms.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<MembershipStatus>()
            .HasOne(ms => ms.Member)
            .WithOne(m => m.MembershipStatus)
            .HasForeignKey<MembershipStatus>(ms => ms.MemberId)
            .IsRequired();

        builder.Entity<MembershipStatus>()
            .HasOne(ms => ms.MembershipType)
            .WithMany(mt => mt.MembershipStatuses)
            .HasForeignKey(ms => ms.MembershipTypeId)
            .IsRequired();
        
        // Employees Context
        
        // employee
        builder.Entity<Employee>().HasKey(e => e.Id);

        builder.Entity<Employee>().Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Employee>().Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Employee>().Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Employee>().Property(e => e.Age)
            .IsRequired();

        builder.Entity<Employee>().Property(e => e.Dni)
            .IsRequired();

        builder.Entity<Employee>().HasIndex(e => e.Dni)
            .IsUnique();

        builder.Entity<Employee>().Property(e => e.PhoneNumber)
            .IsRequired();

        builder.Entity<Employee>().Property(e => e.Address)
            .HasMaxLength(200);

        builder.Entity<Employee>().Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Entity<Employee>().HasIndex(e => e.Email)
            .IsUnique();

        builder.Entity<Employee>().Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Employee>().Property(e => e.Wage)
            .IsRequired();

        builder.Entity<Employee>().Property(e => e.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Employee>().Property(e => e.WorkHours)
            .IsRequired();
        
        // specialty
        builder.Entity<Specialty>().HasKey(s => s.Id);

        builder.Entity<Specialty>().Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Specialty>().Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Specialty>().Property(s => s.Description)
            .HasMaxLength(500);

        builder.Entity<Specialty>()
            .HasOne(s => s.Employee)
            .WithMany(e => e.Specialties)
            .HasForeignKey(s => s.EmployeeId)
            .IsRequired();
        
        // certification
        builder.Entity<Certification>().HasKey(c => c.Id);

        builder.Entity<Certification>().Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Certification>().Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Certification>().Property(c => c.Description)
            .HasMaxLength(500);

        builder.Entity<Certification>()
            .HasOne(c => c.Employee)
            .WithMany(e => e.Certifications)
            .HasForeignKey(c => c.EmployeeId)
            .IsRequired();

        // Classes Context
        
        // class
        builder.Entity<Class>().HasKey(c => c.Id);
        
        builder.Entity<Class>().Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Entity<Class>().Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Entity<Class>().Property(c => c.Description)
            .HasMaxLength(500);
        
        builder.Entity<Class>().Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Entity<Class>().Property(c => c.Capacity)
            .IsRequired();
        
        builder.Entity<Class>().Property(c => c.StartDate)
            .IsRequired();
        
        builder.Entity<Class>().Property(c => c.Duration)
            .IsRequired();
        
        builder.Entity<Class>().Property(c => c.Status)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Entity<Class>()
            .HasOne(c => c.Employee)
            .WithMany(e => e.Classes)
            .HasForeignKey(c => c.EmployeeId)
            .IsRequired();
        
        // class member
        builder.Entity<ClassMember>().HasKey(cm => new { cm.MemberId, cm.ClassId });

        builder.Entity<ClassMember>()
            .HasOne(cm => cm.Member)
            .WithMany(m => m.ClassMembers)
            .HasForeignKey(cm => cm.MemberId);

        builder.Entity<ClassMember>()
            .HasOne(cm => cm.Class)
            .WithMany(c => c.ClassMembers)
            .HasForeignKey(cm => cm.ClassId);
        
        // booking
        builder.Entity<Booking>().HasKey(b => b.Id);

        builder.Entity<Booking>().Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Booking>().Property(b => b.Date)
            .IsRequired();

        builder.Entity<Booking>()
            .HasOne(b => b.Member)
            .WithMany(m => m.Bookings)
            .HasForeignKey(b => b.MemberId);

        builder.Entity<Booking>()
            .HasOne(b => b.Class)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.ClassId);
        
        // attendance
        builder.Entity<Attendance>().HasKey(a => a.Id);

        builder.Entity<Attendance>().Property(a => a.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Attendance>().Property(a => a.EntryTime)
            .IsRequired();

        builder.Entity<Attendance>().Property(a => a.ExitTime)
            .IsRequired();

        
        builder.Entity<Attendance>()
            .HasOne(a => a.Member)
            .WithMany(m => m.Attendances)
            .HasForeignKey(a => a.MemberId);

        builder.Entity<Attendance>()
            .HasOne(a => a.Class)
            .WithMany(c => c.Attendances)
            .HasForeignKey(a => a.ClassId);
        
        // Inventory Context
        
        // item type
        builder.Entity<ItemType>().HasKey(it => it.Id);

        builder.Entity<ItemType>().Property(it => it.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<ItemType>().Property(it => it.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<ItemType>().Property(it => it.Description)
            .HasMaxLength(300);
        
        // item
        builder.Entity<Item>().HasKey(i => i.Id);

        builder.Entity<Item>().Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Item>().Property(i => i.LastMaintenanceDate)
            .IsRequired();

        builder.Entity<Item>().Property(i => i.NextMaintenanceDate)
            .IsRequired();

        builder.Entity<Item>().Property(i => i.Status)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Entity<Item>().Property(i => i.EmployeeId)
            .IsRequired();

        builder.Entity<Item>()
            .HasOne(i => i.Employee)
            .WithMany()
            .HasForeignKey(i => i.EmployeeId);

        builder.Entity<Item>().Property(i => i.ItemTypeId)
            .IsRequired();

        builder.Entity<Item>()
            .HasOne(i => i.ItemType)
            .WithMany(it => it.Items)
            .HasForeignKey(i => i.ItemTypeId);

        builder.Entity<Item>()
            .HasMany(i => i.ItemBookings)
            .WithOne(ib => ib.Item)
            .HasForeignKey(ib => ib.ItemId);
        
        // item booking
        builder.Entity<ItemBooking>().HasKey(ib => ib.Id);

        builder.Entity<ItemBooking>().Property(ib => ib.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<ItemBooking>().Property(ib => ib.ReservationDate)
            .IsRequired();

        builder.Entity<ItemBooking>().Property(ib => ib.UsageTime)
            .IsRequired();

        builder.Entity<ItemBooking>()
            .HasOne(ib => ib.Class)
            .WithMany(c => c.ItemBookings)
            .HasForeignKey(ib => ib.ClassId);

        builder.Entity<ItemBooking>()
            .HasOne(ib => ib.Item)
            .WithMany(i => i.ItemBookings)
            .HasForeignKey(ib => ib.ItemId);

        // Finances Context
        
        // supply purchase
        builder.Entity<SupplyPurchase>().HasKey(sp => sp.Id);

        builder.Entity<SupplyPurchase>().Property(sp => sp.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<SupplyPurchase>().Property(sp => sp.Date)
            .IsRequired();

        builder.Entity<SupplyPurchase>().Property(sp => sp.Amount)
            .IsRequired();

        builder.Entity<SupplyPurchase>().Property(sp => sp.Method)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<SupplyPurchase>().Property(sp => sp.Currency)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<SupplyPurchase>().Property(sp => sp.VendorName)
            .IsRequired()
            .HasMaxLength(100);

        // purchase detail
        builder.Entity<PurchaseDetail>().HasKey(pd => pd.Id);

        builder.Entity<PurchaseDetail>()
            .Property(pd => pd.UnitPrice)
            .IsRequired();

        builder.Entity<PurchaseDetail>()
            .Property(pd => pd.Quantity)
            .IsRequired();

        builder.Entity<PurchaseDetail>()
            .HasOne(pd => pd.SupplyPurchase)
            .WithMany(sp => sp.PurchaseDetails)
            .HasForeignKey(pd => pd.SupplyPurchaseId);

        builder.Entity<PurchaseDetail>()
            .HasOne(pd => pd.ItemType)
            .WithMany()
            .HasForeignKey(pd => pd.ItemTypeId);
        
        // salary payment
        builder.Entity<SalaryPayment>().HasKey(sp => sp.Id);

        builder.Entity<SalaryPayment>()
            .Property(sp => sp.Date)
            .IsRequired();

        builder.Entity<SalaryPayment>()
            .Property(sp => sp.Amount)
            .IsRequired();

        builder.Entity<SalaryPayment>()
            .Property(sp => sp.Method)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<SalaryPayment>()
            .Property(sp => sp.Currency)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<SalaryPayment>()
            .HasOne(sp => sp.Employee)
            .WithMany()
            .HasForeignKey(sp => sp.EmployeeId);
        
        // membership payment
        builder.Entity<MembershipPayment>().HasKey(mp => mp.Id);

        builder.Entity<MembershipPayment>()
            .Property(mp => mp.Date)
            .IsRequired();

        builder.Entity<MembershipPayment>()
            .Property(mp => mp.Amount)
            .IsRequired();

        builder.Entity<MembershipPayment>()
            .Property(mp => mp.Method)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<MembershipPayment>()
            .Property(mp => mp.Currency)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<MembershipPayment>()
            .HasOne(mp => mp.Member)
            .WithMany()
            .HasForeignKey(mp => mp.MemberId);
        
        // Notifications Context
        
        // notification
        builder.Entity<Notification>().HasKey(n => n.Id);

        builder.Entity<Notification>().Property(n => n.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Notification>().Property(n => n.CreatedAt)
            .IsRequired();

        builder.Entity<Notification>().Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Entity<Notification>().Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(1000);
        
        // employee notification
        builder.Entity<EmployeeNotification>().HasKey(en => new { en.NotificationId, en.EmployeeId });

        builder.Entity<EmployeeNotification>()
            .HasOne(en => en.Notification)
            .WithMany()
            .HasForeignKey(en => en.NotificationId);

        builder.Entity<EmployeeNotification>()
            .HasOne(en => en.Employee)
            .WithMany()
            .HasForeignKey(en => en.EmployeeId);
        
        // member notification
        builder.Entity<MemberNotification>().HasKey(mn => new { mn.NotificationId, mn.MemberId });

        builder.Entity<MemberNotification>()
            .HasOne(mn => mn.Notification)
            .WithMany()
            .HasForeignKey(mn => mn.NotificationId);

        builder.Entity<MemberNotification>()
            .HasOne(mn => mn.Member)
            .WithMany()
            .HasForeignKey(mn => mn.MemberId);
    }
}