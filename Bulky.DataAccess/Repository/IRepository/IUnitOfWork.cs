using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public  interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICompanyRepository Company { get; }

        IProductRepository Product { get; }

        IProductImageRepository ProductImage { get; }

        IShoppingCartRepository ShoppingCart { get; }
          
        IApplicationUserRepository ApplicationUser { get; } 
        public void Save()
        {
            
        }
        
    }
}
