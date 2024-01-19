using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Bulkyweb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWorkcs _unitOfWorkcs;
        public ShoppingCartVM shoppingcartvm{ get; set; }
        public CartController(IUnitOfWorkcs unitOfWork) {

            _unitOfWorkcs = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingcartvm = new() {
                CartList = _unitOfWorkcs.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeproperties: "Product"),
            };
            foreach(var cart  in shoppingcartvm.CartList)
            {
               cart.Price =  Getpricebasedonquantity(cart);
                shoppingcartvm.OrderTotal += (cart.Price * cart.count);
            }
            return View(shoppingcartvm);
        }
        private double Getpricebasedonquantity(ShoppingCart shoppingcart)
        {
            if (shoppingcart.count <= 50)
            {                
                return shoppingcart.Product.Price;
            }
            else if(shoppingcart.count <= 100)
            {
                return shoppingcart.Product.Price50;
            }
            else
            {
                return shoppingcart.Product.Price100;
            }

        }
    }
}
