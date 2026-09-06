using Association.Domain.Associations;
using Association.Domain.Members;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;


namespace Association.Infrastructure.Persistence
{
    public class AssociationDbContext : DbContext
    {
        public AssociationDbContext(DbContextOptions<AssociationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AssociationEntity> Associations => Set<AssociationEntity>();

        public DbSet<Member> Members => Set<Member>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================
            // AssociationEntity
            // =====================

            modelBuilder.Entity<AssociationEntity>(builder =>
            {
                builder.HasKey(a => a.Id);

                builder.Property(a => a.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new AssociationID(value));

                builder.Property(a => a.Name)
                    .HasConversion(
                        name => name.Value,
                        value => new Name(value));

                builder.HasMany(a => a.StaffMembers)
                    .WithOne()
                    .HasForeignKey(m => m.AssociationId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Navigation(a => a.StaffMembers)
                    .HasField("_members")
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // =====================
            // Member
            // =====================

            modelBuilder.Entity<Member>(builder =>
            {
                builder.HasKey(m => m.Id);

                builder.Property(m => m.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new MemberID(value));

                builder.Property(m => m.AssociationId)
                    .HasConversion(
                        id => id.Value,
                        value => new AssociationID(value));

                builder.Property(m => m.UserId)
                    .HasConversion(
                        userId => userId.Value,
                        value => new UserID(value))
                    .HasColumnName("UserId")
                    .IsRequired();

                builder.Property(m => m.FullName)
                    .HasConversion(
                        name => name.Value,
                        value => new Name(value))
                    .HasColumnName("FullName")
                    .IsRequired();

                builder.Property(m => m.Email)
                    .HasConversion(
                        email => email.Value,
                        value => new Email(value))
                    .HasColumnName("Email")
                    .IsRequired();

                builder.Property(m => m.Role)
                    .HasConversion<int>()
                    .IsRequired();

                builder.Property(m => m.DeletedAt)
                    .HasColumnName("DeletedAt");

                builder.HasIndex(m => new
                {
                    m.AssociationId,
                    m.UserId
                })
                .IsUnique()
                .HasFilter("[DeletedAt] IS NULL");
            });
        }
    }




}
