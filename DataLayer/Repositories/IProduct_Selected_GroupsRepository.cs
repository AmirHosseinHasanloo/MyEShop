using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IProduct_Selected_GroupsRepository
    {
        void UpdateSelectedGroups(Products products);
        void Save();
    }
}
