using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TrubyiPakety.Models
{
    public class TrubyiPaketyDbContext : DbContext
    {
        public DbSet<Pipe> Pipes { get; set; }
        public DbSet<Package> Packages { get; set; }

        public TrubyiPaketyDbContext(DbContextOptions<TrubyiPaketyDbContext> options) : base(options) { }
    }
}
