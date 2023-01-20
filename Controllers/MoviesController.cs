using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dasp5.Models;
using Dasp5.Services;
using MongoDB.Bson;

namespace Dasp5.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesService _moviesService;

        public MoviesController(MoviesService moviesService) =>
            _moviesService = moviesService;


        public async Task<IActionResult> Setup()
        {
            foreach (Movie mv in SeedData.mv())
            {
                await _moviesService.CreateAsync(mv);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            var movies = await _moviesService.GetAsync();

            if (movies is null)
            {
                return Problem("Movies is null.");

            }
           

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = await _moviesService.Search(searchString);
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = await _moviesService.GetByGenreAsync(movieGenre);
            }
            var AllGenre = await _moviesService.GetAllGenreAsync();
            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(AllGenre),
                Movies = movies
            };
    return View(movieGenreVM);
}

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesService.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                
                await _moviesService.CreateAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesService.GetAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    movie.Id = id;

                    await _moviesService.UpdateAsync(id, movie);
                }
                catch
                {
                   
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            
            var movie = await _moviesService.GetAsync(id);
                
            if (movie == null)
            {
                return NotFound();
            }
            

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var movie = await _moviesService.GetAsync(id);
            if (movie != null)
            {
                await _moviesService.RemoveAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
