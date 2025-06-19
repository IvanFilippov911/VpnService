using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Tariffs;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Route("api/admin/tariffs")]
[ApiController]
[SetLogSource(SystemLogSource.Tariff)]
public class AdminTariffController : WrapperController
{
    private readonly ITariffService tariffService;

    public AdminTariffController(ITariffService tariffService)
    {
        this.tariffService = tariffService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTariff([FromBody] TariffCreateUpdateDto dto)
    {
        await tariffService.CreateTariffAsync(dto);
        return Ok(CreateSuccessResponse("Tariff created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTariff(Guid id, [FromBody] TariffCreateUpdateDto dto)
    {
        await tariffService.UpdateTariffAsync(id, dto);
        return Ok(CreateSuccessResponse("Tariff updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTariff(Guid id)
    {
        var result = await tariffService.DeleteTariffAsync(id);

        if (!result)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode,
                CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse("Tariff deleted successfully"));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterTariffs([FromQuery] TariffFilterDto filter)
    {
        var tariffs = await tariffService.FilterTariffsAsync(filter);
        return Ok(CreateSuccessResponse(tariffs));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTariffById(Guid id)
    {
        var tariff = await tariffService.GetTariffByIdAsync(id);

        if (tariff == null)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode,
                CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse(tariff));
    }
}
