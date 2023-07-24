using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewHogAPI.Models
{
    public class Amazon
    {
        public string link { get; set; }
        public double avgRating { get; set; }
        public int totalRating { get; set; }
    }

    public class Platforms
    {
        public Amazon amazon { get; set; }
        public Walmart walmart { get; set; }
        public Target target { get; set; }
        public Samsclub samsclub { get; set; }
    }

    public class ProductOverallDetails
    {
        public string title { get; set; }
        public double avgRating { get; set; }
        public int ratingScore { get; set; }
        public string sentiment { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string id { get; set; }
        public Platforms platforms { get; set; }
        public List<string> positiveKeywords { get; set; }
        public List<string> negativeKeywords { get; set; }
    }

    public class Samsclub
    {
        public string link { get; set; }
        public double avgRating { get; set; }
        public int totalRating { get; set; }
    }

    public class Target
    {
        public string link { get; set; }
        public double avgRating { get; set; }
        public int totalRating { get; set; }
    }

    public class Walmart
    {
        public string link { get; set; }
        public double avgRating { get; set; }
        public int totalRating { get; set; }
    }


}