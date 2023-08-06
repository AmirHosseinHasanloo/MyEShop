using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        [Display(Name = "عنوان نقش")]
        public string RoleTitle { get; set; }
        [Display(Name = "عنوان سیستمی نقش")]
        public string RoleName { get; set; }

        public virtual List<Users> Users { get; set; }
    }
}
