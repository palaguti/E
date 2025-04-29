using System;
using E.EN;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E.DAL
{
    public class EDBContext : DbContext
    {
        public EDBContext(DbContextOptions<EDBContext> options) : base(options)
        {
        }

        public DbSet<PersonaE> PersonaEs { get; set; }
    }
}
