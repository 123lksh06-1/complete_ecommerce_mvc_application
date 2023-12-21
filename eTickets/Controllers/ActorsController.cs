using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service; //as now injection is in actors interface
        //private readonly AppDbContext _context; // inject dbcontext file which is appdbcontext because it is file to send and receive data and like we are getting the data
       public ActorsController(IActorsService service)
        {
            _service= service; // for as we can use the context we need to inject it in constructor
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(); //here we will get data inside Index
            return View(data); //we need to create view for this
        }
        //Get request, url: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //in create view we can see input as submit that means we are sending post request therefore needs create action result
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor) //binding properties as id is also tHERE AND WE DON'T NEED IT
        {
            if (!ModelState.IsValid) //we will add 'Required' in Actor model and that all will be checked here and that's called validation
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));

        }
// Get : Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails= await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }
        //Get: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);

        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor) //binding properties as id is also tHERE AND WE DON'T NEED IT
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(actor.Id, actor);
            return RedirectToAction(nameof(Index));

        }
        //Get: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);

        }

        [HttpPost, ActionName("Delete")] //action name is delete threfore in view form action is Delete otherwise it would be DeleteConfirmed
        public async Task<IActionResult> DeleteConfirmed(int id) //binding properties as id is also tHERE AND WE DON'T NEED IT
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
