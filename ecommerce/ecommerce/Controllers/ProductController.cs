using ecommerce.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}