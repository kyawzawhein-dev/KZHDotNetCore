using KZHDotNetCore.PizzaApi.Db;
using KZHDotNetCore.PizzaApi.Models;
using KZHDotNetCore.PizzaApi.Queries;
using KZHDotNetCore.Share;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.PizzaApi.Features.Pizza;

[Route("api/[controller]")]
[ApiController]
public class PizzaController : Controller
{

    private readonly AppDbContext _appDbContext;
    private readonly DapperService _dapperService;
    public PizzaController()
    {
        _appDbContext = new AppDbContext();
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var item = await _appDbContext.pizzas.ToListAsync();
        return Ok(item);
    }
    [HttpGet("Extras")]
    public async Task<IActionResult> GetExtrasAsync()
    {
        var item = await _appDbContext.pizzaExtra.ToListAsync();
        return Ok(item);
    }
    [HttpPost("Order")]
    public async Task<IActionResult> GetOrderAsync(OrderRequest orderRequest)
    {
        var itemPizza = await _appDbContext.pizzas.FirstOrDefaultAsync(x => x.PizzaId == orderRequest.PizzaId);
        var total = itemPizza.Price;

        if (orderRequest.Extras.Length > 0)
        {
            // select * from Tbl_PizzaExtras where PizzaExtraId in (1,2,3,4)
            //foreach (var item in orderRequest.Extras)
            //{
            //}

            var lstExtra = await _appDbContext.pizzaExtra.Where(x => orderRequest.Extras.Contains(x.PizzaExtraId)).ToListAsync();
            total += lstExtra.Sum(x => x.Price);
        }

        PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
        {
            PizzaId = orderRequest.PizzaId,
            PizzaOrderinvoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss"),
            TotalAmount = total
        };
        List<PizzaOrderDetailsModel> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailsModel
        {
            PizzaExtraId = extraId,
            PizzaOrderInvoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss"),
        }).ToList();

        await _appDbContext.PizzaOrderModels.AddAsync(pizzaOrderModel);
        await _appDbContext.PizzaOrderDetailsModels.AddRangeAsync(pizzaExtraModels);
        await _appDbContext.SaveChangesAsync();

        OrderReponse response = new OrderReponse()
        {
            InvoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss"),
            Message = "Thank you for your order! Enjoy your pizza!",
            TotalAmount = total,
        };

        return Ok(response);
    }
    //[HttpGet("Order/{invoiceNo}")]
    //public async Task<IActionResult> GetOrder(string invoiceNo)
    //{
    //    var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
    //    var lst = await _appDbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();

    //    return Ok(new
    //    {
    //        Order = item,
    //        OrderDetail = lst
    //    });
    //}
    [HttpGet("Order/{invoiceNo}")]
    public IActionResult GetOrder(string invoiceNo)
    {
        var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
            (
                PizzaQuery.PizzaOrderQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
            );

        var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
            (
                PizzaQuery.PizzaOrderDetailQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
            );

        var model = new PizzaOrderInvoiceResponse
        {
            Order = item,
            OrderDetail = lst
        };

        return Ok(model);
    }
}
