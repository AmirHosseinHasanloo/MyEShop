using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        MyEshopContext _context = new MyEshopContext();

        private GenericRepository<Users> _UsersRepository;

        public GenericRepository<Users> UsersRepository
        {
            get
            {
                if (_UsersRepository == null)
                {
                    _UsersRepository = new GenericRepository<Users>(_context);
                }
                return _UsersRepository;
            }
        }

        private GenericRepository<Slider> _SliderRepository;

        public GenericRepository<Slider> SliderRepository
        {
            get
            {
                if (_SliderRepository == null)
                {
                    _SliderRepository = new GenericRepository<Slider>(_context);
                }
                return _SliderRepository;
            }
        }

        private GenericRepository<SiteVisit> _SiteVisitRepository;

        public GenericRepository<SiteVisit> SiteVisitRepository
        {
            get
            {
                if (_SiteVisitRepository == null)
                {
                    _SiteVisitRepository = new GenericRepository<SiteVisit>(_context);
                }
                return _SiteVisitRepository;
            }
        }

        private GenericRepository<Roles> _RolesRepository;

        public GenericRepository<Roles> RolesRepository
        {
            get
            {
                if (_RolesRepository == null)
                {
                    _RolesRepository = new GenericRepository<Roles>(_context);
                }
                return _RolesRepository;
            }
        }


        private GenericRepository<Products> _ProductsRepository;

        public GenericRepository<Products> ProductsRepository
        {
            get
            {
                if (_ProductsRepository == null)
                {
                    _ProductsRepository = new GenericRepository<Products>(_context);
                }
                return _ProductsRepository;
            }
        }

        private GenericRepository<Product_Tags> _Product_TagsRepository;

        public GenericRepository<Product_Tags> Product_TagsRepository
        {
            get
            {
                if (_Product_TagsRepository == null)
                {
                    _Product_TagsRepository = new GenericRepository<Product_Tags>(_context);
                }
                return _Product_TagsRepository;
            }
        }

        private GenericRepository<Product_Selected_Groups> _Product_Selected_GroupsRepository;

        public GenericRepository<Product_Selected_Groups> Product_Selected_GroupsRepository
        {
            get
            {
                if (_Product_Selected_GroupsRepository == null)
                {
                    _Product_Selected_GroupsRepository = new GenericRepository<Product_Selected_Groups>(_context);
                }
                return _Product_Selected_GroupsRepository;
            }
        }
        private GenericRepository<Product_Groups> _Product_GroupsRepository;

        public GenericRepository<Product_Groups> Product_GroupsRepository
        {
            get
            {
                if (_Product_GroupsRepository == null)
                {
                    _Product_GroupsRepository = new GenericRepository<Product_Groups>(_context);
                }
                return _Product_GroupsRepository;
            }
        }

        private GenericRepository<Product_Galleries> _Product_GalleriesRepository;

        public GenericRepository<Product_Galleries> Product_GalleriesRepository
        {
            get
            {
                if (_Product_GalleriesRepository == null)
                {
                    _Product_GalleriesRepository = new GenericRepository<Product_Galleries>(_context);
                }
                return _Product_GalleriesRepository;
            }
        }

        private GenericRepository<Product_Features> _Product_FeaturesRepository;

        public GenericRepository<Product_Features> Product_FeaturesRepository
        {
            get
            {
                if (_Product_FeaturesRepository == null)
                {
                    _Product_FeaturesRepository = new GenericRepository<Product_Features>(_context);
                }
                return _Product_FeaturesRepository;
            }
        }

        private GenericRepository<Features> _Features_FeaturesRepository;

        public GenericRepository<Features> FeaturesRepository
        {
            get
            {
                if (_Features_FeaturesRepository == null)
                {
                    _Features_FeaturesRepository = new GenericRepository<Features>(_context);
                }
                return _Features_FeaturesRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
