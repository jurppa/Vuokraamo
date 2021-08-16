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
            _context = context;
        }

        [HttpGet]
        public IActionResult Rekisteröidy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Rekisteröidy(Customer a)    // Signup ja asiakkuuden hallinta
        {
            VarastoDBContext db = _context;
            var q = db.Customers;
            q.Add(a);
            db.SaveChanges();





            return View();
        }
    }
}
