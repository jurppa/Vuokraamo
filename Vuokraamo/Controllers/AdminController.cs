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
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product, IFormFile imageUrl)
        {
            VarastoDBContext db = _context;
            var upload = @"~\wwwroot\images\";
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
    }
}
