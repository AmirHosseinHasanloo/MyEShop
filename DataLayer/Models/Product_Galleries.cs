using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Galleries
    {
        [Key]
        public int GalleryID { get; set; }
        [Display(Name ="محصول")]
        public int ProductID { get; set; }
        [Display(Name ="تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Title { get; set; }
        public virtual Products Products { get; set; }
    }
}
