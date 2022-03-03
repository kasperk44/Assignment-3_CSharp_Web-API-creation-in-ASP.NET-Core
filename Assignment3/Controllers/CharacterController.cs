using Assignment3.DTOs.Character;
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
    public class CharacterController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public CharacterController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Check if Character exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.CharacterId == id);
        }

        /// <summary>
        /// Gets a list of Characters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CharacterReadDTO>> GetCharacters()
        {
            var characterList = _mapper.Map<List<CharacterReadDTO>>(_context.Character.Include(p => p.Movie).ToList<Character>());

            return Ok(characterList);
        }

        /// <summary>
        /// Gets a Character by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacterById(int id)
        {
            if (!CharacterExists(id))
            {
                return NotFound();
            }

            var character = await _context.Character.Include(p => p.Movie).FirstOrDefaultAsync(p => p.CharacterId == id);

            return Ok(_mapper.Map<CharacterReadDTO>(character));
        }

        /// <summary>
        /// Adds a new Character
        /// </summary>
        /// <param name="newCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterReadDTO>> PostCharacter([FromBody] CharacterCreateDTO newCharacter)
        {
            var domainCharacter = _mapper.Map<Character>(newCharacter);

            _context.Character.Add(domainCharacter);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //return "Added new Character";
            return CreatedAtAction("GetCharacterById", new { Id = domainCharacter.CharacterId }, newCharacter);
        }

        /// <summary>
        /// Deletes a Character
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            if (!CharacterExists(id))
            {
                return NotFound();
            }

            var character = await _context.Character.FindAsync(id);

            _context.Character.Remove(character);

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
        /// Updates a Character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newCharacter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, CharacterUpdateDTO newCharacter)
        {
            if (!CharacterExists(id))
            {
                return NotFound();
            }

            var domainCharacter = _mapper.Map<Character>(newCharacter);

            if (id != domainCharacter.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(domainCharacter).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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

    }
}
