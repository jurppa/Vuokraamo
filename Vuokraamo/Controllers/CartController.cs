using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class CartController : Controller
    {
        private readonly VarastoDBContext _context;

        public CartController(VarastoDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            Cart customersCart = _context.Carts.Where(a => a.CustomerId == cstId).First();

            return View();
        }
    }
}
