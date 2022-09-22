using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var coupon = await _repository.GetDiscount(productName);
        return Ok(coupon);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(void), 201)]
    [ProducesResponseType(typeof(void), 400)]
    public async Task<ActionResult> CreateDiscount([FromBody] Coupon coupon)
    {
        await _repository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new {productName = coupon.ProductName}, null);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 400)]
    public async Task<ActionResult> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await _repository.UpdateDiscount(coupon));
    }
    
    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 400)]
    public async Task<ActionResult> DeleteDiscount(string productName)
    {
        return Ok(await _repository.DeleteDiscount(productName));
    }
}