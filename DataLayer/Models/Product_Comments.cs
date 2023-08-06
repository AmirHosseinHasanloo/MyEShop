using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Comments
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name = "محصول")]
        public int ProductID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نام کاربری خودتان را وارد کنید")]
        [MaxLength(150, ErrorMessage = "نام کاربری شما نمی تواند بیش از 150 کرکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ایمیل خودتان را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل آدرس معتبر وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "وب سایت")]
        public string WebSite { get; set; }
        [Display(Name = "نظر شما")]
        [Required(ErrorMessage = "کاربر گرامی این فیلد نمی تواند خالی باشد")]
        [MaxLength(800, ErrorMessage = " نظر شما نمی تواند بیش از 800 کرکتر باشد")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        public int? ParentID { get; set; }

        public virtual Products Products { get; set; }

        public virtual List<Product_Comments> Product_Comments1 { get; set; }
        public virtual Product_Comments Product_Comments2 { get; set; }
    }
}
