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


        public IActionResult Index()    // Signup ja asiakkuuden hallinta
        {
            VarastoDBContext db = _context;


            return View();
        }
    }
}
