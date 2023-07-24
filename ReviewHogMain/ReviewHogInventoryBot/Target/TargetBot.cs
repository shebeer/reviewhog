using DataModels;
using LinqToDB;
using Newtonsoft.Json;
using ReviewHogInventoryBot.Target.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReviewHogInventoryBot.Target
{
    public class TargetBot
    {
        public string JsonResponseFilePath 
        {
            get 
            {
                string sPath = Assembly.GetExecutingAssembly().Location;
                return Path.Combine(Directory.GetParent(sPath).FullName, "Target\\Target_Review_Response.json");
            }
        }
        public TargetBot()
        {
        }

        public void StartBot()
        {
            var response = GetTargetReviewResponse();
            long id = InsertTargetDetailData(response);
            InsertTargetReviewData(id, response);
        }

        internal TargetResponseObj GetTargetReviewResponse()
        {
            //Todo get response from API call
            string filePath = JsonResponseFilePath;

            string jsonResponse = File.ReadAllText(filePath);
            var obj = JsonConvert.DeserializeObject<TargetResponseObj>(jsonResponse);
            return obj;
        }

        internal long InsertTargetDetailData(TargetResponseObj response)
        {
            var db = new ReviewHogMainDB();

            ProductDetail dt = new ProductDetail
            {
                Name = "Gatorade Orange Sports Drink - 12pk/12 fl oz Bottles",
                Upc = "",
                BrandName = string.Empty,
                SupermarketId = 51,
                StarRatingScore = Convert.ToDecimal(response.statistics.rating.average),
                TotalStarRatings = response.statistics.review_count
            };

            return db.InsertWithInt64Identity(dt);
        }

        internal void InsertTargetReviewData(long productId, TargetResponseObj response)
        {
            var db = new ReviewHogMainDB();

            foreach (var review in response.reviews.results)
            {
                ProductReview prodReview = new ProductReview
                {
                    ProductId = productId,
                    ReviewText = review.text,
                    ReviewerName = review.author.nickname,
                    ReviewerLocation = "N/A",
                    ReviewDate = review.submitted_at
                };

                db.InsertWithInt64Identity(prodReview);
            }
        }
    }
}
