namespace ReviewHogAPI.Model
{
    public class ProductSentimentResult
    {
        public string product_id { get; set; }
        public string Title { get; set; }
        public string UPC { get; set; }
        public string ImageUrl { get; set;}
        public double AvgRating { get; set; }
    }
}
