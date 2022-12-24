using MeuTodo_net5.Data;
using MeuTodo_net5.Models;
using MeuTodo_net5.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuTodo_net5.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> Get([FromServices] AppDbContext _context)
        {
            var todos = await _context.Todos.AsNoTracking().ToListAsync();

            return Ok(todos ?? new List<Todo>());
        }

        [HttpGet]
        [Route("todos/{id=int}")]
        public async Task<IActionResult> GetId([FromServices] AppDbContext _context,
                                                                int id)
        {
            var todo = await _context.Todos.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Id == id);

            return todo == null
                ? NotFound() : Ok(todo);
        }

        [HttpPost]
        [Route("todos")]
        public async Task<IActionResult> Post([FromServices] AppDbContext _context,
                                              [FromBody] InserirTodoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var todo = new Todo
                {
                    Titulo = model.Titulo,
                    Realizado = false,
                    DataCadastro = DateTime.Now
                };

                await _context.Todos.AddAsync(todo);
                await _context.SaveChangesAsync();

                return Created(uri: $"v1/todos/{todo.Id}", todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("todos/{id=int}")]
        public async Task<IActionResult> Put([FromServices] AppDbContext _context,
                                             [FromBody] EditarTodoViewModel model,
                                             int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

                if (todo is null)
                    return NoContent();

                todo.Titulo = model.Titulo;
                todo.Realizado = model.Realizado;

                await _context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("todos/{id=int}")]
        public async Task<IActionResult> Delete([FromServices] AppDbContext _context,
                                                int id)
        {
            try
            {
                var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

                if (todo is null)
                    return NoContent();

                _context.Remove(todo);                
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
