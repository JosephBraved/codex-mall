using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using BulkyDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Bulkyweb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkcs _unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWorkcs unitOfWork)
        {
            _logger = logger;
            _unitofWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _unitofWork.Product.GetAll(includeproperties:"Category");
            return View(productsList);
        }
		public IActionResult Details(int? id)
		{
			Product product = _unitofWork.Product.Get(u => u.Id == id, includeproperties:"Category");
			Category category = _unitofWork.Category.Get(u => u.Id == product.CategoryId, includeproperties: "Category");
            ShoppingCart cart = new ShoppingCart
            {
                Product = product,
                count = 1,
                ProductId = product.Id,
            };
            product.Category = category;

			return View(cart);
		}
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {            
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitofWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId); 

            if(cartFromDb != null) {
                cartFromDb.count += shoppingCart.count;
                _unitofWork.ShoppingCart.update(cartFromDb);
            }
            else
            {
                shoppingCart.Id = 0;
                _unitofWork.ShoppingCart.Add(shoppingCart);
            }

            TempData["success"] = "Cart updated successfully";
            _unitofWork.Save();            
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
