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
        private Dictionary<string, string> brandProductMapping = new Dictionary<string, string>
        {
            { "Gatorade","Gatorade Orange Sports Drink - 12pk/12 fl oz Bottles" },
            { "Red Bull","Red Bull Energy Drink - 12pk/8.4 fl oz Cans"},
            { "Monster Energy","Monster Energy Zero Ultra - 12pk/16 fl oz Cans"},
            { "Prime Hydration","Prime Hydration Ice Pop Sports Drink - 8pk/16.9 fl oz Bottles"}
        };

        public string JsonResponseFolderPath 
        {
            get 
            {
                string sPath = Assembly.GetExecutingAssembly().Location;
                return Path.Combine(Directory.GetParent(sPath).FullName, "Target\\Brands");
            }
        }
        public TargetBot()
        {
        }

        public void StartBot()
        {
            var folders = Directory.GetDirectories(JsonResponseFolderPath);

            foreach (var folder in folders)
            {
                DirectoryInfo directory = new DirectoryInfo(folder);
                var brandName = directory.Name;
                
                var files = Directory.GetFiles(directory.FullName, "*.json");

                var targetDetailResponse = GetTargetReviewResponse(files[0]);
                long id = InsertTargetDetailData(targetDetailResponse, brandName); //isert only a product detail

                foreach (var file in files)//insert reviewes
                {
                    var response = GetTargetReviewResponse(file);
                    InsertTargetReviewData(id, response);
                }
            }
        }

        private TargetResponseObj GetTargetReviewResponse(string filePath)
        {
            string jsonResponse = File.ReadAllText(filePath);
            var obj = JsonConvert.DeserializeObject<TargetResponseObj>(jsonResponse);
            return obj;
        }

        private long GetProductMappingId(string brandName)
        {
            var db = new ReviewHogMainDB();
            return db.ProductDetails.Where(x => x.BrandName == brandName).FirstOrDefault().ProductMappingId;
        }

        private long InsertTargetDetailData(TargetResponseObj response, string brandName)
        {
            var db = new ReviewHogMainDB();

            ProductDetail dt = new ProductDetail
            {
                Name = brandProductMapping[brandName],
                ProductMappingId = GetProductMappingId(brandName),
                Upc = response.reviews.results[0].Tcin,
                BrandName = brandName,
                SupermarketId = 51,
                StarRatingScore = Convert.ToDecimal(response.statistics.rating.average),
                TotalStarRatings = response.statistics.review_count
            };

            return db.InsertWithInt64Identity(dt);
        }

        private void InsertTargetReviewData(long productId, TargetResponseObj response)
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
