using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KZHDotNetCore.PizzaApi.Models;

[Table("Tbl_PizzaOrderDetail")]
public class PizzaOrderDetailsModel
{
    [Key]
    public int PizzaOrderDetailId { get; set; }
    public string PizzaOrderInvoiceNo { get; set; }
    public int PizzaExtraId { get; set; }
}
