using Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Data
{
    class ApplicationDbContext:DbContext
    {
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public DbSet<Nominee> Nominees { get; set; }
    }
}
