#define Primary
#if Primary
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySQL.Models;

#region TodoController
namespace DatabaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Since the class is TestController, the api route will be api/Test , it ignores the controller part
    public class TestController : ControllerBase
    {
        private readonly AspTestContext _context;
        #endregion

        public TestController(AspTestContext context)
        {
            _context = context;

            if (_context.TestUsers.Count() == 0)
            {
                // Create a new TestUsers if collection is empty,
                // which means you can't delete all TestUsers.
                _context.TestUsers.Add(new TestUsers { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestUsers>>> GetTodoItems()
        {
            return await _context.TestUsers.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestUsers>> GetTodoItem(long id)
        {
            var TestUsers = await _context.TestUsers.FindAsync(id);

            if (TestUsers == null)
            {
                return NotFound();
            }

            return TestUsers;
        }
        #endregion
        #endregion

        #region snippet_Create
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TestUsers>> PostTodoItem(TestUsers item)
        {
            _context.TestUsers.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
        #endregion

        #region snippet_Update
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TestUsers item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var TestUsers = await _context.TestUsers.FindAsync(id);

            if (TestUsers == null)
            {
                return NotFound();
            }

            _context.TestUsers.Remove(TestUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
#endif