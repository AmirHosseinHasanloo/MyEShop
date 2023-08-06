using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDetails
    {
        [Key]
        public int DetailID { get; set; }
        [Display(Name ="مشخصات خرید")]
        public int OrderID { get; set; }
        [Display(Name = "محصول")]
        public int ProductID { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }

        public virtual Products Products { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
