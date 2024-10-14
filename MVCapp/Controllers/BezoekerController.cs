using Microsoft.AspNetCore.Mvc;
using MVCapp.Db;
using MVCapp.Models;
using System.Linq;

namespace MVCapp.Controllers
{
    public class BezoekerController : Controller
    {
        private readonly VoorbeeldDb _context;

        public BezoekerController(VoorbeeldDb context)
        {
            _context = context;
        }

        // GET: Bezoeker/Index
        public IActionResult Index(string category)
        {
            var events = _context.Evenementen.AsQueryable();

            // Filteren op de geselecteerde categorie als deze niet null of leeg is
            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Category == category);
            }

            return View(events.ToList());
        }

        // POST: Bezoeker/KoopTicket
        [HttpPost]
        public IActionResult KoopTicket(int id)
        {
            var eventToBuy = _context.Evenementen.FirstOrDefault(e => e.Id == id);
            if (eventToBuy != null && eventToBuy.MaxParticipants > 0)
            {
                // Verlaag het aantal beschikbare plaatsen
                eventToBuy.MaxParticipants--;

                // Sla de wijziging op
                _context.SaveChanges();

                // Bevestigingsboodschap tonen
                TempData["Message"] = $"U heeft succesvol een ticket gekocht voor {eventToBuy.Title}.";
            }
            else
            {
                TempData["Error"] = "Dit evenement is uitverkocht.";
            }

            return RedirectToAction("Index");
        }
    }
}
