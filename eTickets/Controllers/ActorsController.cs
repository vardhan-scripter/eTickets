using eTickets.Data.Services.Interfaces;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Actors/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName", "ProfilePictureURL" ,"Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Details/Id
        public async Task<IActionResult> Details(int Id)
        {
            var ActorDetails = await _service.GetByIdAsync(Id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }

        // Get: Actors/Edit/Id
        public async Task<IActionResult> Edit(int Id)
        {
            var ActorDetails = await _service.GetByIdAsync(Id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }

        // Post: Actors/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "FullName", "ProfilePictureURL", "Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(Id, actor);
            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Delete/Id
        public async Task<IActionResult> Delete(int Id)
        {
            var ActorDetails = await _service.GetByIdAsync(Id);
            if (ActorDetails == null) return View("NotFound");
            return View(ActorDetails);
        }

        // Post: Actors/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int Id)
        {
            var ActorDetails = await _service.GetByIdAsync(Id);
            if (ActorDetails == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
