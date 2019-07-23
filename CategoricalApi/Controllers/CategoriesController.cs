using System.Collections.Generic;
using CategoricalApi.Data;
using CategoricalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoricalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return this.categoryService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            return this.categoryService.GetById(id);
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody]Category category)
        {
            var newCategory = new Category
            {
                Title = category.Title,
                Description = category.Description
            };

            var cat = this.categoryService.Create(newCategory);
            return cat;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Category category)
        {
            var existingCategory = this.categoryService.GetById(id);
            if (existingCategory != null)
            {
                existingCategory.Title = category.Title;
                existingCategory.Description = category.Description;
                this.categoryService.Update(existingCategory);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = this.categoryService.GetById(id);
            if (category != null)
            {
                this.categoryService.Remove(category);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/items")]
        public ActionResult<Item> AddItemToCategory(int id, [FromBody]Item item)
        {
            var existingCategory = this.categoryService.GetById(id);
            if (existingCategory != null)
            {
                var newItem = new Item
                {
                    Title = item.Title,
                    Description = item.Description
                };
                existingCategory.Items.Add(newItem);
                this.categoryService.Update(existingCategory);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{categoryId}/items/{itemId}")]
        public IActionResult RemoveItemFromCategory(int categoryId, int itemId)
        {
            var existingCategory = this.categoryService.GetById(categoryId);
            if (existingCategory != null)
            {
                var existingItem = existingCategory.Items.Find(x=>x.Id == itemId);
                if (existingItem != null)
                {
                    existingCategory.Items.Remove(existingItem);
                    this.categoryService.Update(existingCategory);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}