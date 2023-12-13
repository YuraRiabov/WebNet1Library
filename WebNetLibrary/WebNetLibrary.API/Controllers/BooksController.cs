using Microsoft.AspNetCore.Mvc;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.Common.Contracts.Book;

namespace WebNetLibrary.API.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? nameFilter, [FromQuery] List<long>? authorsFilter, [FromQuery] List<long>? themesFilter)
    {
        return Ok(_bookService.Search(nameFilter, authorsFilter, themesFilter));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        return Ok(await _bookService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        return Ok(await _bookService.Create(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookDto updateDto)
    {
        await _bookService.Update(updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _bookService.Delete(id);
        return NoContent();
    }
}