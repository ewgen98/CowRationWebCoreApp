using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CowRationWebApplication
{
    public partial class CowRationContext : IdentityDbContext<User>
    {
        public virtual DbSet<CatalogIndexNutritional> CatalogIndexNutritional { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Faza> Faza { get; set; }
        public virtual DbSet<FnutritionalCharacteristics> FnutritionalCharacteristics { get; set; }
        public virtual DbSet<Korm> Korm { get; set; }
        public virtual DbSet<Milk> Milk { get; set; }
        public virtual DbSet<NutritionalCategories> NutritionalCategories { get; set; }
        public virtual DbSet<Ration> Ration { get; set; }
        public virtual DbSet<RationCow> RationCow { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        // Unable to generate entity type for table 'dbo.NNutritionalCharacteristics'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.NormNeed'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=10.10.10.12;DataBase=CowRation;User ID=123;Password=321;Integrated Security=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogIndexNutritional>(entity =>
            {
                entity.HasKey(e => e.IdIndex);

                entity.Property(e => e.IdIndex)
                    .HasColumnName("Id_index")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Article)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Faza>(entity =>
            {
                entity.HasKey(e => e.IdFaza);

                entity.Property(e => e.IdFaza).HasColumnName("Id_faza");

                entity.Property(e => e.Faza1)
                    .IsRequired()
                    .HasColumnName("Faza")
                    .HasColumnType("nchar(20)");
            });

            modelBuilder.Entity<FnutritionalCharacteristics>(entity =>
            {
                entity.HasKey(e => new { e.IdTask, e.IdKorm, e.IdIndex });

                entity.ToTable("FNutritionalCharacteristics");

                entity.Property(e => e.IdTask).HasColumnName("Id_task");

                entity.Property(e => e.IdKorm).HasColumnName("Id_korm");

                entity.Property(e => e.IdIndex).HasColumnName("Id_index");

                entity.Property(e => e.Fvalue).HasColumnName("FValue");

                entity.Property(e => e.IdCategory).HasColumnName("Id_category");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.FnutritionalCharacteristics)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FNutritionalCharacteristics_NutritionalCategories");

                entity.HasOne(d => d.IdIndexNavigation)
                    .WithMany(p => p.FnutritionalCharacteristics)
                    .HasForeignKey(d => d.IdIndex)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FNutritionalCharacteristics_CatalogIndexNutritional");

                entity.HasOne(d => d.IdKormNavigation)
                    .WithMany(p => p.FnutritionalCharacteristics)
                    .HasForeignKey(d => d.IdKorm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FNutritionalCharacteristics_Korm1");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.FnutritionalCharacteristics)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FNutritionalCharacteristics_Task");
            });

            modelBuilder.Entity<Korm>(entity =>
            {
                entity.HasKey(e => e.IdKorm);

                entity.Property(e => e.IdKorm)
                    .HasColumnName("Id_korm")
                    .ValueGeneratedNever();

                entity.Property(e => e.KormCategory).HasColumnName("Korm_category");

                entity.Property(e => e.NameKorm)
                    .IsRequired()
                    .HasColumnName("Name_korm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceKorm)
                    .HasColumnName("Price_korm")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Voluminousness)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Milk>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<NutritionalCategories>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.Property(e => e.IdCategory).HasColumnName("Id_category");

                entity.Property(e => e.FatContent)
                    .HasColumnName("Fat_content")
                    .HasColumnType("decimal(3, 2)");

                entity.Property(e => e.IdFaza).HasColumnName("Id_faza");

                entity.Property(e => e.Protein).HasColumnType("decimal(3, 2)");

                entity.HasOne(d => d.IdFazaNavigation)
                    .WithMany(p => p.NutritionalCategories)
                    .HasForeignKey(d => d.IdFaza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NutritionalCategories_Faza");
            });

            modelBuilder.Entity<Ration>(entity =>
            {
                entity.HasKey(e => new { e.IdRation, e.IdKorm });

                entity.Property(e => e.IdRation).HasColumnName("Id_ration");

                entity.Property(e => e.IdKorm).HasColumnName("Id_korm");

                entity.Property(e => e.Heft).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.IdTask).HasColumnName("Id_task");

                entity.HasOne(d => d.IdKormNavigation)
                    .WithMany(p => p.Ration)
                    .HasForeignKey(d => d.IdKorm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ration_Korm");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Ration)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ration_Task");
            });

            modelBuilder.Entity<RationCow>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Korm)
                    .WithMany(p => p.RationCow)
                    .HasForeignKey(d => d.KormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RationCow_Korm");

                entity.HasOne(d => d.Milk)
                    .WithMany(p => p.RationCow)
                    .HasForeignKey(d => d.MilkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RationCow_Milk");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Korm)
                    .WithMany(p => p.Storage)
                    .HasForeignKey(d => d.KormId)
                    .HasConstraintName("FK_Storage_Korm");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask);

                entity.HasIndex(e => new { e.IdTask, e.Task1 })
                    .HasName("IDZad");

                entity.Property(e => e.IdTask).HasColumnName("Id_task");

                entity.Property(e => e.Task1)
                    .IsRequired()
                    .HasColumnName("Task")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }

        public CowRationContext(DbContextOptions<CowRationContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
