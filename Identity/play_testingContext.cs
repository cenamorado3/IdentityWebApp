
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Identity
{
    public partial class play_testingContext : DbContext
    {
        public play_testingContext()
        {
        }

        public play_testingContext(DbContextOptions<play_testingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aspnetrole> Aspnetroles { get; set; } = null!;
        public virtual DbSet<Aspnetuser> Aspnetusers { get; set; } = null!;
        public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; } = null!;
        public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; } = null!;
        public virtual DbSet<Aspnetuserrole> Aspnetuserroles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                using (ConfigurationManager config = new())
                {
                    optionsBuilder.UseNpgsql(config.GetConnectionString("Default"));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aspnetrole>(entity =>
            {
                entity.ToTable("aspnetroles");

                entity.HasIndex(e => e.Id, "aspnetrolesid_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Aspnetuser>(entity =>
            {
                entity.ToTable("aspnetusers");

                entity.HasIndex(e => e.Id, "id_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Discriminator)
                    .HasMaxLength(128)
                    .HasColumnName("discriminator");

                entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");

                entity.Property(e => e.Securitystamp).HasColumnName("securitystamp");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<Aspnetuserclaim>(entity =>
            {
                entity.ToTable("aspnetuserclaims");

                entity.HasIndex(e => e.Id, "aspnetuserclaims_id_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Claimtype).HasColumnName("claimtype");

                entity.Property(e => e.Claimvalue).HasColumnName("claimvalue");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Aspnetuserclaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("aspnetuserclaims_user_id_fkey");
            });

            modelBuilder.Entity<Aspnetuserlogin>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("aspnetuserlogins_pkey");

                entity.ToTable("aspnetuserlogins");

                entity.HasIndex(e => e.Userid, "aspnetuserlogins_userid_index")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("userid");

                entity.Property(e => e.Loginprovider)
                    .HasMaxLength(128)
                    .HasColumnName("loginprovider");

                entity.Property(e => e.Providerkey)
                    .HasMaxLength(128)
                    .HasColumnName("providerkey");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Aspnetuserlogin)
                    .HasForeignKey<Aspnetuserlogin>(d => d.Userid)
                    .HasConstraintName("aspnetuserlogins_userid_fkey");
            });

            modelBuilder.Entity<Aspnetuserrole>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("aspnetuserroles_pkey");

                entity.ToTable("aspnetuserroles");

                entity.HasIndex(e => e.Userid, "aspnetuserroles_userid_index")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("userid");

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Aspnetuserroles)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("aspnetuserroles_roleid_fkey");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Aspnetuserrole)
                    .HasForeignKey<Aspnetuserrole>(d => d.Userid)
                    .HasConstraintName("aspnetuserroles_userid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
