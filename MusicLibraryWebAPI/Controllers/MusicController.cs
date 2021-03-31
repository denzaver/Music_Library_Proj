using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll(int id)
        {
            var song = _context.Songs.ToList();
            return Ok(song);
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var song = _context.Songs.Where(x => x.Id == id);
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
                return CreatedAtAction(nameof(Get), new { Id = song.Id }, song);
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
                return Ok();
            }
            catch (Exception derrrrrrp)
            {
                return BadRequest(derrrrrrp);
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
                return Ok();
            }
            catch (Exception derrrrrrp)
            {
                return BadRequest(derrrrrrp);
            }


        }
    }
}
