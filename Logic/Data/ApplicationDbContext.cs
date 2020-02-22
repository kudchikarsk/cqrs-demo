using Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }

    public sealed class DbContextFactory
    {
        private readonly ConnectionString connectionString;

        public DbContextFactory(ConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public ApplicationDbContext GetApplicationContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString.Value);
            return new ApplicationDbContext(builder.Options);
        }
    }

    public class ConnectionString
    {
        public ConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
