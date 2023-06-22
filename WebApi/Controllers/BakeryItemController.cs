using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore.Metadata;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class BakeryItemController : Controller
    {
        private readonly ItemApiDbContext _dbContext;

        public BakeryItemController(ItemApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBakeryItems()
        {
            return Ok(await _dbContext.BakeryItems.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddBakeryItem(AddBakeryItemRequest addBakeryItem)
        {
            var bakeryitem = new BakeryItems()
            {
                Id = Guid.NewGuid(),
                Name = addBakeryItem.Name,
                Description = addBakeryItem.Description,
                Price = addBakeryItem.Price
            };
            await _dbContext.BakeryItems.AddAsync(bakeryitem);
            _dbContext.SaveChanges();
            return Ok(bakeryitem);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> FindBakeryItem([FromRoute]Guid id, BakeryItems bakeryItems)
        {
            var bakeryItem = await _dbContext.BakeryItems.FindAsync(id);
            if(bakeryItem != null)
            {
                return Ok(bakeryItem);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBakeryItem([FromRoute]Guid id, UpdateBakeryItemRequest updateBakeryItemRequest)
        {
            var bakeryItem = await _dbContext.BakeryItems.FindAsync(id);
            if (bakeryItem != null)
            {
                bakeryItem.Name = updateBakeryItemRequest.Name;
                bakeryItem.Description = updateBakeryItemRequest.Description;
                bakeryItem.Price = updateBakeryItemRequest.Price;
                await _dbContext.SaveChangesAsync();
                return Ok(bakeryItem);
            }
            return NotFound();

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletebakeryItem([FromRoute]Guid id, DeleteBakeryItemRequest deleteBakeryItemRequest)
        {
            var bakeryItem = await _dbContext.BakeryItems.FindAsync(id);
            if (bakeryItem != null)
            {

                _dbContext.BakeryItems.Remove(bakeryItem);
                await _dbContext.SaveChangesAsync();
                return Ok(bakeryItem);
            }
            return NotFound();
        }
    }
}
