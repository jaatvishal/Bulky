using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        
        public IProductRepository Product { get; private set; }

        public IProductImageRepository ProductImage { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository Shoppingcart { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db= db;
            ProductImage = new ProductImageRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company =new ComapanyRepository(_db);
            Shoppingcart = new ShoppingCartRepository(_db);
        }
       

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
