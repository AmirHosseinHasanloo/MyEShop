using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductTagsRepository : IProductTagsRepository
    {
        MyEshopContext _db = new MyEshopContext();

        public ProductTagsRepository(MyEshopContext context)
        {
            this._db = context;
        }
        public void UpdateTags(Products products)
        {
            _db.Product_Tags.Where(t => t.ProductID == products.ProductID).ToList().ForEach(t => _db.Product_Tags.Remove(t));
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
