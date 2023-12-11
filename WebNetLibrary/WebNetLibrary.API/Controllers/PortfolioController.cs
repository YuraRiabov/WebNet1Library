using Microsoft.AspNetCore.Mvc;
using WebNetLibrary.BLL.Interfaces;

namespace WebNetLibrary.API.Controllers;

[ApiController]
[Route("users")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }
    
    [HttpGet("{userId}/portfolio")]
    public async Task<IActionResult> Get(long userId)
    {
        return Ok(await _portfolioService.GetBookIds(userId));
    }
    
    [HttpPost("{userId}/portfolio")]
    public async Task<IActionResult> Add(long userId, [FromBody] long bookId)
    {
        var result = await _portfolioService.Add(userId, bookId);
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpDelete("{userId}/portfolio")]
    public async Task<IActionResult> Get(long userId, [FromBody] long bookId)
    {
        var result = await _portfolioService.Remove(userId, bookId);
        if (result)
        {
            return NoContent();
        }

        return BadRequest();
    }
}