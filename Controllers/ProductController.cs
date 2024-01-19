using Microsoft.AspNetCore.Mvc;
using eclowcode.Model;
using eclowcode.Data;
using eclowcode.Filter;

namespace eclowcode.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly eclowcodeContext _context;

        public ProductController(eclowcodeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product model)
        {
            _context.Product.Add(model);
            var returnData = this._context.SaveChanges();
            return Ok(returnData);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string filters)
        {
            var filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            var query = _context.Product.AsQueryable();
            var result = FilterService<Product>.ApplyFilter(query, filterCriteria);
            return Ok(result);
        }

        [HttpGet]
        [Route("{entityId:int}")]
        public IActionResult GetById([FromRoute] int entityId)
        {
            var entityData = _context.Product.FirstOrDefault(entity => entity.Id == entityId);
            return Ok(entityData);
        }

        [HttpDelete]
        [Route("{entityId:int}")]
        public IActionResult DeleteById([FromRoute] int entityId)
        {
            var entityData = _context.Product.FirstOrDefault(entity => entity.Id == entityId);
            if (entityData == null)
            {
                return NotFound();
            }

            _context.Product.Remove(entityData);
            var returnData = this._context.SaveChanges();
            return Ok(returnData);
        }

        [HttpPut]
        [Route("{entityId:int}")]
        public IActionResult UpdateById(int entityId, [FromBody] Product updatedEntity)
        {
            if (entityId != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var entityData = _context.Product.FirstOrDefault(entity => entity.Id == entityId);
            if (entityData == null)
            {
                return NotFound();
            }

            var propertiesToUpdate = typeof(Product).GetProperties().Where(property => property.Name != "Id").ToList();
            foreach (var property in propertiesToUpdate)
            {
                property.SetValue(entityData, property.GetValue(updatedEntity));
            }

            var returnData = this._context.SaveChanges();
            return Ok(returnData);
        }
    }
}