using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Visitor_Management.Models;

namespace Visitor_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Visitor_Management.Models.Visitor> Visitor { get; set; } = default!;

        public DbSet<Visitor_Management.Models.Item> Items{ get; set; } = default!;
        public DbSet<Visitor_Management.Models.Tempitems> TempItem{ get; set; } = default!;
    }
}