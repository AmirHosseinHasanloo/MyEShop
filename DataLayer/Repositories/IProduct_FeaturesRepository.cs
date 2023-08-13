using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IProduct_FeaturesRepository
    {
        IEnumerable<ShowProductFeatureViewModel> Get_Features(int id);
    }
}
