using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vuokraamo.Controllers
{
    public class LoginController : Controller
    {
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
