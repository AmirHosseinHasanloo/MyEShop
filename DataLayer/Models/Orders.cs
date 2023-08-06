using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        [Display(Name = "مشتری/کاربر")]
        public int UserID { get; set; }
        [Display(Name = "تاریخ ثبت فاکتور")]
        public DateTime Date { get; set; }
        [Display(Name = "پرداخت نهایی")]
        public bool IsFinally { get; set; }

        public virtual Users User { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
