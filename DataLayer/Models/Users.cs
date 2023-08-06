using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Display(Name = "عنوان نقش")]
        public int RoleID { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت حساب")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ فعالسازی")]
        public DateTime RegisterDate { get; set; }

        public virtual List<Orders> Orders { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
