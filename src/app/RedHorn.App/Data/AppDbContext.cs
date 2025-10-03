using Microsoft.EntityFrameworkCore;
using RedHorn.App.Models;

namespace RedHorn.App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Question> Questions => Set<Question>();
}
