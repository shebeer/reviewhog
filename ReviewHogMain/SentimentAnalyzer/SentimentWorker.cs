using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LinqToDB;

namespace SentimentAnalyzer
{
    public static class SentimentWorker
    {
        public static void DoWork()
        {
            //analyze all reviews, update db
            //calculate overall average when done

            UpdateReviewsAndEntities();
            CalculateAverageScores();
        }

        private static void UpdateReviewsAndEntities()
        {
            var db = new ReviewHogMainDB();
            var allReviews = db.ProductReviews.ToList();

            foreach (var review in allReviews)
            {
                if (!string.IsNullOrEmpty(review.ReviewText))
                {
                    SentimentResponseModel response = GoogNLCore.AnalyzeText(review.ReviewText);

                    db.InsertWithIdentity(new ReviewSentimental()
                    {
                        ProductId = review.Product.Id,
                        ReviewId = review.Id,
                        SentimentScore = response.SentimentScore
                    });

                    foreach (var entity in response.Entities)
                    {
                        //check if entity exists. if yes, update score and hit count
                        //otherwise add to db
                        if (db.ProductEntities.Any(x =>
                                x.ProductId == review.ProductId && x.EntityName.ToLower() == entity.Name.ToLower()))
                        {
                            var prodEntity = db.ProductEntities.FirstOrDefault(x => x.ProductId == review.ProductId && x.EntityName.ToLower() == entity.Name.ToLower());
                            if (prodEntity != null)
                            {
                                var totalScore = (prodEntity.EntityAvgSentiment * prodEntity.HitCount) +
                                                 entity.SentimentScore;
                                var averageScore = totalScore / (prodEntity.HitCount + 1);
                                var hitCount = prodEntity.HitCount + 1;
                                db.ProductEntities.Where(x =>
                                        x.ProductId == review.ProductId &&
                                        x.EntityName.ToLower() == entity.Name.ToLower())
                                    .Set(x => x.HitCount, hitCount)
                                    .Set(x => x.EntityAvgSentiment, averageScore)
                                    .Update();
                            }
                            else
                            {
                                throw new Exception("Unable to get entity from ProductEntity table");
                            }
                        }
                        else
                        {
                            db.InsertWithIdentity(new ProductEntity()
                            {
                                HitCount = 1,
                                EntityAvgSentiment = entity.SentimentScore,
                                EntityName = entity.Name.ToLower(),
                                ProductId = (long)review.ProductId,
                            });
                        }
                    }
                }
            }
        }

        private static void CalculateAverageScores()
        {

        }
    }
}
