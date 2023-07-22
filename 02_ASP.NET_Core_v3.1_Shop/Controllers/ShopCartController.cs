using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.Repository;
using Shop.ViewModels;
using System.Linq;

namespace Shop.Controllers {

    public class ShopCartController : Controller {
        
        private IAllCars _carRep;
        private readonly ShopCart _shopCart;

        public ShopCartController(CarRepository car, ShopCart shop) {
            _carRep = car;
            _shopCart = shop;
        }

        public ViewResult Index() {
            var items = _shopCart.GetShopItems();
            _shopCart.listShopItems = items;

            var obj = new ShopCartViewModel {
                shopCart = _shopCart,
            };
            return View(obj);
        }

        public RedirectToActionResult addToCart(int id) {
            var item = _carRep.Cars.FirstOrDefault(i =>  i.Id == id);
            if (item == null) {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }
    }
}