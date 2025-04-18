using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<PolicyRequest> PolicyRequests { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
}

