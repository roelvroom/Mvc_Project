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
        public Mvc_ProjectContext (DbContextOptions<Mvc_ProjectContext> options) : base(options) { }

        public DbSet<Aankoop> Aankopen { get; set; } = default!;
        public DbSet<Gebruiker>? Gebruikers { get; set; } = default!;
        public DbSet<Vak>? Vakken { get; set; } = default!;
        public DbSet<Bijlagen>? Bijlagen { get; set; } = default!;
        public DbSet<Product>? Producten { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aankoop>().ToTable("Aankoop");
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<Vak>().ToTable("Vak");
            modelBuilder.Entity<Bijlagen>().ToTable("Bijlagen");
            modelBuilder.Entity<Product>().ToTable("Producten");
        }

    }
}
