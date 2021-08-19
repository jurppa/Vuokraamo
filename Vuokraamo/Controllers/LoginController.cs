using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;
using Microsoft.AspNetCore.Http;


namespace Vuokraamo.Controllers
{
    public class LoginController : Controller
    {
        private readonly VarastoDBContext _context;

        public LoginController(VarastoDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult AdminLogin(string email, string password)
        {
            VarastoDBContext db = _context;
            Admin admin = db.Admins.Where(a => a.Email == email).FirstOrDefault();
            
            if(admin == null)
            {
                RedirectToAction("AdminLogin");
            }
            
            if(admin.Password == password)
            {
                HttpContext.Session.SetString("key", "admin");
                
                Console.WriteLine("kirjautuminen onnistui");
                return RedirectToAction("Index","Admin");
            }
            else
            {
                Console.WriteLine("kirjautuminen ei onnistunut");
                RedirectToAction("AdminLogin");
            }


            return RedirectToAction("AdminLogin");
        }
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(string email, string password)
        {
            VarastoDBContext db = _context;
            Customer customer = db.Customers.Where(a => a.Email == email).FirstOrDefault();
            if (customer != null )
            {
                if (customer.Password == password)
                {
                HttpContext.Session.SetString("ckey", "customer");
                HttpContext.Session.SetString("cname", customer.Name);
                HttpContext.Session.SetInt32("cid", customer.CustomerId);
                Console.WriteLine("kirjautuminen onnistui");
                return RedirectToAction("Index","Home");
                }
            
           }
                 
            else
            {
                Console.WriteLine("kirjautuminen ei onnistunut");
                RedirectToAction("UserLogin");
            }


            return RedirectToAction("UserLogin");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
