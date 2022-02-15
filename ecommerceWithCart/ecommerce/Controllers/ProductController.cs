using ecommerce.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ecommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            AdvDotNetEntities db = new AdvDotNetEntities();
            var data = db.Products.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
            
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                AdvDotNetEntities db = new AdvDotNetEntities();
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            return View(p);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            AdvDotNetEntities db = new AdvDotNetEntities();
            var product = (from u in db.Products where u.id == id select u).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            AdvDotNetEntities db = new AdvDotNetEntities();
            var product = (from u in db.Products where u.id == p.id select u).FirstOrDefault();
            db.Entry(product).CurrentValues.SetValues(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Delete(int id)
        {
            AdvDotNetEntities db = new AdvDotNetEntities();
            var product = (from u in db.Products where u.id == id select u).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Buy(int id)
        {
            AdvDotNetEntities db = new AdvDotNetEntities();
            var product = (from u in db.Products where u.id == id select u).FirstOrDefault();

            if (Session["cart"] == null)
            {
                List<Product> cart = new List<Product>();
                cart.Add(product);
                string json = new JavaScriptSerializer().Serialize(cart);
                Session["cart"] = json;
            }
            else
            {
                string json = (string)Session["cart"];
                var d = new JavaScriptSerializer().Deserialize<List<Product>>(json);
                d.Add(product);
                string json2 = new JavaScriptSerializer().Serialize(d);
                Session["cart"] = json2;
            }
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Showcart()
        {
            if (Session["cart"] != null)
            {
                string json = (string)Session["cart"];
                var d = new JavaScriptSerializer().Deserialize<List<Product>>(json);
                return View(d);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
            
        }
    }
}