﻿using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
       
        public async Task<IActionResult> Index(){
           var allMovies = await _service.GetAllAsync(n =>n.Cinema);
            return View(allMovies);
        }
        //Get :movies/details/id
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }
        //Get: movies/create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas,"Id","Name"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
            ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
            ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
                ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
                ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        //Get: movies/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null)
            {
                return View("NotFound"); //mvc by default search in view and then in shared folder
            }
            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate=movieDetails.StartDate,
                EndDate=movieDetails.EndDate,   
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };
            var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();


            ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
            ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
            ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
           if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
                ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName"); //we are creating 3 select list to bind them with the select in the view therefore using viewbag to pass data from controller to view
                ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index",filteredResult);

            }
            return View("Index",allMovies);
        }
    }
}
