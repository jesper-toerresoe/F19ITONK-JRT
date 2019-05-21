using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using F19ITONK.ASPNETCore.MicroService.ClassLib.Models;

namespace F19ITONK.ASPNETCore.MicroService.SecondFrontEnd.Models
{
    public class SecondFrontEndContext : DbContext
    {
        public SecondFrontEndContext (DbContextOptions<SecondFrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<F19ITONK.ASPNETCore.MicroService.ClassLib.Models.Haandvaerker> Haandvaerker { get; set; }
    }
}
