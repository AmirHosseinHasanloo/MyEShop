using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductRepository : IProductRepository
    {
        MyEshopContext db = new MyEshopContext();

        public ProductRepository(MyEshopContext _context)
        {
            this.db = _context;
        }

        public IEnumerable<Products> LastProducts()
        {
            return db.Products.OrderByDescending(P => P.CreateDate).Take(12).ToList();
        }
    }
}
