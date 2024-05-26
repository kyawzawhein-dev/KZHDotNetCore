using KZHDotNetCore.PizzaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.PizzaApi.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<PizzaModel> pizzas { get; set; }
    public DbSet<PizzaExtraModel> pizzaExtra { get; set; }
    public DbSet<PizzaOrderModel> PizzaOrderModels { get; set; }
    public DbSet<PizzaOrderDetailsModel> PizzaOrderDetailsModels { get; set; }
}
public class PizzaOrderInvoiceHeadModel
{
    public int PizzaOrderId { get; set; }
    public string PizzaOrderInvoiceNo { get; set; }
    public decimal TotalAmount { get; set; }
    public int PizzaId { get; set; }
    public string Pizza { get; set; }
    public decimal Price { get; set; }
}

public class PizzaOrderInvoiceDetailModel
{
    public int PizzaOrderDetailId { get; set; }
    public string PizzaOrderInvoiceNo { get; set; }
    public int PizzaExtraId { get; set; }
    public string PizzaExtraName { get; set; }
    public decimal Price { get; set; }
}

public class PizzaOrderInvoiceResponse
{
    public PizzaOrderInvoiceHeadModel Order { get; set; }
    public List<PizzaOrderInvoiceDetailModel> OrderDetail { get; set; }
}
