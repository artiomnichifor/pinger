using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbAccessLayer
{
    public class PingerContext : DbContext
    {
        public PingerContext(DbContextOptions<PingerContext> options)
            : base(options)
        { }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Ping> Pings { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=DbAccessLayer/sites.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SiteConfiguration());
            modelBuilder.ApplyConfiguration(new PingConfiguration());


        }

    }


    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PingerContext>
    {
        public PingerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PingerContext>();
            optionsBuilder.UseSqlite("Data Source=sites.sqlite");

            return new PingerContext(optionsBuilder.Options);
        }
    }
}
