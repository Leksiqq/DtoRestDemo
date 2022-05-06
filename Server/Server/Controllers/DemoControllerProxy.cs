//------------------------------
// MVC Controller proxy 
// DtoKit.Demo.DemoControllerProxy
// (Generated automatically)
//------------------------------
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.Leksi.Dto;
using System.Text.Json;

namespace DtoKit.Demo;

public class DemoControllerProxy : Controller
{

    [Route("/shipCalls/{filter}/{amount:double}/{date}")]
    [HttpGet]
    //[Authorize(Roles = "1, 2, 3")]
    public async Task GetShipCalls(String date, Double amount, String filter)
    {
        DtoJsonConverterFactory converter = HttpContext.RequestServices.GetRequiredService<DtoJsonConverterFactory>();
        JsonSerializerOptions options = new();
        options.Converters.Add(converter);
        DateTime _date = JsonSerializer.Deserialize<DateTime>(date, options);
        ShipCallsFilter _filter = JsonSerializer.Deserialize<ShipCallsFilter>(filter, options);
        Controller controller = (Controller)HttpContext.RequestServices.GetRequiredService<IDemoController>();
        controller.ControllerContext = ControllerContext;
        await ((IDemoController)controller).GetShipCalls(_date, amount, _filter);
    }

    [Route("/form")]
    [HttpPost]
    public async Task Commit()
    {
        DtoJsonConverterFactory converter = HttpContext.RequestServices.GetRequiredService<DtoJsonConverterFactory>();
        JsonSerializerOptions options = new();
        options.Converters.Add(converter);
        IShipCall shipCall = await HttpContext.Request.ReadFromJsonAsync<IShipCall>(options);
        Controller controller = (Controller)HttpContext.RequestServices.GetRequiredService<IDemoController>();
        controller.ControllerContext = ControllerContext;
        await ((IDemoController)controller).Commit(shipCall);
    }

}
