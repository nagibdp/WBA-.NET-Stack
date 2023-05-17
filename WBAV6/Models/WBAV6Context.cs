using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WBAV6.Models
{
    public partial class WBAV6Context : DbContext
    {
        public WBAV6Context()
        {
        }

        public WBAV6Context(DbContextOptions<WBAV6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Proyect> Proyects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Validation> Validations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database=WBAV6; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.HasIndex(e => e.UserId, "UQ__profiles__CB9A1CFE8286212D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Academy)
                    .HasMaxLength(255)
                    .HasColumnName("academy");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Cel)
                    .HasMaxLength(10)
                    .HasColumnName("cel");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DtVisible).HasColumnName("dt_visible");

                entity.Property(e => e.Estudy).HasColumnName("estudy");

                entity.Property(e => e.Keyword1)
                    .HasMaxLength(255)
                    .HasColumnName("keyword1");

                entity.Property(e => e.Keyword2)
                    .HasMaxLength(255)
                    .HasColumnName("keyword2");

                entity.Property(e => e.Keyword3)
                    .HasMaxLength(255)
                    .HasColumnName("keyword3");

                entity.Property(e => e.Keyword4)
                    .HasMaxLength(255)
                    .HasColumnName("keyword4");

                entity.Property(e => e.Keyword5)
                    .HasMaxLength(255)
                    .HasColumnName("keyword5");

                entity.Property(e => e.Picture)
                    .HasMaxLength(255)
                    .HasColumnName("picture");

                entity.Property(e => e.PlaceAt)
                    .HasMaxLength(255)
                    .HasColumnName("place_at");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Profile)
                    .HasForeignKey<Profile>(d => d.UserId)
                    .HasConstraintName("fkTeacher");
            });

            modelBuilder.Entity<Proyect>(entity =>
            {
                entity.ToTable("proyects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Document)
                    .HasMaxLength(255)
                    .HasColumnName("document");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(255)
                    .HasColumnName("keyword");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "UQ__users__AB6E61640278F999")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Career)
                    .HasMaxLength(30)
                    .HasColumnName("career");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.IdProyect).HasColumnName("idProyect");

                entity.Property(e => e.LastNameF)
                    .HasMaxLength(30)
                    .HasColumnName("lastNameF");

                entity.Property(e => e.LastNameM)
                    .HasMaxLength(30)
                    .HasColumnName("lastNameM");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("pass");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("role");

                entity.HasOne(d => d.IdProyectNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdProyect)
                    .HasConstraintName("fkProyect");
            });

            modelBuilder.Entity<Validation>(entity =>
            {
                entity.ToTable("validation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
