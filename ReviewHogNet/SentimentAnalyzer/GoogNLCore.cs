using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Cloud.Language.V1;
using Grpc.Core;

namespace SentimentAnalyzer
{
    public static class GoogNLCore
    {
        public static SentimentResponseModel AnalyzeText(string text)
        {
            LanguageServiceSettings settings = new LanguageServiceSettings
            {
                //TODO: Need to enter a key to make the API calls
                CallSettings = CallSettings.FromHeader("X-Goog-Api-Key", "AIzaSyAm7yi5ZyyZdt8_rBKNRd63VE9dP4Nvh6o")
            };

            LanguageServiceClient client = new LanguageServiceClientBuilder
            {
                ChannelCredentials = ChannelCredentials.SecureSsl,
                Settings = settings
            }.Build();

            Document document = Document.FromPlainText(text);
            AnalyzeSentimentResponse response = client.AnalyzeSentiment(document);
            AnalyzeEntitySentimentRequest entityRequest = new AnalyzeEntitySentimentRequest()
            {
                Document = document
            };
            AnalyzeEntitySentimentResponse entityResponse = client.AnalyzeEntitySentiment(entityRequest);

            SentimentResponseModel result = new SentimentResponseModel();
            result.MapFromGoogleSentimentResponse(response, entityResponse);

            return result;
        }
    }
}
