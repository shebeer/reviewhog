using DataModels;
using LinqToDB;
using System;
using System.Linq;

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
                        ProductId = review.ProductId,
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
            var db = new ReviewHogMainDB();

            var mappingIds = db.ProductDetails.Select(x => x.ProductMappingId).Distinct().ToList();
            foreach (var mappingId in mappingIds)
            {
                var productIds = db.ProductDetails.Where(x => x.ProductMappingId == mappingId).Select(x => x.Id)
                    .ToList();

                float sentimentScore = 0;
                int count = 0;
                float totalRating = 0;
                long ratingCount = 0;
                foreach (var productId in productIds)
                {
                    sentimentScore+= db.ReviewSentimentals.Where(x => x.ProductId == productId).Sum(x => x.SentimentScore);
                    count += db.ReviewSentimentals.Count(x => x.ProductId == productId);
                    var rating = (float)db.ProductDetails.Where(x => x.Id == productId).Select(x => x.StarRatingScore).First();
                    var rCount = (float)db.ProductDetails.Where(x => x.Id == productId).Select(x => x.TotalStarRatings).First();
                    totalRating += rating * rCount;
                    ratingCount += (long)rCount;
                }

                var sentimentScoreAvg = sentimentScore / count;
                string sentiment = "Neutral";
                if (sentimentScoreAvg > 0)
                {
                    sentiment = "Positive";
                }
                else if (sentimentScoreAvg < 0)
                {
                    sentiment = "Negative";
                }

                db.InsertWithIdentity(new ProductSentiment()
                {
                    ProductMappingId = mappingId,
                    SentimentScoreAvg = sentimentScoreAvg,
                    RatingCount = ratingCount,
                    RatingScoreAvg = (totalRating/ratingCount),
                    Sentiment = sentiment
                });
            }
        }
    }
}
