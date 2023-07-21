using Microsoft.AspNetCore.Mvc;
using Task.Api.Data;
using Task.Api.Models;

namespace Api.Task.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Get the list category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/categories
    /// </remarks>
    /// <returns>
    /// Returns Category ListVM
    /// </returns>
    /// <response code="200">Success</response>
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategories()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }
    
    /// <summary>
    /// Get the category by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/categories/2a114149-1778-414f-8f04-9b5a4903f535
    /// </remarks>
    /// <param name="id">Category id (guid)</param>
    /// <returns>
    /// Returns Category
    /// </returns>
    /// <response code="200">Success</response>
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(Guid id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    
    /// <summary>
    /// Get the category by categoryName
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/categories/smartphone
    /// </remarks>
    /// <param name="categoryName">Category Name (text)</param>
    /// <returns>
    /// Returns Category
    /// </returns>
    /// <response code="200">Success</response>
    [HttpGet("{categoryName}")]
    public ActionResult<Category> GetCategoriesByCategoryName(string categoryName)
    {
        if (categoryName == null) throw new ArgumentNullException(nameof(categoryName));
        var categories = _context.Categories
            .Where(c => c.CategoryName == categoryName)
            .ToList();

        if (categories.Count == 0)
        {
            return NotFound();
        }

        return Ok(categories[0]);
    }

    /// <summary>
    /// Creates the category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/categories
    /// {
    ///     Id: "2a114149-1778-414f-8f04-9b5a4903f535",
    ///     CategoryName: "CategoryTest",
    ///     RangeFrom: "3",
    ///     RangeTo: "21",
    ///     Percent: "6"
    /// }
    /// </remarks>
    /// <param name="category">CreateCategoryDto object</param>
    /// <returns>
    /// Returns Id (GUID)
    /// </returns>
    /// <response code="200">Success</response>
    [HttpPost]
    public IActionResult CreateCategory(Category category)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Categories.Add(category);
        _context.SaveChanges();
        return Ok();
    }

    /// <summary>
    /// Update the category by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /api/categories/2a114149-1778-414f-8f04-9b5a4903f535
    /// </remarks>
    /// <param name="id">Category id (guid)</param>
    /// <param name="updatedCategory"></param>
    /// <returns>
    /// Returns UpdateContent
    /// </returns>
    /// <response code="200">Success</response>
    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid id, Category updatedCategory)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        category.CategoryName = updatedCategory.CategoryName;
        category.RangeFrom = updatedCategory.RangeFrom;
        category.RangeTo = updatedCategory.RangeTo;
        category.Percent = updatedCategory.Percent;

        _context.SaveChanges();

        return Ok();
    }
    
    /// <summary>
    /// Deletes the category by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /api/categories/2a114149-1778-414f-8f04-9b5a4903f535
    /// </remarks>
    /// <param name="id">Category id (guid)</param>
    /// <returns>
    /// Returns NoContent
    /// </returns>
    /// <response code="200">Success</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return Ok();
    }
}