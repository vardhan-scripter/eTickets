using eTickets.Data.Services.Interfaces;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Cinemas/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Logo", "Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        // Get: Cinemas/Details/Id
        public async Task<IActionResult> Details(int Id)
        {
            var cinemaDetails = await _service.GetByIdAsync(Id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // Get: Cinemas/Edit/Id
        public async Task<IActionResult> Edit(int Id)
        {
            var cinemaDetails = await _service.GetByIdAsync(Id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // Post: Cinemas/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "Name", "Logo", "Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            if(Id == cinema.Id)
            {
                await _service.UpdateAsync(Id, cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // Get: Cinemas/Delete/Id
        public async Task<IActionResult> Delete(int Id)
        {
            var cinemaDetails = await _service.GetByIdAsync(Id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // Post: Cinemas/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int Id)
        {
            var cinemaDetails = await _service.GetByIdAsync(Id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
