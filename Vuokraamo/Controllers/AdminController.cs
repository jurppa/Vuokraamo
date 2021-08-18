using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class AdminController : Controller
    {
        private readonly VarastoDBContext _context;
        private IWebHostEnvironment webHostEnvironment;


        public AdminController(VarastoDBContext context, IWebHostEnvironment _env)
        {
            _context = context;
            webHostEnvironment = _env;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            string cstId = HttpContext.Session.GetString("key");
            if (cstId == "admin")
            {
            return View();
            }
            return RedirectToAction("index", "home");


        }
        [HttpPost]
        public IActionResult AddProduct(Product product, IFormFile imageUrl)
        {            
            VarastoDBContext db = _context;
            var fileName = imageUrl.FileName;
            product.ImageUrl = fileName;
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageUrl.CopyTo(fileStream);
            }
            db.Products.Add(product);
            db.SaveChanges();

            return View("Index");
        }
        [HttpGet]
        public IActionResult DeleteProduct(int Id)
        {
            Console.WriteLine("delete product :"+ Id);
            VarastoDBContext db = _context;
            Product productToRemove = db.Products.Where(a => a.Id == Id).FirstOrDefault();
            Console.WriteLine(productToRemove.Name);
            db.Products.Remove(productToRemove);
            db.SaveChanges();
            return RedirectToAction("ProductList", "Home");
        }
        public IActionResult Edit(int Id)
        {
            VarastoDBContext db = _context;
            Product editoitavaTuote = db.Products.Where(a => a.Id == Id).FirstOrDefault();

            return View(editoitavaTuote);
        }
        [HttpPost]
        public IActionResult Edit(Product product, IFormFile imageUrl)
        {
            VarastoDBContext db = _context;
            var fileName = imageUrl.FileName;
            product.ImageUrl = fileName;
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageUrl.CopyTo(fileStream);
            }
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("ProductList", "Home");

        }
    }
}
