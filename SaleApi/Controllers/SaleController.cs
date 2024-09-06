using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationTier.Interfaces;
using ApplicationTier.Classes;

namespace SaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        public readonly ISaleMethods _saleMethods;

        public SaleController(ISaleMethods saleMethods)
        {
         _saleMethods = saleMethods;
        }

        [HttpPost(Name = "AddSale")]
        public async Task<JsonResult> AddSale(int customerid,int productid, int storeid)
        {

            var sale = await _saleMethods.AddSale(customerid, productid, DateTime.Now, storeid);

            return new JsonResult(sale);
        }
    }
}
