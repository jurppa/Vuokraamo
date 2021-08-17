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

        public AdminController(VarastoDBContext context)
        {
            _context = context;
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
            string filePath = Path.Combine(upload, fileName);
            
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
