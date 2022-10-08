using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using OfficeWrapper;
using Logger;

namespace PriceUpdate
{
    public class CompareProducts
    {
        public static List<Product> GetDifferenceProductsPrice(IEnumerable<Product> newProducts, IEnumerable<Product> curProducts)
        {
            return newProducts.Except(curProducts).ToList();
        }

    }
}
