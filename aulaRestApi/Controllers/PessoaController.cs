using aulaRestApi.Data;
using aulaRestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aulaRestApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        [HttpGet]
        [Route("pessoas")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {

            var pessoas = await contexto.Pessoas.AsNoTracking().ToListAsync();//.AsNoTracking()só pode ser utilizado em consultas - altamente recomendado por questões de desempenho
            
            return pessoas == null ? NotFound() : Ok(pessoas); //if ternario
        }

        [HttpGet]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> getByIdAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {

            var pessoa = await contexto.Pessoas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            return pessoa == null ? NotFound() : Ok(pessoa);

        }

        [HttpPost]
        [Route("pessoas")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Pessoa pessoa)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                
                await contexto.Pessoas.AddAsync(pessoa);
                await contexto.SaveChangesAsync();
                return Created($"api/pessoas/{pessoa.Id}", pessoa);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }

        }


        [HttpPut]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] Contexto contexto, [FromBody] Pessoa pessoa, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var p = await contexto.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }

            try
            {
                
                p.Nome = pessoa.Nome;
                contexto.Pessoas.Update(p);
                await contexto.SaveChangesAsync();
                return Ok(p);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }

        [HttpDelete]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {

            var pDelete = await contexto.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
            if (pDelete == null)
            {
                return NotFound();
            }

            try
            {

                contexto.Pessoas.Remove(pDelete);
                await contexto.SaveChangesAsync();
                return Ok(pDelete);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
