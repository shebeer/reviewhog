using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewHogInventoryBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new ReviewHogMainDB();
            var listProduct = db.ProductDetails.ToList();
        }
    }
}
