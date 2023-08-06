using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Tags
    {
        [Key]
        public int TagID { get; set; }
        [Display(Name ="محصول")]
        public int ProductID { get; set; }
        [Display(Name ="کلمه کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Tag { get; set; }
        public virtual Products Products { get; set; }
    }
}
