using HotelGuide.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelGuide.Context
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.UUID);

            modelBuilder.Entity<ContactInfo>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<ContactInfo>()
             .HasOne<Hotel>()
             .WithMany(h => h.ContactInfos)
             .HasForeignKey(c => c.HotelUUID);
        }

    }

}
