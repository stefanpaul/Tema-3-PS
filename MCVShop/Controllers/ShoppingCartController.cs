using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using MCVShop.Models;
using System.Net;

namespace MCVShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private List<Models.Product> products = new List<Models.Product>();
        private ProductDBContext db = new ProductDBContext();
        private static int firstTime =0;
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            if(firstTime == 1){
                products = Session["Products"] as List<MCVShop.Models.Product>; 
            }
            firstTime = 1;
            Product product = db.Movies.Find(id);
            products.Add(product);
            Session["Products"] = products;
            return View(products);
        }

        public ActionResult RemoveFromCart(int id)
        {
            int index = 0;
            products = Session["Products"] as List<MCVShop.Models.Product>;
            foreach(MCVShop.Models.Product item in products)
            {
                if (item.ID == id)
                {
                    products.RemoveAt(index);
                    break;
                }
                index++;
            }
            Session["Products"] = products;
            return View(products);
        }

        public ActionResult ViewCart()
        {
            if (firstTime == 1)
            {
                products = Session["Products"] as List<MCVShop.Models.Product>;
                return View(products);
            }
            else Response.Redirect("../Products/Index");
            return null;
        }
    }
}