using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;

namespace Mvc_Project.Data
{
    public class Mvc_ProjectContext : DbContext
    {
        public Mvc_ProjectContext (DbContextOptions<Mvc_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Aankoop> Aankoop { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aankoop>().ToTable("Aankoop");
        }
    }
}
