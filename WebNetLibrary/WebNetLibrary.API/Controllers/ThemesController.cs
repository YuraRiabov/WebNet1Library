using Microsoft.AspNetCore.Mvc;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.Common.Contracts.Theme;

namespace WebNetLibrary.API.Controllers;

[ApiController]
[Route("themes")]
public class ThemesController : ControllerBase
{
    private readonly IThemeService _themeService;

    public ThemesController(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_themeService.Get());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        return Ok(await _themeService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateThemeDto dto)
    {
        return Ok(await _themeService.Create(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ThemeDto updateDto)
    {
        await _themeService.Update(updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _themeService.Delete(id);
        return NoContent();
    }
}