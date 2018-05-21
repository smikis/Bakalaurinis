using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Category;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
       
        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            return _categoryService.GetAll();
        }

        // GET: api/Category/5
        [HttpGet("{id}"), Authorize]
        public CategoryDto Get(int id)
        {
            return _categoryService.GetById(id);
        }
        
        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody]CategoryDto value)
        {
            if (ModelState.IsValid)
            {
                if (_categoryService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        
        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CategoryUpdateDto value)
        {
            if (ModelState.IsValid)
            {
                if (_categoryService.Update(value, id))
                {
                    return Ok();
                }
            }
            return BadRequest();

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (_categoryService.Delete(id))
                {
                    return Ok();
                }
            }
            return BadRequest();

        }
    }
}
