using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCOREAPI.Models
{
    public class DudeContext : DbContext
    {
        public DudeContext(DbContextOptions<DudeContext> options) : base(options)
        {

        }

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