using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ElectronicsItemController : Controller
    {
        private readonly ItemApiDbContext _dbContext;
        public ElectronicsItemController(ItemApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllElectronicItems()
        {
            return Ok(await _dbContext.ElectronicsItems.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddElectronicsItem(AddElectronicItemRequest addElectronicItemRequest)
        {
            var addElectronicsItem = new ElectronicsItems()
            {
                Id = Guid.NewGuid(),
                Name = addElectronicItemRequest.Name,
                Description = addElectronicItemRequest.Description,
                Price = addElectronicItemRequest.Price

            };
            await _dbContext.ElectronicsItems.AddAsync(addElectronicsItem);
            await _dbContext.SaveChangesAsync();
            return Ok(addElectronicsItem);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteElectronicsItem([FromRoute]Guid id, DeleteElectronicsItemRequest deleteElectronicsItemRequest)
        {
            var electronicsItem = await _dbContext.ElectronicsItems.FindAsync(id);
            if (electronicsItem == null)
            {
                _dbContext.ElectronicsItems.Remove(electronicsItem);
                await _dbContext.SaveChangesAsync();
                return Ok(electronicsItem);
            }
            return NotFound();
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateElectronicsItem([FromRoute]Guid id, UpdateElectronicsItemRequest updateElectronicsItemRequest)
        {
            var electronicsItem = await _dbContext.ElectronicsItems.FindAsync(id);
            if (electronicsItem != null)
            {
                electronicsItem.Name = updateElectronicsItemRequest.Name;
                electronicsItem.Description = updateElectronicsItemRequest.Description;
                electronicsItem.Price = updateElectronicsItemRequest.Price;
                await _dbContext.SaveChangesAsync();
                return Ok(electronicsItem);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> FindElectronicItem([FromRoute]Guid id, ElectronicsItems electronicsItems)
        {
            var electronicsItem = await _dbContext.ElectronicsItems.FindAsync(id);
            if (electronicsItem != null)
            {
                return Ok(electronicsItem);
            }
            return NotFound();
        }
    
    }
}
