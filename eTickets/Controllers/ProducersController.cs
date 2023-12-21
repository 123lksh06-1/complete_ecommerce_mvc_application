using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        //private readonly AppDbContext _context; // inject dbcontext file which is appdbcontext because it is file to send and receive data
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service; // for as we can use the context we need to inject it in constructor
        }
        //public IActionResult Index(){
           // var allProducers = _context.Producers.ToList();
            //return View();}

        //changing above code to asynchronus method
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }
        // Get: Producers/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
        // 1st step is to display empty view to user which is get request then click Create button which sends post request to controller

        //Get: producers/create
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Get: producers/edit/1
        public async Task<IActionResult> Edit(int id)
        { var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails== null)
            {
                return View("NotFound");
            }
            return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer) //id is what coming from request
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if (id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        //Get: producers/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View(producerDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) //id is what coming from request
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
