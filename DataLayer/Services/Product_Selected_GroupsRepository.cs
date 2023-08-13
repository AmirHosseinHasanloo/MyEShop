using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Selected_GroupsRepository : IProduct_Selected_GroupsRepository
    {
        MyEshopContext _db = new MyEshopContext();

        public Product_Selected_GroupsRepository(MyEshopContext context)
        {
            this._db = context;
        }

        public void UpdateSelectedGroups(Products products)
        {
            _db.Product_Selected_Groups.Where(ps => ps.ProductID == products.ProductID).ToList().ForEach(ps => _db.Product_Selected_Groups.Remove(ps));
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
