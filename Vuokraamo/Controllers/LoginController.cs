using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

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
        public void AdminLogin(string email, string password)
        {
            Console.WriteLine(email);
            Console.WriteLine(password);

        }
    }
}
