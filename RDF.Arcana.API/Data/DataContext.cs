using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RDF.Arcana.API.Domain;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RDF.Arcana.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
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
    public virtual DbSet<Approvals> Approvals { get; set; }
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

        modelBuilder.Entity<UserRoles>()
            .HasOne(ur => ur.User)
            .WithOne(u => u.UserRoles)
            .HasForeignKey<User>(u => u.UserRoleId);

        modelBuilder.Entity<Company>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Company>(x => x.AddedBy);

        modelBuilder.Entity<Department>()
            .HasOne(u => u.AddedByUser)
            .WithOne()
            .HasForeignKey<Department>(u => u.AddedBy);

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
            .WithOne()
            .HasForeignKey<UserRoles>(u => u.AddedBy);

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.RequestedByUser)
            .WithMany()
            .HasForeignKey(x => x.AddedBy);
        
        modelBuilder.Entity<Clients>()
            .HasOne(x => x.ModifiedByUser)
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy);
        
        modelBuilder.Entity<Clients>()
            .HasMany(c => c.ClientDocuments)
            .WithOne(cd => cd.Clients)
            .HasForeignKey(cd => cd.ClientId);

        modelBuilder.Entity<Clients>()
            .HasMany(c => c.Approvals)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId);
        
        modelBuilder.Entity<FreebieRequest>()
            .HasOne(fr => fr.Approvals)
            .WithOne(a => a.FreebieRequest)
            .HasForeignKey<FreebieRequest>(fr => fr.ApprovalId);

        modelBuilder.Entity<Approvals>()
            .HasOne(a => a.FreebieRequest)
            .WithOne(fr => fr.Approvals)
            .OnDelete(DeleteBehavior.Restrict);

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

        modelBuilder.Entity<Clients>()
            .HasOne(x => x.ModeOfPayments)
            .WithMany()
            .HasForeignKey(x => x.ModeOfPayment);

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

        modelBuilder.Entity<TermOptions>()
            .HasOne(x => x.Clients)
            .WithMany()
            .HasForeignKey(x => x.ClientId);
        
        modelBuilder.Entity<Clients>()
            .HasOne(x => x.FixedDiscounts)
            .WithMany()
            .HasForeignKey(x => x.FixedDiscountId);

        modelBuilder.Entity<FixedDiscounts>()
            .HasOne(x => x.Clients)
            .WithMany()
            .HasForeignKey(x => x.ClientId);
    }
}