using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private ApplicationDbContext _context;

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<MusicController>
        [HttpGet]
        public IQueryable<SongDTO> GetAll()
        {
            var songs = from s in _context.Songs
                       select new SongDTO()
                       {
                           Id = s.Id,
                           Title = s.Title,
                           Artist = s.Artist,
                       };
            return songs;
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetSong(int id)
        {
            var song = await _context.Songs.Select(s => new SongDetailsDto()
            {
                Id = s.Id,
                Title = s.Title,
                Artist = s.Artist,
                Album = s.Album,
                ReleaseDate = s.ReleaseDate
            }).SingleOrDefaultAsync(s => s.Id == id);

            return Ok(song);
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            try
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSong), new { Id = song.Id }, song);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            try
            {
                _context.Entry(song).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok(song);
            }
            catch (Exception durrrrrp)
            {
                return BadRequest(durrrrrp);
            }
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                 var song = _context.Songs.Where(s => s.Id == id).FirstOrDefault();
                _context.Remove(song);
                _context.SaveChanges();
                return Ok(song);
            }
            catch (Exception derrrrrrp)
            {
                return BadRequest(derrrrrrp);
            }
        }
    }
}
