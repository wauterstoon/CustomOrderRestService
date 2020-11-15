using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace CustomerOrderRESTService.EFLayer.DataAccess
{
    public class DataContext : DbContext
    {
        private string connectionString;

        public DataContext()
        {
        }

        public DataContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        private void SetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;

                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        public DbSet<BusinessLayer.Models.Order> Orders { get; set; }
        public DbSet<BusinessLayer.Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessLayer.Models.Customer>(ConfigureCustomer);
            modelBuilder.Entity<BusinessLayer.Models.Order>(ConfigureOrder);
        }

        private void ConfigureCustomer(EntityTypeBuilder<BusinessLayer.Models.Customer> entityBuilder)
        {
            entityBuilder.ToTable("Customer");
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(200);

            entityBuilder
                .Property(x => x.UniqueNameAddressCombo)
                .IsRequired()
                .HasMaxLength(300);
        }

        private void ConfigureOrder(EntityTypeBuilder<BusinessLayer.Models.Order> entityBuilder)
        {
            entityBuilder.ToTable("Order");
            entityBuilder.HasKey(x => x.Id);

            entityBuilder
                    .HasOne(x => x.Customer)
                    .WithMany(x => x.Orders)
                    .IsRequired(true)
                    .HasForeignKey(x => x.CustomerId);

            entityBuilder.Property(x => x.Amount)
                .IsRequired();

            entityBuilder.Property(x => x.Product)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}