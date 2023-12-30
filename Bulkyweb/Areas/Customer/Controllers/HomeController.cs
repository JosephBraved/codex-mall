using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using BulkyDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            product.Category = category;
			return View(product);
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
