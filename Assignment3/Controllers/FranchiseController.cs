using Assignment3.DTOs.Character;
using Assignment3.DTOs.Franchise;
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
    public class FranchiseController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Check if Franchise exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool FranchiseExists(int id)
        {
            return _context.Franchise.Any(e => e.FranchiseId == id);
        }

        /// <summary>
        /// Gets a list of Franchises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<FranchiseReadDTO>> GetFranchises()
        {
            var franchiseList = _mapper.Map<List<FranchiseReadDTO>>(_context.Franchise.Include(p => p.Movie).ToList<Franchise>());

            return Ok(franchiseList);
        }

        /// <summary>
        /// Gets a Franchise by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchiseById(int id)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            var franchise = await _context.Franchise.Include(p => p.Movie).FirstOrDefaultAsync(p => p.FranchiseId == id);

            return Ok(_mapper.Map<FranchiseReadDTO>(franchise));
        }

        /// <summary>
        /// Adds a new Franchise
        /// </summary>
        /// <param name="newFranchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseReadDTO>> PostFranchise([FromBody] FranchiseCreateDTO newFranchise)
        {
            var domainFranchise = _mapper.Map<Franchise>(newFranchise);

            _context.Franchise.Add(domainFranchise);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //return "Added new Franchise";
            return CreatedAtAction("GetFranchiseById", new { Id = domainFranchise.FranchiseId }, newFranchise);
        }


        /// <summary>
        /// Deletes a Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteFranchise(int id)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            var franchise = await _context.Franchise.FindAsync(id);

            _context.Franchise.Remove(franchise);

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
        /// Updates a Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newFranchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFranchise(int id, FranchiseUpdateDTO newFranchise)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            var domainFranchise = _mapper.Map<Franchise>(newFranchise);

            if (id != domainFranchise.FranchiseId)
            {
                return BadRequest();
            }

            _context.Entry(domainFranchise).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
        /// Update Movies in a Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<ActionResult> UpdateFranchiseMovie(int id, List<int> movies)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            foreach (int movieId in movies)
            {
                Movie movie = await _context.Movie.FindAsync(movieId);

                movie.FranchiseId = id;
            }

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
        /// Get all Movies in a Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        public ActionResult<IEnumerable<MovieReadDTO>> GetAllMoviesFranchise(int id)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            var movieList = _mapper.Map<List<MovieReadDTO>>(_context.Movie.Include(p => p.Franchise).Where(c => c.FranchiseId == id).ToList<Movie>());

            return Ok(movieList);
        }

        /// <summary>
        /// Get all Characters in a Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public ActionResult<IEnumerable<MovieReadDTO>> GetAllCharactersFranchise(int id)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            List<CharacterReadDTO> allCharacters = new();

                var movieToUpdateCharacters = _mapper.Map<List<MovieReadDTO>>(_context.Movie
                    .Include(p => p.Character)
                    .Where(c => c.FranchiseId == id)
                    .ToList<Movie>());

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
