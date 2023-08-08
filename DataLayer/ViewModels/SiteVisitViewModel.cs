using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SiteVisitViewModel
    {
        public int VisitToDay { get; set; }
        public int VisitYesterday { get; set; }
        public int VisitInLastMonth { get; set; }
        public int VisitInLastYear { get; set; }
        public int VisitSum { get; set; }
    }
}
