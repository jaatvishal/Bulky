using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _UnitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productsList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _UnitOfWork.Product.Get(u => u.id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoopingcart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoopingcart.ApplicationUserId=userid;

            ShoppingCart cartFromDb =_UnitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId==userid && u.ProductId==shoopingcart.ProductId);

            if (cartFromDb != null)
            {
                cartFromDb.Count += shoopingcart.Count;              
                _UnitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                _UnitOfWork.ShoppingCart.Add(shoopingcart);
            }           

            _UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
