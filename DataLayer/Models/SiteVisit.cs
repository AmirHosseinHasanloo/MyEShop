using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SiteVisit
    {
        [Key]
        public int VisitID { get; set; }
        [MaxLength(150)]
        public string IP { get; set; }
        public DateTime VisitDate { get; set; }

    }
}
