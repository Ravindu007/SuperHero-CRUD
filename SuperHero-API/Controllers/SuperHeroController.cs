using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero_API.Data;

namespace SuperHero_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHeroModel>>> GetSuperHeros()
        {
           return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHeroModel>>> CreateSuperHero(SuperHeroModel hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<SuperHeroModel>>> UpdateSuperhero(SuperHeroModel hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.id);
            if(dbHero is null)
            {
                return BadRequest("Hero Not Found");
            }

            dbHero.name = hero.name;
            dbHero.firstName = hero.firstName;
            dbHero.lastName = hero.lastName;
            dbHero.place  = hero.place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHeroModel>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero is null)
            {
                return BadRequest("Hero Not Found");
            }

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
