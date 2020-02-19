using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Infrastructure.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
            : base(options)
        { }

        //public DbSet<DbAccount> Accounts { get; set; }
        //public DbSet<DbAccountPermission> AccountPermissions { get; set; }
        //public DbSet<DbPermission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        }
    }
}
