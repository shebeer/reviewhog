using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewHogAPI.Controllers
{
    [ApiController]
    public class ReviewhogController : ControllerBase
    {
        [HttpGet]
        [Route("all_products")]
        public async Task<List<ProductDetail>> GetProductDetails()
        {
            await using var db = new ReviewHogMainDB();
            return await db.ProductDetails.ToListAsync();
        }

        [HttpGet]
        [Route("get-product")]
        public async Task<ProductDetail> GetProductDetails(int productId = 1)
        {
            await using var db = new ReviewHogMainDB();
            return await db.ProductDetails.Where(x => x.Id  == productId).FirstAsync();
        }
    }
}
