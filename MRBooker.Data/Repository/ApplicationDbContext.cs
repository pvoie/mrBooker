﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRBooker.Data.Mappers;
using MRBooker.Data.Models.Entities;

namespace MRBooker.Data.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            new ReservationMap(builder.Entity<Reservation>());
            //builder.Entity<Reservation>().ToTable("Reservations");
            //builder.Entity<Reservation>().HasKey(x => x.Id);
            //builder.Entity<Reservation>().Property(x => x.Id).IsRequired();
            //builder.Entity<Reservation>().Property(x => x.Title).IsRequired().HasMaxLength(100);
            //builder.Entity<Reservation>().Property(x => x.Description).HasMaxLength(500);
            //builder.Entity<Reservation>().Property(x => x.Status).IsRequired().HasMaxLength(50);
            //builder.Entity<Reservation>().Property(x => x.Start).IsRequired();
            //builder.Entity<Reservation>().Property(x => x.End).IsRequired();

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<ApplicationUser>().HasKey(x => x.Id);
            builder.Entity<ApplicationUser>().Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Entity<ApplicationUser>()
                .HasMany(r => r.Reservations)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.SetNull);

            //builder.Entity<Reservation>()
            //    .HasOne(u => u.User)
            //    .WithMany(r => r.Reservations)
            //    .HasForeignKey(x => x.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
