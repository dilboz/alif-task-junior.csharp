using System.Globalization;
using Api.Task.Services;
using Microsoft.AspNetCore.Mvc;

namespace Task.Api.Controllers;

[ApiController]
public class PaymentsController: Controller
{
    private readonly CategoryService _categoryService;

    public PaymentsController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get the payment
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/payments/product/amount/installmentRange
    /// </remarks>
    /// <param name="product">Products name (text)</param>
    /// <param name="amount">Amount (number > 0)</param>
    /// <param name="installmentRange">Month (CountMonth => 0)</param>
    /// <returns>
    /// Returns PaymentsCount
    /// </returns>
    /// <response code="200">Success</response>
    [HttpGet("api/payments")]
    public IActionResult Index(string product, decimal amount, int installmentRange)
    {
        var additionalPercentage = _categoryService.GetAdditionalPercentage(product, installmentRange);

        if (additionalPercentage == 0)
        {
            return BadRequest("Invalid category!");
        }

        var totalAmount = ((double)amount * additionalPercentage) / 100;

        return Ok((totalAmount + (double)amount).ToString(CultureInfo.InvariantCulture));
    }
}