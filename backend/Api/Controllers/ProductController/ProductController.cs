
using Api.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProductController;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService service): ControllerBase
{
	[HttpGet("get-by-id/{productId:guid}")]
	public async Task<IActionResult> GetById(Guid productId)
	{
		var product = await service.GetByProductIdAsync(productId);
		
		return Ok(product);
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var products = await service.GetAllAsync();
		
		return Ok(products);
	}

}