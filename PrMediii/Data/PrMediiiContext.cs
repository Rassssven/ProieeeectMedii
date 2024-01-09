using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrMediii.Modules;

namespace PrMediii.Data
{
    public class PrMediiiContext : DbContext
    {
        public PrMediiiContext (DbContextOptions<PrMediiiContext> options)
            : base(options)
        {
        }

        public DbSet<PrMediii.Modules.Module> Module { get; set; } = default!;

        public DbSet<PrMediii.Modules.Trainer>? Trainer { get; set; }

        public DbSet<PrMediii.Modules.Course>? Course { get; set; }

        public DbSet<PrMediii.Modules.Student>? Student { get; set; }

        public DbSet<PrMediii.Modules.StudentGroups>? StudentGroups { get; set; }
    }
}
