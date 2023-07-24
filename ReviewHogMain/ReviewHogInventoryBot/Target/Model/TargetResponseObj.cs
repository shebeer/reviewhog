using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewHogInventoryBot.Target.Model
{
    public class TargetResponseObj
    {
        public Statistics statistics { get; set; }
        public Reviews reviews { get; set; }
        public ReviewsWithPhotos reviews_with_photos { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Author
    {
        public string external_id { get; set; }
        public string nickname { get; set; }
    }

    public class Badges
    {
        public VerifiedPurchaser verifiedPurchaser { get; set; }
    }

    public class Distribution
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("2")]
        public int _2 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("4")]
        public int _4 { get; set; }

        [JsonProperty("5")]
        public int _5 { get; set; }
    }

    public class Feedback
    {
        public int helpful { get; set; }
        public int unhelpful { get; set; }
        public int inappropriate { get; set; }
    }

    public class Metadata
    {
        public bool has_verified { get; set; }
        public bool has_photos { get; set; }
        public bool has_entities { get; set; }
    }

    public class Quality
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public string Label { get; set; }
        public int ValueRange { get; set; }
        public string DisplayType { get; set; }
    }

    public class Rating
    {
        public Distribution distribution { get; set; }
        public double average { get; set; }
        public int positive_percentage { get; set; }
        public int count { get; set; }
        public List<SecondaryAverage> secondary_averages { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string external_id { get; set; }
        public bool is_syndicated { get; set; }
        public string channel { get; set; }
        public Author author { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public bool is_verified { get; set; }
        public bool is_recommended { get; set; }
        public Feedback feedback { get; set; }
        public string status { get; set; }
        public DateTime submitted_at { get; set; }
        public DateTime modified_at { get; set; }
        public List<object> photos { get; set; }
        public List<object> tags { get; set; }
        public List<object> reviewer_attributes { get; set; }
        public List<object> ClientResponses { get; set; }
        public List<object> Entities { get; set; }
        public string Tcin { get; set; }
        public int Rating { get; set; }
        public int RatingRange { get; set; }
        public List<string> SecondaryRatingsOrder { get; set; }
        public SecondaryRatings SecondaryRatings { get; set; }
        public List<string> BadgesOrder { get; set; }
        public Badges Badges { get; set; }
        public string SourceClient { get; set; }
        public bool IsRatingsOnly { get; set; }
        public int ClientResponseCount { get; set; }
    }

    public class Reviews
    {
        public int page { get; set; }
        public int size { get; set; }
        public int total_results { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public bool first_page { get; set; }
        public bool last_page { get; set; }
    }

    public class ReviewsWithPhotos
    {
        public int page { get; set; }
        public int size { get; set; }
        public int total_results { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public bool first_page { get; set; }
        public bool last_page { get; set; }
    }

    

    public class SecondaryAverage
    {
        public string id { get; set; }
        public double value { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public int range { get; set; }
    }

    public class SecondaryRatings
    {
        public Value Value { get; set; }
        public Quality Quality { get; set; }
        public Taste Taste { get; set; }
    }

    public class Statistics
    {
        public int review_count { get; set; }
        public int question_count { get; set; }
        public int recommended_count { get; set; }
        public int not_recommended_count { get; set; }
        public int recommended_percentage { get; set; }
        public int reviews_with_images_count { get; set; }
        public Rating rating { get; set; }
        public DateTime calculated_at { get; set; }
    }

    public class Taste
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public string Label { get; set; }
        public int ValueRange { get; set; }
        public string DisplayType { get; set; }
    }

    public class Value
    {
        public string Id { get; set; }
        [JsonProperty("Value")]
        public int Value1 { get; set; }
        public string Label { get; set; }
        public int ValueRange { get; set; }
        public string DisplayType { get; set; }
    }

    public class VerifiedPurchaser
    {
        public string Id { get; set; }
        public string ContentType { get; set; }
        public string BadgeType { get; set; }
    }


}
