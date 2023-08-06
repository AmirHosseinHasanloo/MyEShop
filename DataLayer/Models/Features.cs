using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Features
    {
        public Features()
        {
            this.Product_Features = new HashSet<Product_Features>();
        }
        [Key]
        public int FeatureID { get; set; }
        [Display(Name ="عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string FeatureTitle { get; set; }

        public virtual ICollection<Product_Features> Product_Features { get; set; }
    }
}
