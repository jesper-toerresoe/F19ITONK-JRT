using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using F19ITONK.ASPNETCore.MicroService.ClassLib.Models;

namespace F19ITONK.ASPNETCore.MicroService.Backend.Models
{
    public class BackendDBContext : DbContext
    {
        public BackendDBContext (DbContextOptions<BackendDBContext> options)
            : base(options)
        {
        }

        public DbSet<F19ITONK.ASPNETCore.MicroService.ClassLib.Models.Haandvaerker> Haandvaerker { get; set; }

        public DbSet<F19ITONK.ASPNETCore.MicroService.ClassLib.Models.Vaerktoejskasse> Vaerktoejskasse { get; set; }

        public DbSet<F19ITONK.ASPNETCore.MicroService.ClassLib.Models.Vaerktoej> Vaerktoej { get; set; }
    }
}
