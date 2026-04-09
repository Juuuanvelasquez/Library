using LibraryAPI.DTOs;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _libroService;

        public BookController(IBookService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var libros = await _libroService.ObtenerTodosAsync();
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var libro = await _libroService.ObtenerPorIdAsync(id);
            if (libro == null)
                return NotFound(new { mensaje = "Libro no encontrado." });

            return Ok(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var libro = await _libroService.CrearAsync(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = libro.Id }, libro);
            }
            catch (AuthorException ex)
            {
                // El author not registered
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (BookException ex)
            {
                // Book limit reached
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var libro = await _libroService.ActualizarAsync(id, dto);
                if (libro == null)
                    return NotFound(new { mensaje = "Libro no encontrado." });

                return Ok(libro);
            }
            catch (AuthorException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _libroService.EliminarAsync(id);
            return NoContent();
        }
    }
}
