using CRUDTest.Database;
using CRUDTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;

namespace CRUDTest.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UsuariosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _appDbContext.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _appDbContext.Usuarios.FindAsync(id);

            Usuario usuario1 = new();

            if(usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CadastraUsuario(Usuario usuario)
        {
            _appDbContext.Usuarios.Add(usuario);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioById", new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(usuario).State = EntityState.Modified;

            try{
                await _appDbContext.SaveChangesAsync();

            }catch(DbUpdateConcurrencyException ex)
            {
                if (!UsuarioExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _appDbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _appDbContext.Usuarios.Remove(usuario);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExiste(int id)
        {
            return _appDbContext.Usuarios.Any(u => u.Id == id);
        }

    }
}
