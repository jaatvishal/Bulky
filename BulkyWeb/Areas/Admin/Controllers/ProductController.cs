using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objCategoryList = _unitOfWork.Product.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Product? CategoryFromDb = _unitOfWork.Product.Get(u => u.id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
           
            if (ModelState.IsValid)
            {
               // _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");

            }


            return View();

        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
               // _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Category edit succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Product? CategoryFromDb = _unitOfWork.Product.Get(u => u.id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.id == id);


            if (obj == null)
            {
                return NotFound();
            }

           // _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["sucess"] = "Category Deleted succesfully";
            return RedirectToAction("Index");


        }
    }
}
