using DrakarVpn.API.Controllers;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Mvc;

[Route("api/user/[controller]")]
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

}