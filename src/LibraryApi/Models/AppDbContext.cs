using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace LibraryApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<AuthorItem> Authors { get; set; }

        public DbSet<BookItem> Books { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
