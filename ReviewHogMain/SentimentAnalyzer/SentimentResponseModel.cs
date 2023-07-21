using Google.Cloud.Language.V1;
using System.Collections.Generic;

namespace SentimentAnalyzer
{
    public class SentimentResponseModel
    {
        public class EntityModel
        {
            public string Name { get; set; }
            public float SentimentScore { get; set; }
            public float Salience { get; set; }
        }

        public float SentimentScore { get; }
        public float SentimentMagnitude { get; }
        public Sentiment OverallSentiment { get; }
        public List<EntityModel> Entities { get; }

        public enum Sentiment
        {
            Negative = -1,
            Neutral = 0,
            Positive = 1
        }

        public SentimentResponseModel(AnnotateTextResponse sentimentResponse)
        {
            SentimentScore = sentimentResponse.DocumentSentiment.Score;
            SentimentMagnitude = sentimentResponse.DocumentSentiment.Magnitude;
            if (SentimentScore > 0)
            {
                OverallSentiment = Sentiment.Positive;
            } else if (SentimentScore < 0)
            {
                OverallSentiment = Sentiment.Negative;
            }
            else
            {
                OverallSentiment = Sentiment.Neutral;
            }

            Entities = new List<EntityModel>();
            foreach (var entity in sentimentResponse.Entities)
            {
                Entities.Add(new EntityModel()
                {
                    Name = entity.Name,
                    SentimentScore = entity.Sentiment.Score,
                    Salience = entity.Salience
                });
            }
        }
    }
}
