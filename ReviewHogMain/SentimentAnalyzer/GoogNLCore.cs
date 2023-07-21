using Google.Api.Gax.Grpc;
using Google.Cloud.Language.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalyzer
{
    internal static class GoogNLCore
    {
        public static SentimentResponseModel AnalyzeText(string text)
        {
            LanguageServiceSettings settings = new LanguageServiceSettings
            {
                //TODO: Need to enter a key to make the API calls
                CallSettings = CallSettings.FromHeader("X-Goog-Api-Key", "")
            };

            LanguageServiceClient client = new LanguageServiceClientBuilder
            {
                ChannelCredentials = ChannelCredentials.SecureSsl,
                Settings = settings
            }.Build();

            Document document = Document.FromPlainText(text);

            AnnotateTextResponse senResponse = client.AnnotateText(document,
                new AnnotateTextRequest.Types.Features
                    { ExtractDocumentSentiment = true, ExtractEntities = true, ExtractEntitySentiment = true });

            SentimentResponseModel result = new SentimentResponseModel(senResponse);
            
            return result;
        }
    }
}
