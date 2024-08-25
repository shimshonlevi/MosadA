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

        public DbSet<Agent> Agents { get; set; } = default!;
        public DbSet<Mission> Missions { get; set; } = default!;
        public DbSet<Target> Targets { get; set; } = default!;
    }
}
