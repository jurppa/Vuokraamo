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
                                // Haetaan asiakkaan ostoskori Id:n perusteella, id tallennettu sessioon
        public IActionResult Ostoskori()
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            List <Cart> customersCart = _context.Carts.Where(a => a.CustomerId == cstId).ToList();

            return View(customersCart);
        }
                                // Lisää tuote ostoskorin tietokantaan, asiakkaan id:llä
        [HttpGet]
        public IActionResult LisääOstoskoriin(int productId)
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            VarastoDBContext db = _context;
            Product productToAdd = db.Products.Where(a => a.Id == productId).FirstOrDefault();

            Cart customersCart = new Cart();
            customersCart.CustomerId = cstId;
            customersCart.ProductId = productToAdd.Id;
            customersCart.ProductPrice = (decimal) productToAdd.Price;
            customersCart.ProductName = productToAdd.Name;
            db.Carts.Add(customersCart);
            db.SaveChanges();
            return RedirectToAction("ProductList", "Home");

        }
                                // poistaa tuotteen ostoskorista
        public IActionResult Poista(int Id)
        {
            VarastoDBContext db = _context;
            Cart poistettava = db.Carts.Where(a => a.CartId == Id).FirstOrDefault();
            db.Carts.Remove(poistettava);
            db.SaveChanges();

            return RedirectToAction("Ostoskori");
        }
    }
}
