using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }
        
        public DbSet<Account> Accounts { get; set; } 
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => new
                {
                    e.NIK,
                    e.Email,
                    e.PhoneNumber
                }).IsUnique();

            //Many Education with One University(N: 1)
            //modelBuilder.Entity<Education>()
            //    .HasOne(e => e.University)
            //    .WithMany(u => u.Educations)
            //    .HasForeignKey(u => u.UniversityGuid);

            // One University with Many Education (1:N)
            modelBuilder.Entity<University>()
                .HasMany(u => u.Educations)
                .WithOne(e => e.University)
                .HasForeignKey(e => e.UniversityGuid)
                .OnDelete(DeleteBehavior.Restrict); // untuk memastikan data referensinya tidak bisa dihapus jika memiliki relasi FK

            // Room - Booking
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking - Employee
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee - Account
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a => a.Guid)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee - Education
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Education)
                .WithOne(e => e.Employee)
                .HasForeignKey<Education>(e => e.Guid)
                .OnDelete(DeleteBehavior.Restrict);

            // Account - Account Roles
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // Roles - Account Roles
            modelBuilder.Entity<Role>()
               .HasMany(r => r.AccountRoles)
               .WithOne(a => a.Role)
               .HasForeignKey(a => a.RoleGuid)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
