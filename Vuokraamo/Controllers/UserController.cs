using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class UserController : Controller 
    {
  
        private readonly ILogger<UserController> _logger;       
        private readonly VarastoDBContext _context;

        public UserController(ILogger<UserController> logger, VarastoDBContext context)
        {
            _logger = logger;
            _context = context;                                                                 // Connection stringin piilotuksesta tulleita.
        }

        [HttpGet]
        public IActionResult Rekisteröidy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Rekisteröidy(Customer Create)    // Tässä Controllerissa voidaan luoda uusi käyttäjä. 
        {
            VarastoDBContext db = _context;
            var q = db.Customers;
            db.Customers.Add(Create);
            db.SaveChanges();





            return View();
      
       }
        [HttpGet]
        public IActionResult AsiakasTiedot(int id)
        {
            VarastoDBContext db = _context;
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            return View(db.Customers.Where(a => a.CustomerId == cstId).FirstOrDefault());
        }

        public IActionResult Asiakas()
        {
            return View();
        }

    }
}
