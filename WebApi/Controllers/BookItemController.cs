using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class BookItemController : Controller
    {   
        private readonly ItemApiDbContext _dbContext;
        public BookItemController(ItemApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookItems()
        {
            return Ok(await _dbContext.BooksItems.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddBookItem(AddBookItemRequest addBookItemRequest)
        {
            var addbookitem = new BooksItems()
            {
                Id = Guid.NewGuid(),
                Name = addBookItemRequest.Name,
                Description = addBookItemRequest.Description,
                Price = addBookItemRequest.Price
            };
            await _dbContext.BooksItems.AddAsync(addbookitem);
            _dbContext.SaveChanges();
            return Ok(addbookitem);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBookItem([FromRoute]Guid id, UpdateBookItemRequest updateBookItemRequest)
        {
            var updatebookitem = await _dbContext.BooksItems.FindAsync(id);
            if(updatebookitem != null)
            {
                updatebookitem.Name = updateBookItemRequest.Name;
                updatebookitem.Description = updateBookItemRequest.Description;
                updatebookitem.Price = updateBookItemRequest.Price;
                _dbContext.SaveChanges();
                return Ok(updatebookitem);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteBookItem([FromRoute]Guid id,DeleteBookItemRequest deleteBookItemRequest)
        {
            var deleteBookItem = await _dbContext.BooksItems.FindAsync(id);
            if(deleteBookItem != null)
            {
                _dbContext.BooksItems.Remove(deleteBookItem);
                _dbContext.SaveChanges();
                return Ok(deleteBookItem);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> FindBookItem([FromRoute]Guid id, BooksItems booksItems)
        {
            var bookItem = await _dbContext.BooksItems.FindAsync(id);
            if(bookItem != null)
            {
                return Ok(bookItem);
            }
            return NotFound();
        }

    }
}
