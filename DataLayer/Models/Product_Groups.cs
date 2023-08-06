using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class Product_Groups
    {
        [Key]
        public int GroupID { get; set; }
        [Display(Name = "گروه محصولات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        public int? ParentID { get; set; }

        public virtual List<Products> Products { get; set; }
        public virtual List<Product_Groups> Product_Groups1 { get; set; }
        public virtual Product_Groups Product_Groups2 { get; set; }
        public virtual List<Product_Selected_Groups> Product_Selected_Groups { get; set; }
    }
}
