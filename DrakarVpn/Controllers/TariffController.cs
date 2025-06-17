using DrakarVpn.API.Controllers;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Domain.ModelDto.Tariffs;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class TariffController : WrapperController
{
    private readonly ITariffService tariffService;

    public TariffController(ITariffService tariffService)
    {
        this.tariffService = tariffService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTariffs()
    {
        var tariffs = await tariffService.GetAllTariffsAsync();
        return Ok(CreateSuccessResponse(tariffs));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTariffById(Guid id)
    {
        var tariff = await tariffService.GetTariffByIdAsync(id);

        if (tariff == null)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode, CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse(tariff));
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTariff([FromBody] TariffCreateUpdateDto dto)
    {
        await tariffService.CreateTariffAsync(dto);
        return Ok(CreateSuccessResponse("Tariff created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTariff(Guid id, [FromBody] TariffCreateUpdateDto dto)
    {
        await tariffService.UpdateTariffAsync(id, dto);
        return Ok(CreateSuccessResponse("Tariff updated successfully"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTariff(Guid id)
    {
        var result = await tariffService.DeleteTariffAsync(id);

        if (!result)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode, CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse("Tariff deleted successfully"));
    }

    [HttpGet("filter")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> FilterTariffs([FromQuery] TariffFilterDto filter)
    {
        var tariffs = await tariffService.FilterTariffsAsync(filter);
        return Ok(CreateSuccessResponse(tariffs));
    }

}