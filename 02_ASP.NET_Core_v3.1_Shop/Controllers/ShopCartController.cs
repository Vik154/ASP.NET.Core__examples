using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repository;

namespace Shop.Controllers {

    public class ShopCartController {
        
        private readonly CarRepository _carRep;
        private readonly ShopCart _shopCart;

        public ShopCartController(CarRepository car, ShopCart shop) {
            _carRep = car;
            _shopCart = shop;
        }

        public ViewResult Index() {
            var items = _shopCart.GetShopItems();
            _shopCart.listShopItems = items;
        }

    }
}
