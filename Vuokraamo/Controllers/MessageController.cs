using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class MessageController : Controller
    {
    VarastoDBContext _context;

        public MessageController(VarastoDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Id = (int)HttpContext.Session.GetInt32("cid");

            return View();
        }

                            // asiakaspalautteen jättäminen
        [HttpPost]
        public IActionResult Post(string aihe, string viesti, int id)
        {
            VarastoDBContext db = _context;
            Message message = new Message();
            message.CustomerId = id;
            message.Title = aihe;
            message.Message1 = viesti;
            message.Done = false;
            db.Messages.Add(message);
            db.SaveChanges();
            return View("Index");
        }
    }
}
