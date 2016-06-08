using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace LibraryApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<AuthorItem> Authors { get; set; }

        public DbSet<BookItem> Books { get; set; }
    }
}
