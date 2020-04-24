using AircraftFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryDatabaseImplement
{
    class AircraftFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Initial Catalog=AircraftFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Part> Parts { set; get; }

        public virtual DbSet<Aircraft> Aircrafts { set; get; }

        public virtual DbSet<AircraftPart> AircraftParts { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

        public virtual DbSet<Stock> Stocks { set; get; }

        public virtual DbSet<StockPart> StockParts { set; get; }
    }
}
