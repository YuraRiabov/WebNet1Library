using Microsoft.AspNetCore.Mvc;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.Common.Contracts.Author;

namespace WebNetLibrary.API.Controllers;

[ApiController]
[Route("authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_authorService.Get());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        return Ok(await _authorService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorDto dto)
    {
        return Ok(await _authorService.Create(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(AuthorDto updateDto)
    {
        await _authorService.Update(updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _authorService.Delete(id);
        return NoContent();
    }
}