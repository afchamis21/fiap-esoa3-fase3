using Fiap.Agnello.CLI.Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Agnello.CLI.DB
{
    internal class DatabaseContext : DbContext
    {
        private static readonly string CONN_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AgnelloCLI;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public virtual DbSet<Wine> Wines { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONN_STRING).EnableSensitiveDataLogging();    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wine>(entity =>
            {
                entity.ToTable("VINHOS");
                entity.HasKey(e => e.Id);

                #region columns
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Price).IsRequired().HasPrecision(10, 2);
                entity.Property(e => e.Grape).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Stock).HasDefaultValue(0);
                entity.Property(e => e.MakerId).IsRequired();
                #endregion

                #region relationships
                entity.HasOne(e => e.Maker)
                      .WithMany()
                      .HasForeignKey(e => e.MakerId)
                      .OnDelete(DeleteBehavior.Restrict);
                #endregion

                #region constraints
                entity.HasIndex(e => e.Name).IsUnique();
                #endregion
            });


            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("MARCAS");
                entity.HasKey(e => e.Id);

                #region columns
                entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                #endregion

                #region constraints
                entity.HasIndex(e => e.Name);
                #endregion
            });
        }
    }
}
