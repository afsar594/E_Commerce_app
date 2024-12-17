using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;

namespace E_Commerce.Data
{
    public partial class E_CommerceContext : DbContext
    {
        public E_CommerceContext()
        {
        }

        public E_CommerceContext(DbContextOptions<E_CommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Add configuration logic here if needed
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Item entity
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Items");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");
                entity.Property(e => e.Price)
                    .HasColumnName("Price");
                entity.Property(e => e.Description)
                    .HasColumnName("Description");
                entity.Property(e => e.Brand)
                    .HasColumnName("Brand");
                entity.Property(e => e.FilePath)
                    .HasColumnName("FilePath");
                entity.Property(e => e.Category)
                    .HasColumnName("Category");
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");
                entity.Property(e => e.Price)
                    .HasColumnName("Price");
                entity.Property(e => e.Description)
                    .HasColumnName("Description");
                entity.Property(e => e.Brand)
                    .HasColumnName("Brand");
                entity.Property(e => e.FilePath)
                    .HasColumnName("FilePath");
                entity.Property(e => e.Category)
                    .HasColumnName("Category");
                entity.Property(e => e.TotalPrice)
                 .HasColumnName("TotalPrice");
                entity.Property(e => e.UserId)
                .HasColumnName("UserId");
                entity.Property(e => e.itemId)
              .HasColumnName("itemId");
                entity.Property(e => e.Qty) // Fixed casing
       .HasColumnName("qty");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();  // Ensure the ID is auto-generated

                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
