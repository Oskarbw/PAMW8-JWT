using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Jwt.API.Model
{
    public class PostOfficeContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }

        public PostOfficeContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\DbPostOffice");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parcel>(eb =>
            {
                eb.HasKey(p => p.Id);
                eb.Property(p => p.Sender).HasColumnType("varchar(50)");
                eb.Property(p => p.Receiver).HasColumnType("varchar(50)").IsRequired();
                eb.HasData(new List<Parcel>
                {
                    new Parcel
                    {
                        Id = 1,
                        Sender = "John",
                        Receiver = "Frank",
                        Weight = 12
                    },
                    new Parcel
                    {
                        Id = 2,
                        Sender = "Janice",
                        Receiver = "Hilary",
                        Weight = 17
                    }
                }
                );
            });

        }

    }
}
