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





            return RedirectToAction("UserLogin", "Login");

        }
        [HttpGet]
        public IActionResult AsiakasTiedot()
        {

            VarastoDBContext db = _context;
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            return View(db.Customers.Where(a => a.CustomerId == cstId).FirstOrDefault());
        }

        public IActionResult Asiakas()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult AsiakasTiedot(Customer customer)
        {
            VarastoDBContext db = _context;
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            db.Customers.Update(customer);
            db.SaveChanges();

            return RedirectToAction("Asiakastiedot", customer.CustomerId);
        }
        [HttpGet]
        public IActionResult Laskut()
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");

            VarastoDBContext db = _context;
            List<Invoice> asiakkaanLaskut = db.Invoices.Where(a => a.CustomerId == cstId).OrderBy(a => a.Paid).ThenBy(a => a.DueDate).ToList();
            return View(asiakkaanLaskut);
        }
        public IActionResult Maksa(int id)
        {
            VarastoDBContext db = _context;
            Invoice invoice = db.Invoices.Where(a => a.InvoiceId == id).FirstOrDefault();
            invoice.Paid = true;
            db.Invoices.Update(invoice);
            db.SaveChanges();

            return RedirectToAction("Laskut");

        }

    }
}
