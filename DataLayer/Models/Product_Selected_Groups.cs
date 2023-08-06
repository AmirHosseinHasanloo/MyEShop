using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product_Selected_Groups
    {
        [Key]
        public int PG_ID { get; set; }
        [Display(Name ="محصول")]
        public int ProductID { get; set; }
        [Display(Name ="گروه کالا")]
        public int GroupID { get; set; }

        public virtual Product_Groups Product_Groups { get; set; }
        public virtual Products Products { get; set; }
    }
}
