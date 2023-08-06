using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MyEshopContext : DbContext
    {
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Product_Groups> Product_Groups { get; set; }
        public virtual DbSet<Product_Selected_Groups> Product_Selected_Groups { get; set; }
        public virtual DbSet<Product_Galleries> Product_Galleries { get; set; }
        public virtual DbSet<Product_Tags> Product_Tags { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Product_Features> Product_Features { get; set; }
        public virtual DbSet<Product_Comments> Product_Comments { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
    }
}
