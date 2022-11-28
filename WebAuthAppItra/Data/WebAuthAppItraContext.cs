using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAuthAppItra.Models;

namespace WebAuthAppItra.Data
{
    public class WebAuthAppItraContext : DbContext
    {
        public WebAuthAppItraContext (DbContextOptions<WebAuthAppItraContext> options)
            : base(options)
        {
        }

        public DbSet<WebAuthAppItra.Models.User> User { get; set; } = default!;
    }
}
