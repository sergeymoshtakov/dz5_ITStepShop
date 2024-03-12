using ITStepShop.Data;
using ITStepShop.Models.ViewModel;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;
using ITStepShop.Utility;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ITStepShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult Index(List<int> productIds)
        {
            if (productIds != null && productIds.Any())
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Отримати ідентифікатор користувача

                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                };

                _db.Orders.Add(order);
                _db.SaveChanges();

                HttpContext.Session.Remove(WC.SessionCart);

                return RedirectToAction("Index", "Home"); 
            }
            return View(); 
        }
        public IActionResult Remove(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            shopingCartList.Remove(shopingCartList.FirstOrDefault(x => x.ProductId==id));
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }
    }
}
