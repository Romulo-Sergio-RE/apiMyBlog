using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set;}
        public DbSet<Comment> Comments { get; set;}
    }
}