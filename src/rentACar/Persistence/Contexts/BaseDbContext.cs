using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext :DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public BaseDbContext(DbContextOptions options, IConfiguration configuration) :base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        public DbSet<CreditCardInfo> CreditCardInfos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CarDamage> CarDamages { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<AdditionalService> AdditionalServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("RentACarConnectionString")) );
            //}            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly() );

            //tüm tablolarda delete yapılınca cascade yapılmamasını sağlar:
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.NoAction;
            //}

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users").HasKey(k => k.Id);
                u.Property(p => p.UserName).HasColumnName("UserName");
                u.Property(p => p.Email).HasColumnName("Email");
                u.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                u.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                u.Property(p => p.Status).HasColumnName("Status");
            });

            modelBuilder.Entity<Customer>(c =>
            {
                c.ToTable("Customers");
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Email).HasColumnName("Email");
            });

            modelBuilder.Entity<IndividualCustomer>(c =>
            {
                c.ToTable("IndividualCustomers");
                c.Property(p => p.NationalId).HasColumnName("NationalId");
                c.Property(p => p.FirstName).HasColumnName("FirstName");
                c.Property(p => p.LastName).HasColumnName("LastName");
            });

            modelBuilder.Entity<CorporateCustomer>(c =>
            {
                c.ToTable("CorporateCustomers");
                c.Property(p => p.TaxNumber).HasColumnName("TaxNumber");
                c.Property(p => p.CompanyName).HasColumnName("CompanyName");
            });

            modelBuilder.Entity<OperationClaim>(oc =>
            {
                oc.ToTable("OperationClaims").HasKey(k => k.Id);
                oc.Property(o=> o.Id).HasColumnName("Id");
                oc.Property(o=> o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(oc =>
            {
                oc.ToTable("UserOperationClaims").HasKey(k => k.Id);
                oc.Property(o => o.Id).HasColumnName("Id");
                oc.Property(o => o.UserId).HasColumnName("UserId");
                oc.Property(o => o.OperationClaimId).HasColumnName("OperationClaimId");
            });

            modelBuilder.Entity<CarDamage>(c =>
            {
                c.ToTable("CarDamages").HasKey(k => k.Id);
                c.HasOne(cd => cd.Car);
            });

            modelBuilder.Entity<Brand>( b =>
            {
                b.ToTable("Brands").HasKey(k => k.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.Name).HasColumnName("Name");
                //b.HasMany(p => p.Models);
            });

            modelBuilder.Entity<CreditCardInfo>(c =>
            {
                c.ToTable("CreditCardInfos").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.CustomerId).HasColumnName("CustomerId");
                c.Property(p => p.CreditCardNo).HasColumnName("CreditCardNo");
                c.Property(p => p.CVC).HasColumnName("CVC");
                c.Property(p => p.ValidDate).HasColumnName("ValidDate");
                c.Property(p => p.CardHolder).HasColumnName("CardHolder");
                c.HasOne(p => p.Customer);
            });

            modelBuilder.Entity<Color>(c =>
            {
                c.ToTable("Colors").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                c.HasMany(p => p.Cars);
            });

            modelBuilder.Entity<City>(c =>
            {
                c.ToTable("Cities").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                //c.HasMany(p => p.Cars);
            });

            modelBuilder.Entity<Payment>(c =>
            {
                c.ToTable("Payments").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.PaymentDate).HasColumnName("PaymentDate");
                c.Property(p => p.TotalSum).HasColumnName("TotalSum");
                c.Property(p => p.RentalId).HasColumnName("RentalId");
            });

            modelBuilder.Entity<Fuel>(c =>
            {
                c.ToTable("Fuels").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                //c.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Transmission>(c =>
            {
                c.ToTable("Transmissions").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                c.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Model>(m =>
            {
                m.ToTable("Models").HasKey(k => k.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.Name).HasColumnName("Name");
                m.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
                m.Property(p => p.FuelId).HasColumnName("FuelId");
                m.Property(p => p.BrandId).HasColumnName("BrandId");
                m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
                m.HasMany(p => p.Cars);
                m.HasOne(p => p.Transmission);
                m.HasOne(p => p.Fuel);
                m.HasOne(p => p.Brand);
            });

            modelBuilder.Entity<Maintenance>(m =>
            {
                m.ToTable("Maintenances").HasKey(k => k.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.CarId).HasColumnName("CarId");
                m.Property(p => p.Description).HasColumnName("Description");
                m.Property(p => p.MaintenanceDate).HasColumnName("MaintenanceDate");
                m.Property(p => p.ReturnDate).HasColumnName("ReturnDate");
                m.HasOne(c => c.Car);
            });

            modelBuilder.Entity<Rental>( r =>
            {
                r.ToTable("Rentals").HasKey(r => r.Id);
                r.Property(p => p.Id).HasColumnName("Id");
                r.Property(p => p.CarId).HasColumnName("CarId");
                r.Property(p => p.CustomerId).HasColumnName("CustomerId");
                r.Property(p => p.RentCityId).HasColumnName("RentCityId");
                r.Property(p => p.ReturnCityId).HasColumnName("ReturnCityId");
                r.Property(p => p.ReturnedCityId).HasColumnName("ReturnedCityId");
                r.Property(p => p.RentDate).HasColumnName("RentDate");
                r.Property(p => p.ReturnDate).HasColumnName("ReturnDate");
                r.Property(p => p.ReturnedDate).HasColumnName("ReturnedDate");
                r.Property(p => p.RentedKilometer).HasColumnName("RentedKilometer");
                r.Property(p => p.ReturnedKilometer).HasColumnName("ReturnedKilometer");
                r.HasOne(r => r.Car);
            });

            modelBuilder.Entity<Car>(c =>
            {
                c.ToTable("Cars").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.ModelId).HasColumnName("ModelId");
                c.Property(p => p.ColorId).HasColumnName("ColorId");
                c.Property(p => p.CityId).HasColumnName("CityId");
                c.Property(p => p.Plate).HasColumnName("Plate");
                c.Property(p => p.ModelYear).HasColumnName("ModelYear");
                c.Property(p => p.CarState).HasColumnName("CarState");
                c.HasOne(c => c.Model);
                c.HasOne(c => c.Color);
                c.HasOne(c => c.City);
            });

        }
    }
}
