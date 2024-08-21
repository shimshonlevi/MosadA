using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mosad1.Models;

namespace Mosad1.Data
{
    public class Mosad1Context : DbContext
    {
        public Mosad1Context (DbContextOptions<Mosad1Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Mosad1.Models.Agent> Agent { get; set; } = default!;
        public DbSet<Mosad1.Models.Mission> Mission { get; set; } = default!;
        public DbSet<Mosad1.Models.Target> Target { get; set; } = default!;
    }
}
