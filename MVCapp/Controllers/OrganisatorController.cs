using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCapp.Controllers
{
    public class OrganisatorController : Controller
    {
        private static List<Organisator> events = new List<Organisator>();


        // GET: Organisator/Index
        public IActionResult Index()
        {
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
                newEvent.Id = events.Count + 1;
                events.Add(newEvent);
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }

        // GET: Organisator/Edit/5
        public IActionResult Edit(int id)
        {
            var eventToEdit = events.FirstOrDefault(e => e.Id == id);
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
                var eventToUpdate = events.FirstOrDefault(e => e.Id == id);
                if (eventToUpdate != null)
                {
                    eventToUpdate.Title = updatedEvent.Title;
                    eventToUpdate.Location = updatedEvent.Location;
                    eventToUpdate.EventDateTime = updatedEvent.EventDateTime;
                    eventToUpdate.Cost = updatedEvent.Cost;
                    eventToUpdate.MaxParticipants = updatedEvent.MaxParticipants;
                    eventToUpdate.Description = updatedEvent.Description;
                    eventToUpdate.Category = updatedEvent.Category;
                    eventToUpdate.ImagePath = updatedEvent.ImagePath;

                    return RedirectToAction("Index");
                }
            }
            return View(updatedEvent);
        }

        // GET: Organisator/Delete/5
        public IActionResult Delete(int id)
        {
            var eventToDelete = events.FirstOrDefault(e => e.Id == id);
            if (eventToDelete != null)
            {
                events.Remove(eventToDelete);
            }
            return RedirectToAction("Index");
        }
    }
}
