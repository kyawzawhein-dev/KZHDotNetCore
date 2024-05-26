namespace KZHDotNetCore.PizzaApi.Models;

public class OrderReponse
{
    public string Message { get; set; }
    public string InvoiceNo { get; set; }
    public decimal TotalAmount { get; set; }
}
