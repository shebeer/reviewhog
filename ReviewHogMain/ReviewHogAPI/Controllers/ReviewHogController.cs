using DataModels;
using LinqToDB;
using ReviewHogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ReviewHogAPI.Controllers
{

    public enum SupermarketEnum : long
    {
        Amazon = 4,
        Walmart = 3,
        Target = 51,
        Samsclub = 105,
    }

    public class ReviewHogController : ApiController
    {
        ReviewHogMainDB _db;
        public ReviewHogController()
        {
            _db = new ReviewHogMainDB();
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

        private Amazon GetAmazonRating(long product_mapping_id)
        {
            var result = _db.ProductDetails
                .Where(x => x.ProductMappingId == product_mapping_id && x.SupermarketId == (long)SupermarketEnum.Amazon)
                .FirstOrDefault();

            return new Amazon 
            {
                avgRating = Convert.ToDouble(result.StarRatingScore),
                totalRating = Convert.ToInt32(result.TotalStarRatings),
                link = result.ImageUrl
            };
        }

        private Walmart GetWalmartRating(long product_mapping_id)
        {
            var result = _db.ProductDetails
                .Where(x => x.ProductMappingId == product_mapping_id && x.SupermarketId == (long)SupermarketEnum.Walmart)
                .FirstOrDefault();

            return new Walmart
            {
                avgRating = Convert.ToDouble(result.StarRatingScore),
                totalRating = Convert.ToInt32(result.TotalStarRatings),
                link = result.ImageUrl
            };
        }

        private Target GetTargetRating(long product_mapping_id)
        {
            var result = _db.ProductDetails
                .Where(x => x.ProductMappingId == product_mapping_id && x.SupermarketId == (long)SupermarketEnum.Target)
                .FirstOrDefault();

            return new Target
            {
                avgRating = Convert.ToDouble(result.StarRatingScore),
                totalRating = Convert.ToInt32(result.TotalStarRatings),
                link = result.ImageUrl
            };
        }

        private Samsclub GetSamsclubRating(long product_mapping_id)
        {
            var result = _db.ProductDetails
                .Where(x => x.ProductMappingId == product_mapping_id && x.SupermarketId == (long)SupermarketEnum.Samsclub)
                .FirstOrDefault();

            if (result != null)
            {
                return new Samsclub
                {
                    avgRating = Convert.ToDouble(result.StarRatingScore),
                    totalRating = Convert.ToInt32(result.TotalStarRatings),
                    link = result.ImageUrl
                };
            }

            return null;
        }

        [HttpGet]
        [Route("productdata")]
        public List<ProductOverallDetails> GetOverallData()
        {
            List<ProductOverallDetails> prodList = new List<ProductOverallDetails>();

            List<string> completedRetailer = new List<string>();

            foreach (var productDetail in _db.ProductDetails.ToList())
            {
                if (completedRetailer.Contains($"{productDetail.BrandName}-{productDetail.SupermarketId}"))
                {
                    continue;
                }

                long prodId = productDetail.Id;
                ProductOverallDetails pd = new ProductOverallDetails();

                pd.id = productDetail.Upc;
                pd.title = productDetail.Name;
                pd.description = productDetail.BrandName;
                pd.platforms = new Platforms
                {
                    amazon = GetAmazonRating(productDetail.ProductMappingId),
                    walmart = GetWalmartRating(productDetail.ProductMappingId),
                    target = GetTargetRating(productDetail.ProductMappingId),
                    samsclub = GetSamsclubRating(productDetail.ProductMappingId)
                };

                var sentiments = _db.ProductSentiments.Where(x => x.ProductMappingId == productDetail.ProductMappingId).FirstOrDefault();

                pd.avgRating = Convert.ToDouble(sentiments.RatingScoreAvg);
                pd.ratingScore = sentiments.SentimentScoreAvg.Value;
                pd.sentiment = sentiments.Sentiment;

                var positiveEntity = _db.ProductEntities.Where(x => x.EntityAvgSentiment > 0 && x.ProductId == prodId)
                                        .OrderByDescending(x => x.HitCount).Take(5).Select(x => x.EntityName);

                var negativeEntity = _db.ProductEntities.Where(x => x.EntityAvgSentiment < 0 && x.ProductId == prodId)
                                        .OrderByDescending(x => x.HitCount).Take(5).Select(x => x.EntityName);

                pd.positiveKeywords = positiveEntity.ToList();
                pd.negativeKeywords = negativeEntity.ToList();

                prodList.Add(pd);
                completedRetailer.Add($"{productDetail.BrandName}-{productDetail.SupermarketId}");
            }

            return prodList;
        }
    }
}
