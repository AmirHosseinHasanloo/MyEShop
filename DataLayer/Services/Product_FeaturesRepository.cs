using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_FeaturesRepository : IProduct_FeaturesRepository
    {
        private MyEshopContext db;

        public Product_FeaturesRepository(MyEshopContext _context)
        {
            this.db = _context;
        }

        public IEnumerable<ShowProductFeatureViewModel> Get_Features(int id)
        {
            var ProductPage = db.Products.Find(id);
            return ProductPage.Product_Features.DistinctBy(F => F.FeatureID).Select(F => new ShowProductFeatureViewModel()
            {
                FeatureTitle = F.Features.FeatureTitle,
                Values = db.Product_Features.Where(PF => PF.FeatureID == F.FeatureID && PF.ProductID == id).Select(PF => PF.Value).ToList()
            }).ToList();
        }
    }
}
