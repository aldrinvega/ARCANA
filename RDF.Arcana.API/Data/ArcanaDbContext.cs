using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Data;

public class ArcanaDbContext : DbContext
{
    public ArcanaDbContext(DbContextOptions<ArcanaDbContext> options) : base(options) { }
    public virtual DbSet<OwnersAddress> Address { get; set; }
    public virtual DbSet<BusinessAddress> BusinessAddress { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Items> Items { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<MeatType> MeatTypes { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    public virtual DbSet<Uom> Uoms { get; set; }
    public virtual DbSet<UserRoles> UserRoles { get; set; }
    public virtual DbSet<Discount> Discounts { get; set; }
    public virtual DbSet<TermDays> TermDays { get; set; }
    public virtual DbSet<Clients> Clients { get; set; }
    public virtual DbSet<ClientDocuments> ClientDocuments { get; set; }
    public virtual DbSet<FixedDiscounts> FixedDiscounts { get; set; }
    public virtual DbSet<VariableDiscounts> VariableDiscounts { get; set; }
    public virtual DbSet<FreebieItems> FreebieItems { get; set; }
    public virtual DbSet<FreebieRequest> FreebieRequests { get; set; }
    public virtual DbSet<StoreType> StoreTypes { get; set; }
    public virtual DbSet<BookingCoverages> BookingCoverages { get; set; }
    public virtual DbSet<ModeOfPayment> ModeOfPayments { get; set; }
    public virtual DbSet<TermOptions> TermOptions { get; set; }
    public virtual DbSet<Terms> Terms { get; set; }
    public virtual DbSet<ListingFee> ListingFees { get; set; }
    public virtual DbSet<ListingFeeItems> ListingFeeItems { get; set; }
    public virtual DbSet<UpdateRequestTrail> UpdateRequestTrails { get; set; }
    public virtual DbSet<ClearedPayments> ClearedPayments { get; set; }

    //Approver 

    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<Approval> Approval { get; set; }
    public virtual DbSet<Approver> Approvers { get; set; }
    public virtual DbSet<RequestApprovers> RequestApprovers {get; set;}
    public virtual DbSet<ItemPriceChange> ItemPriceChanges { get; set; }
    public virtual DbSet<ClientModeOfPayment> ClientModeOfPayments { get; set; }
    public virtual DbSet<Cluster> Clusters { get; set; }
    public virtual DbSet<OtherExpenses> OtherExpenses { get; set; }
    public virtual DbSet<ExpensesRequest> ExpensesRequests { get; set; }
    public virtual DbSet<Expenses> Expenses { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<CdoCluster> CdoClusters { get; set; }
    public virtual DbSet<PriceMode> PriceMode { get; set; }
    public virtual DbSet<PriceModeItems> PriceModeItems { get; set; }
    public virtual DbSet<SpecialDiscount> SpecialDiscounts { get; set; }
    public virtual DbSet<Freezer> Freezers { get; set; }
    public virtual DbSet<Transactions> Transactions { get; set; }
    public virtual DbSet<TransactionItems> TransactionItems { get; set; }
    public virtual DbSet<TransactionSales> TransactionSales { get; set; }
    public virtual DbSet<AdvancePayment> AdvancePayments { get; set; }
    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public virtual DbSet<PaymentRecords> PaymentRecords { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoles>()
            .Property(e => e.Permissions)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
                new ValueComparer<ICollection<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        //Unique Constraints

        modelBuilder.Entity<UserRoles>()
               .HasIndex(u => u.UserRoleName)
               .IsUnique();

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.AddedByUser)
            .WithMany(x => x.Clients)
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<Company>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Company>(x => x.AddedBy);

        modelBuilder.Entity<Department>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Department>(u => u.AddedBy);
        
        modelBuilder.Entity<OtherExpenses>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<OtherExpenses>(u => u.AddedBy);

        modelBuilder.Entity<Discount>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Discount>(u => u.AddedBy);

        modelBuilder.Entity<Items>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Items>(u => u.AddedBy);

        modelBuilder.Entity<Location>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Location>(u => u.AddedBy);

        modelBuilder.Entity<MeatType>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<MeatType>(u => u.AddedBy);

        modelBuilder.Entity<ProductSubCategory>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<ProductSubCategory>(u => u.AddedBy);
        
        modelBuilder.Entity<OtherExpenses>()
            .HasOne(u => u.AddedByUser)
            .WithMany()
            .HasForeignKey(u => u.AddedBy);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<ProductCategory>(u => u.AddedBy);

        modelBuilder.Entity<TermDays>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<TermDays>(u => u.AddedBy);

        modelBuilder.Entity<Uom>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Uom>(u => u.AddedBy);

        modelBuilder.Entity<User>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<User>(u => u.AddedBy);

        modelBuilder.Entity<UserRoles>()
            .HasOne(u => u.AddedByUser)
            .WithMany()
            .HasForeignKey(u => u.AddedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<Clients>()
            .HasMany(c => c.ClientDocuments)
            .WithOne(cd => cd.Clients)
            .HasForeignKey(cd => cd.ClientId);

        /*modelBuilder.Entity<Clients>()
            .HasMany(c => c.Approvals)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId);

        modelBuilder.Entity<FreebieRequest>()
            .HasOne(fr => fr.Approvals)
            .WithMany(a => a.FreebieRequest)
            .OnDelete(DeleteBehavior.Restrict);*/

        modelBuilder.Entity<StoreType>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<StoreType>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.StoreType)
            .WithMany()
            .HasForeignKey(x => x.StoreTypeId);

        modelBuilder.Entity<BookingCoverages>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.BookingCoverages)
            .WithMany()
            .HasForeignKey(x => x.BookingCoverageId);

        modelBuilder.Entity<ModeOfPayment>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.Term)
            .WithMany()
            .HasForeignKey(x => x.Terms);

        modelBuilder.Entity<Terms>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<TermOptions>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.FixedDiscounts)
            .WithMany()
            .HasForeignKey(x => x.FixedDiscountId);

        /*modelBuilder.Entity<FixedDiscounts>()
            .HasOne(x => x.Clients)
            .WithMany()
            .HasForeignKey(x => x.ClientId);*/

        modelBuilder.Entity<FreebieRequest>()
            .HasOne(x => x.RequestedByUser)
            .WithMany(x => x.FreebieRequests)
            .HasForeignKey(x => x.RequestedBy);

        /*modelBuilder.Entity<Approvals>()
            .HasOne(x => x.ApproveByUser)
            .WithMany()
            .HasForeignKey(x => x.ApprovedBy);

        modelBuilder.Entity<Approvals>()
            .HasOne(x => x.RequestedByUser)
            .WithMany()
            .HasForeignKey(x => x.RequestedBy);*/
        
        modelBuilder.Entity<Expenses>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);
        
        modelBuilder.Entity<Expenses>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<ListingFee>()
            .HasOne(x => x.RequestedByUser)
            .WithMany(x => x.ListingFees)
            .HasForeignKey(x => x.RequestedBy);
        
        modelBuilder.Entity<Approval>()
            .HasOne(x => x.Approver)
            .WithMany(x => x.Approvals)
            .HasForeignKey(x => x.ApproverId);
        
        modelBuilder.Entity<Request>()
            .HasOne(x => x.Requestor)
            .WithMany(x => x.RequesterRequests)
            .HasForeignKey(x => x.RequestorId);
        
        modelBuilder.Entity<Request>()
            .HasOne(x => x.CurrentApprover)
            .WithMany(x => x.ApproverRequests)
            .HasForeignKey(x => x.CurrentApproverId);

        modelBuilder.Entity<UpdateRequestTrail>()
            .HasOne(x => x.Request)
            .WithMany(x => x.UpdateRequestTrails)
            .HasForeignKey(x => x.RequestId);

        modelBuilder.Entity<SpecialDiscount>()
            .Property(d => d.Discount)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<ListingFee>()
            .Property(p => p.Total)
            .HasColumnType("decimal(8,2)");
        
        modelBuilder.Entity<FixedDiscounts>()
            .Property(p => p.DiscountPercentage)
            .HasColumnType("decimal(8,2)");
        
        modelBuilder.Entity<VariableDiscounts>()
            .Property(p => p.MinimumAmount)
            .HasColumnType("decimal(18,6)");

        modelBuilder.Entity<VariableDiscounts>()
            .Property(p => p.MaximumAmount)
            .HasColumnType("decimal(18,6)");
        
        modelBuilder.Entity<VariableDiscounts>()
            .Property(p => p.MinimumPercentage)
            .HasColumnType("decimal(8,2)");
        
        modelBuilder.Entity<VariableDiscounts>()
            .Property(p => p.MaximumPercentage)
            .HasColumnType("decimal(8,2)");

        modelBuilder.Entity<Clients>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        modelBuilder.Entity<FreebieRequest>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        modelBuilder.Entity<TermOptions>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        /*modelBuilder.Entity<Approvals>()
            .Property(p => p.Id)
            .UseHiLo("arcana_hilo_sequence");*/
        modelBuilder.Entity<OwnersAddress>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        modelBuilder.Entity<BusinessAddress>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        modelBuilder.Entity<FixedDiscounts>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");
        modelBuilder.Entity<Freezer>().Property(p => p.Id).UseHiLo("arcana_hilo_sequence");


        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Fullname = "Admin",
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                UserRolesId = 1
            });

        modelBuilder.Entity<UserRoles>().HasData(new UserRoles
        {
            Id = 1,
            UserRoleName = "Admin",
            IsActive = true,
            Permissions = new List<string>
            {
            "User Management",
            "User Account",
            "User Role",
            "Company",
            "Department",
            "Location",
            "Masterlist",
            "Products",
            "Meat Type",
            "UOM",
            "Discount Type",
            "Terms",
            "Customer Registration",
            "Prospect",
            "Direct",
            "Freebies",
            "Inventory",
            "Setup",
            "Product Category",
            "Product Sub Category",
            "Unit of Measurements",
            "Store Type",
            "Discount",
            "Term Days",
            "Approval",
            "Freebie Approval",
            "Direct Approval",
            "Admin Dashboard",
            "Direct Registration",
            "Listing Fee",
            "Registration Approval",
            "Sp. Discount Approval",
            "Listing Fee Approval",
            "Business Type",
            "Registration",
            "Customer Management",
            "Product Setup",
            "Variable Discount"
            }
        });

        modelBuilder.Entity<BookingCoverages>().HasData(
            new BookingCoverages { Id = 1, BookingCoverage = "F1", AddedBy = 1, IsActive = true},
            new BookingCoverages { Id = 2, BookingCoverage = "F2", AddedBy = 1, IsActive = true},
            new BookingCoverages { Id = 3, BookingCoverage = "F3", AddedBy = 1, IsActive = true},
            new BookingCoverages { Id = 4, BookingCoverage = "F4", AddedBy = 1, IsActive = true},
            new BookingCoverages { Id = 5, BookingCoverage = "F5", AddedBy = 1, IsActive = true}
         );

        modelBuilder.Entity<Terms>().HasData(
            new Terms { Id = 1, TermType = "COD", AddedBy = 1, IsActive = true},
            new Terms { Id = 2, TermType = "1 Up 1 Down", AddedBy = 1, IsActive = true},
            new Terms { Id = 3, TermType = "Credit Type", AddedBy = 1, IsActive = true}
        );

        modelBuilder.Entity<ModeOfPayment>().HasData(
            new ModeOfPayment { Id = 1, Payment = "Cash", AddedBy = 1, IsActive = true},
            new ModeOfPayment { Id = 2, Payment = "Online/Check", AddedBy = 1, IsActive = true}
        );

        modelBuilder.Entity<PriceMode>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<PriceMode>()
           .HasOne(x => x.ModifiedByUser)
           .WithMany()
           .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<PriceModeItems>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<PriceModeItems>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<SpecialDiscount>()
           .HasOne(x => x.AddedByUser)
           .WithMany()
           .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<SpecialDiscount>()
           .HasOne(x => x.ModifiedByUser)
           .WithMany()
           .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<Transactions>()
           .HasOne(x => x.AddedByUser)
           .WithMany()
           .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<TransactionItems>()
           .HasOne(x => x.AddedByUser)
           .WithMany()
           .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<TransactionSales>()
           .HasOne(x => x.AddedByUser)
           .WithMany()
           .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<AdvancePayment>()
           .HasOne(x => x.AddedByUser)
           .WithMany()
           .HasForeignKey(x => x.AddedBy);
        modelBuilder.Entity<AdvancePayment>()
           .HasOne(x => x.ModifiedByUser)
           .WithMany()
           .HasForeignKey(x => x.ModifiedBy);
        
        modelBuilder.Entity<PaymentTransaction>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<PaymentRecords>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);

        modelBuilder.Entity<PaymentRecords>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<ClearedPayments>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);

        modelBuilder.Entity<ClearedPayments>()
            .HasOne(x => x.AddedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);
    }
}