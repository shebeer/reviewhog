using Google.Cloud.Language.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalyzer
{
    public class SentimentResponseModel
    {
        public class EntityModel
        {
            public string Name { get; set; }
            public float SentimentScore { get; set; }
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

        public void MapFromGoogleSentimentResponse(AnalyzeSentimentResponse sentimentResponse, AnalyzeEntitySentimentResponse entityResponse)
        {

        }
    }
}
