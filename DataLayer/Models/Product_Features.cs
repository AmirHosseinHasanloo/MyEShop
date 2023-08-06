using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Features
    {
        [Key]
        public int PF_ID { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int ProductID { get; set; }
        [Display(Name ="ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int FeatureID { get; set; }
        [Display(Name = "مقدار ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Value { get; set; }

        public virtual Features Features { get; set; }
        public virtual Products Products { get; set; }
    }
}
