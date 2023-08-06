using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [Display(Name = "عنوان کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Title { get; set; }
        [Display(Name = "توضیحات کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(500, ErrorMessage = "توضیحات کالای شما نمی تواند بیش از 500 کارامتر باشد")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [Display(Name = "متن کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }
        [Display(Name = "قیمت کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int Price { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ اییجاد کالا")]
        public System.DateTime CreateDate { get; set; }

        public virtual Product_Groups Product_Group { get; set; }
        public virtual List<Product_Tags> Product_Tags { get; set; }

        public virtual List<Product_Selected_Groups> Prodct_Selected_Groups { get; set; }

        public virtual List<Product_Galleries> Product_Galleries { get; set; }

        public virtual List<Product_Features> Product_Features { get; set; }
        public virtual List<Product_Comments> Product_Comments { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
