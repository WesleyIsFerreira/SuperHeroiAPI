using Microsoft.AspNetCore.Mvc;
using SuperHeroiAPI.Validators;

namespace SuperHeroiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : ControllerBase
    {

        public SuperHeroiController(DataContext context)
        {
            this.context = context;
        }

        private readonly DataContext context;

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroi>> Get(int id)
        {
            var heroi = await this.context.superHerois.FindAsync(id);
            if (heroi == null)
                return BadRequest("Herói não encontrado");
            
            return Ok(heroi);
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHeroi>> DeleteHeroi(int id)
        {
            var heroi = await this.context.superHerois.FindAsync(id);
            if (heroi == null)
                return BadRequest("Herói não encontrado");

            this.context.superHerois.Remove(heroi);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.superHerois.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHeroi>>> UpdateHeroi(SuperHeroi request)
        {
            var heroi = await this.context.superHerois.FindAsync(request.Id);
            if (heroi == null)
                return BadRequest("Herói não encontrado");

            heroi.Name = request.Name;
            heroi.FirstName = request.FirstName;
            heroi.LastName = request.LastName;
            heroi.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.superHerois.ToListAsync());
        }

        [HttpGet("all/{page}")]

        public async Task<ActionResult<List<SuperHeroi>>> GetAll(int page)
        {
            var pageResults = 10f;
            var pageCout = Math.Ceiling(this.context.superHerois.Count() / pageResults);

            var herois = await this.context.superHerois
                .Skip((page - 1) * (int) pageResults)
                .Take((int) pageResults)
                .ToListAsync();

            var response = new HeroiResponse
            {
                SuperHerois = herois,
                Pages = (int)pageCout,
                CurrentPage = page
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroi>>> AddHero(SuperHeroi heroi)
        {

            var validator = new HeroiValidator();

            var result = validator.Validate(heroi);

            if (result.IsValid == false)
            {
                return BadRequest(result.Errors);
            }

            this.context.superHerois.Add(heroi);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.superHerois.ToListAsync());
        }

    }
}
