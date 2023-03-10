using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
