﻿namespace KZHDotNetCore.RestApiWithNLayer.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}