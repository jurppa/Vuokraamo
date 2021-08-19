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
{                       // Ylläpitäjän toimintojen määrittelyt
    public class AdminController : Controller
    {
        private readonly VarastoDBContext _context;
        private IWebHostEnvironment webHostEnvironment;

                            // Tietokanta saadaan dependency injectionilla
        public AdminController(VarastoDBContext context, IWebHostEnvironment _env)
        {
            _context = context;
            webHostEnvironment = _env;
        }

        public IActionResult Index()
        {
            return View();
        }
                            //Tuotteen lisäysnäkymä admin oikeuksilla. Hakee sessiosta onko admin.
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
                            //Lisätään tietokantaan tuote, tallennetaan kuva
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
                            // Poistetaan tuote Id:n perusteella
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
                                // Tuotteen editointinäkymä
        public IActionResult Edit(int Id)
        {
            VarastoDBContext db = _context;
            Product editoitavaTuote = db.Products.Where(a => a.Id == Id).FirstOrDefault();

            return View(editoitavaTuote);
        }
                                // Tallennetaan päivitetty tuote
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
                                // Näyttää asikkaiden lähettämät viestit ad:lle
        [HttpGet]
        public IActionResult ShowMessage()
        {
            VarastoDBContext db = _context;
            string cstId = HttpContext.Session.GetString("key");
            if (cstId == "admin")
            {
            return View(db.Messages.ToList());
            }
            return RedirectToAction("index", "home");
        }
    }
}
