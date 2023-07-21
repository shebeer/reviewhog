using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewHogAPI.Controllers
{
    public class ReviewHogController : ApiController
    {
        ReviewHogMainDB _db;
        public ReviewHogController()
        {
            _db= new ReviewHogMainDB();
        }
        
        [HttpGet]
        [Route("products")]
        public IEnumerable<ProductDetail> GetAllProducts()
        {
            return _db.ProductDetails.ToList();
        }

        [HttpGet]
        [Route("product/{productId}")]
        public List<ProductReview> GetProductReview(int productId)
        {
            return _db.ProductReviews.Where(x => x.ProductId == productId).ToList();
        }
    }
}
