using eTickets.Data.Services.Interfaces;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Get: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Producers/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName", "ProfilePictureURL", "Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        // Get: Producers/Details/Id
        public async Task<IActionResult> Details(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        // Get: Producers/Edit/Id
        public async Task<IActionResult> Edit(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        // Post: Producers/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "FullName", "ProfilePictureURL", "Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            if(Id == producer.Id)
            {
                await _service.UpdateAsync(Id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // Get: Producers/Delete/Id
        public async Task<IActionResult> Delete(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        // Post: Producers/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
