using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCOREAPI.Models
{
    public class DynamicsContext : DbContext
    {
        public DynamicsContext(DbContextOptions<DynamicsContext> options) : base(options)
        {

        }

        // Moneda
        //public DbSet<MC40200> MC40200 { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=xxx;Database=Demo;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyModelRelationship(modelBuilder);
        }

        private void ApplyModelRelationship(ModelBuilder modelBuilder)
        {

        }
    }
}
