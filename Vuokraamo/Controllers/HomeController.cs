using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VarastoDBContext _context;

        public HomeController(ILogger<HomeController> logger, VarastoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            VarastoDBContext db = _context;
            var testi = db.Products.First();
            ViewBag.Data = testi.Name;
            return View();
        }
        
        public IActionResult ProductList(int number = 0)
        {
            //if(number < 0)
            //{ number = 0; }
            Console.WriteLine($"ID on: {0}", number);
            VarastoDBContext db = _context;
            ViewBag.number = number;
            int showAmount = 5;
            List<Product> products = db.Products.Skip(number * showAmount).Take(showAmount).ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            VarastoDBContext db = _context;
            Console.WriteLine(id);
            return View(db.Products.Where(a => a.Id == id).First());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
