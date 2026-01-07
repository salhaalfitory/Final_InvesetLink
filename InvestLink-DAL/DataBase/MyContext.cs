using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.DataBase
{
    public class MyContext:IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
           : base(options)
        {
        }



        public DbSet<Project> Projects { get; set; }

        public DbSet<CoordinatorReport> CoordinatorReports { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Nationality> Nationalitys { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<ProjectInvestor> ProjectInvestors { get; set; }
        public DbSet<ProjectCoordinator> ProjectCoordinators { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        //----------------------------------------------
       
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProjectCoordinator>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<ProjectCoordinator>()
                .HasIndex(sc => new { sc.EmployeeId, sc.ProjectId })
                .IsUnique(); // Ensure unique pair

            modelBuilder.Entity<ProjectCoordinator>()
            .HasOne(bc => bc.Employee)
            .WithMany(b => b.ProjectCoordinators)
            .HasForeignKey(bc => bc.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<ProjectCoordinator>()
            .HasOne(bc => bc.Project)
            .WithMany(c => c.ProjectCoordinators)
            .HasForeignKey(bc => bc.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            //--------------------------------------------
            modelBuilder.Entity<ProjectInvestor>()
               .HasKey(sc => sc.Id);

            modelBuilder.Entity<ProjectInvestor>()
               .HasIndex(sc => new { sc.InvestorId, sc.ProjectId })
               .IsUnique(); // Ensure unique pair


            modelBuilder.Entity<ProjectInvestor>()
            .HasOne(bc => bc.Investor)
            .WithMany(b => b.ProjectInvestors)
            .HasForeignKey(bc => bc.InvestorId)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<ProjectInvestor>()
            .HasOne(bc => bc.Project)
            .WithMany(c => c.ProjectInvestors)
            .HasForeignKey(bc => bc.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);



        }
    }
}

