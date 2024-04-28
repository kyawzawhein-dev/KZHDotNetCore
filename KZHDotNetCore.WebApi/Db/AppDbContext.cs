using KZHDotNetCore.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.WebApi.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogDto> Blogs { get; set; }
}