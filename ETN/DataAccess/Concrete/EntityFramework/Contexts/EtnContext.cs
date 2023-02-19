using Castle.Core.Resource;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class EtnContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MSKK-HCM-LT6548\SQLEXPRESS;Database=Etn;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Order Table */


            //modelBuilder.Entity<Order>()
            //            .HasOne<OrderInfo>(o => o.OrderInfo)
            //            .WithMany(oi => oi.Orders)
            //            .HasForeignKey(o => o.OrderInfoId);


            /* OrderInfo Table */
            //modelBuilder.Entity<OrderInfo>()
            //            .HasMany<Order>(oi => oi.Orders)
            //            .WithOne(o => o.OrderInfo)
            //            .HasForeignKey(o => o.OrderInfoId);




            /* Product-Order -> Many-to-Many */
            modelBuilder.Entity<ProductOrder>().HasKey(i => new { i.PId, i.OId });

            modelBuilder.Entity<ProductOrder>()
                        .HasOne<Product>(p => p.Product)
                        .WithMany(po => po.ProductOrders)
                        .HasForeignKey(p => p.PId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductOrder>()
                        .HasOne<Order>(o => o.Order)
                        .WithMany(po => po.ProductOrders)
                        .HasForeignKey(o => o.OId);


            /* Log Table */
            modelBuilder.Entity<Log>()
                        .Property(l => l.Detail)
                        .IsRequired(false);

            modelBuilder.Entity<Log>()
                        .Property(l => l.Audit)
                        .IsRequired(false);

            /* OperationClaim Table */
            modelBuilder.Entity<OperationClaim>()
                        .Property(x => x.Name)
                        .HasMaxLength(250)
                        .IsRequired();

            /* User Table */
            modelBuilder.Entity<User>()
                        .Property(x => x.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(x => x.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(x => x.Email)
                        .HasMaxLength(50)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(x => x.PasswordSalt)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(x => x.PasswordHash)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(x => x.Status)
                        .IsRequired();
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
