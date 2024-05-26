using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KZHDotNetCore.PizzaApi.Models;

[Table("Tbl_PizzaOrder")]
public class PizzaOrderModel
{
    [Key]
    public int PizzaOrderId { get; set; }
    public string PizzaOrderinvoiceNo { get; set; }
    public int PizzaId { get; set; }
    public decimal TotalAmount { get; set; }
}
