using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shop.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shop.Models {
    
    public class ShopCart {
        private readonly AppDBContent content;
        public ShopCart(AppDBContent app) => content = app;

        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopItems { get; set;}

        public static ShopCart GetCart(IServiceProvider service) { 
            ISession session = service
                .GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = service.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(context) { ShopCartId = shopCartId };
        }
        
        public void AddToCart(Car car) {
            content.ShopCartItem.Add(new ShopCartItem {
                ShopCartId = ShopCartId,
                car = car,
                price = car.price
            });
            content.SaveChanges();
        }

        public List<ShopCartItem> GetShopItems() {
            return content.ShopCartItem.Where(c => 
                c.ShopCartId == ShopCartId).Include(s => s.car).ToList();
        }
    }
}
