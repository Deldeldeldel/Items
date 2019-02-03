using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Items.Models
{
    public partial class ItemdataContext : DbContext
    {
        public ItemdataContext()
        {
        }

        public ItemdataContext(DbContextOptions<ItemdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Kayttaja> Kayttaja { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Itemdata;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                //ValueGeneratedNever() muutettu ValueGeneratedOnAdd(), jotta tuottaa itse id-keyn (taulu myös muutettu SET IDENTITY_INSERT Item ON +
                // Is Identity Yes, identity increment 1
                entity.Property(e => e.ItemId).ValueGeneratedOnAdd();

                entity.Property(e => e.ItemBox).HasMaxLength(50);

                entity.Property(e => e.ItemClass)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemDescription).HasMaxLength(50);

                entity.Property(e => e.ItemLocation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemOwner).HasMaxLength(50);
            });

            modelBuilder.Entity<Kayttaja>(entity =>
            {
                //ValueGeneratedNever() muutettu ValueGeneratedOnAdd(), jotta tuottaa itse id-keyn (taulu myös muutettu SET IDENTITY_INSERT Item ON +
                // Is Identity Yes, identity increment 1
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
