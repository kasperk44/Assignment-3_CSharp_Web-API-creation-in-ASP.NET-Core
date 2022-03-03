using Assignment3.DTOs.Character;
using Assignment3.DTOs.Movie;
using Assignment3.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Check if Movie exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }

        /// <summary>
        /// Gets a list of Movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MovieReadDTO>> GetMovies()
        {
            var movieList = _mapper.Map<List<MovieReadDTO>>(_context.Movie.Include(p => p.Character).ToList<Movie>());

            return Ok(movieList);
        }


        /// <summary>
        /// Gets a Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovieById(int id)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            var movie = await _context.Movie.Include(p => p.Franchise).Include(p => p.Character).FirstOrDefaultAsync(p => p.MovieId == id);

            return Ok(_mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// Adds a new Movie
        /// </summary>
        /// <param name="newMovie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieReadDTO>> PostMovie([FromBody] MovieCreateDTO newMovie)
        {
            var domainMovie = _mapper.Map<Movie>(newMovie);

            _context.Movie.Add(domainMovie);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //return "Added new Movie";
            return CreatedAtAction("GetMovieById", new { Id = domainMovie.MovieId }, newMovie);
        }


        /// <summary>
        /// Deletes a Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);

            _context.Movie.Remove(movie);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newMovie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, MovieUpdateDTO newMovie)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            var domainMovie = _mapper.Map<Movie>(newMovie);

            if (id != domainMovie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(domainMovie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Update Characters in Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharacter(int id, List<int> characters)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            Movie movieToUpdateCharacters = await _context.Movie
                .Include(c => c.Character)
                .Where(c => c.MovieId == id)
                .FirstAsync();

            List<Character> allCharacters = new();
            foreach (int characterId in characters)
            {
                Character character = await _context.Character.FindAsync(characterId);
                if (character == null)
                    return BadRequest("Characters doesnt exist!");
                allCharacters.Add(character);
            }
            movieToUpdateCharacters.Character = allCharacters;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Get all Characters in a Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public ActionResult<IEnumerable<CharacterReadDTO>> GetAllCharactersMovie(int id)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            var movieToUpdateCharacters = _mapper.Map<List<MovieReadDTO>>(_context.Movie
                .Include(p => p.Character)
                .Where(c => c.MovieId == id)
                .ToList<Movie>());

            List<CharacterReadDTO> allCharacters = new();

            foreach (var movie in movieToUpdateCharacters)
            {
                foreach (var character in movie.Characters)
                {
                    CharacterReadDTO characterInMovie = _mapper.Map<CharacterReadDTO>(_context.Character.Find(character));

                    allCharacters.Add(characterInMovie);
                }
            }

            return Ok(allCharacters);
        }
    }
}
