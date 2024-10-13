using Microsoft.AspNetCore.Mvc;
using MVCapp.Db; // Zorg ervoor dat je de juiste namespace gebruikt voor je DbContext
using MVCapp.Models;
using System.Linq;

namespace MVCapp.Controllers
{
    public class OrganisatorController : Controller
    {
        private readonly VoorbeeldDb _context;

        // Injecteer de DbContext via de constructor
        public OrganisatorController(VoorbeeldDb context)
        {
            _context = context;
        }

        // GET: Organisator/Index
        public IActionResult Index()
        {
            // Haal de lijst van evenementen op uit de database en stuur deze naar de view
            var events = _context.Evenementen.ToList();
            return View(events);
        }

        // GET: Organisator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organisator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Organisator newEvent)
        {
            if (ModelState.IsValid)
            {
                // Voeg het nieuwe evenement toe aan de database
                _context.Evenementen.Add(newEvent);

                // Zorg ervoor dat SaveChanges wordt aangeroepen om de wijzigingen op te slaan
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            // Als het model ongeldig is, blijf op de Create-pagina
            return View(newEvent);
        }

        // GET: Organisator/Edit/5
        public IActionResult Edit(int id)
        {
            var eventToEdit = _context.Evenementen.FirstOrDefault(e => e.Id == id);
            if (eventToEdit == null)
            {
                return NotFound();
            }
            return View(eventToEdit);
        }

        // POST: Organisator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Organisator updatedEvent)
        {
            if (ModelState.IsValid)
            {
                var eventToUpdate = _context.Evenementen.FirstOrDefault(e => e.Id == id);
                if (eventToUpdate != null)
                {
                    // Werk de evenementdetails bij
                    eventToUpdate.Title = updatedEvent.Title;
                    eventToUpdate.Location = updatedEvent.Location;
                    eventToUpdate.EventDateTime = updatedEvent.EventDateTime;
                    eventToUpdate.Cost = updatedEvent.Cost;
                    eventToUpdate.MaxParticipants = updatedEvent.MaxParticipants;
                    eventToUpdate.Description = updatedEvent.Description;
                    eventToUpdate.Category = updatedEvent.Category;

                    // Sla de wijzigingen op in de database
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(updatedEvent);
        }

        // POST: Organisator/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var eventToDelete = _context.Evenementen.FirstOrDefault(e => e.Id == id);
            if (eventToDelete != null)
            {
                _context.Evenementen.Remove(eventToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
