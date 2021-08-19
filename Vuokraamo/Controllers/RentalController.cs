using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vuokraamo.Models;

namespace Vuokraamo.Controllers
{
    public class RentalController : Controller
    {
        private readonly VarastoDBContext _context;

        public RentalController(VarastoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult VuokrausTiedot()
        {

            VarastoDBContext db = _context;
            int cstId = (int)HttpContext.Session.GetInt32("cid");
            List<Rental> Vuokrat = db.Rentals.Where(a => a.CustomerId == cstId).ToList();

            Vuokrat.ForEach(a => a.Product = db.Products
            .Where(b => b.Id == a.ProductId)
            .FirstOrDefault());

            return View(Vuokrat);
        }
        public IActionResult Osta()
        {
            int cstId = (int)HttpContext.Session.GetInt32("cid");

            VarastoDBContext db = _context;
            List<Cart> asiakkaanOstokset = db.Carts.Where(a => a.CustomerId == cstId).ToList();
            List<Rental> vuokratut = new List<Rental>();

            foreach (var a in asiakkaanOstokset)
            {
                DateTime dt = DateTime.Now;
                Rental rental = new Rental();
                Product product = db.Products.Where(b => b.Id == a.ProductId).FirstOrDefault();
                product.Amount--;
                rental.CustomerId = a.CustomerId;
                rental.Price = Convert.ToInt32(a.ProductPrice);
                rental.ProductId = (int)a.ProductId;
                rental.Rentaldate = dt;
                rental.Returndate = dt.AddDays(7);
                db.Rentals.Add(rental);
                db.Carts.Remove(a);
                db.SaveChanges();
                GeneroiLasku(rental);
            }
            // Tässä kutsutaan apumetodia joka generoi laskun


            return RedirectToAction("Vuokraustiedot");
        }
        public void GeneroiLasku(Rental rental)
        {
            DateTime dt = DateTime.Now;
            VarastoDBContext db = _context;
            Invoice invoice = new Invoice();
            invoice.CustomerId = (int)rental.CustomerId;
            invoice.Paid = false;
            invoice.RentalId = db.Rentals.OrderBy(a=> a.RentalId).Last().RentalId;
            invoice.TotalDue = rental.Price;
            invoice.DueDate = dt.AddDays(7);
            db.Invoices.Add(invoice);
            db.SaveChanges();

        }
        public IActionResult PalautaTuote(int Id)
        {
            VarastoDBContext db = _context;
            Rental rental = db.Rentals.Where(a => a.RentalId == Id).FirstOrDefault();
            rental.Returndate = DateTime.Now;
            Product product = db.Products.Where(a => a.Id == rental.ProductId).FirstOrDefault();
            product.Amount++;
            db.Rentals.Update(rental);
            db.Products.Update(product);
            db.SaveChanges();

            return RedirectToAction("Vuokraustiedot");
            
        }
        
        
    }
}
