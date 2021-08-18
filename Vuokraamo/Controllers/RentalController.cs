using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class RentalController : Controller
    {
        private readonly VarastoDBContext _context;

        public RentalController(VarastoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult VuokrausTiedot()
        {
            VarastoDBContext db = _context;
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            List<Rental> Vuokrat = db.Rentals.Where(a => a.CustomerId == cstId).ToList();

            Vuokrat.ForEach(a => a.Product = db.Products
            .Where(b => b.Id == a.ProductId)
            .FirstOrDefault());

            return View(Vuokrat);
        }
        public IActionResult Osta()
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");

            VarastoDBContext db = _context;
            List<Cart> asiakkaanOstokset = db.Carts.Where(a => a.CustomerId == cstId).ToList();
            

            return View("VuokrausTiedot");
        }
    }
}
