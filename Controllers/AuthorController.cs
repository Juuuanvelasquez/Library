using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _autorService;

        public AuthorController(IAuthorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var autores = await _autorService.ObtenerTodosAsync();
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var autor = await _autorService.ObtenerPorIdAsync(id);
            if (autor == null)
                return NotFound(new { mensaje = "Autor no encontrado." });

            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autor = await _autorService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = autor.Id }, autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autor = await _autorService.ActualizarAsync(id, dto);
            if (autor == null)
                return NotFound(new { mensaje = "Autor no encontrado." });

            return Ok(autor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _autorService.EliminarAsync(id);
            return NoContent();
        }
    }
}
