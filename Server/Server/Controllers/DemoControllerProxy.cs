//------------------------------
// MVC Controller proxy class DtoKit.Demo.DemoControllerProxy (Generated automatically)
//------------------------------
using Microsoft.AspNetCore.Mvc;
using Net.Leksi.Dto;
using System.Text.Json;

namespace DtoKit.Demo;

public class DemoControllerProxy : Controller
{
    [Route("/shipCalls/{filter}/{count:int}/{date}")]
    [HttpGet]
    public async Task GetShipCalls(String date, Int32 count, String filter)
    {
        DtoJsonConverterFactory dtojsonconverterfactory = HttpContext.RequestServices.GetRequiredService<DtoJsonConverterFactory>();
        JsonSerializerOptions jsonserializeroptions = new();
        jsonserializeroptions.Converters.Add(dtojsonconverterfactory);
        DateTime _date = JsonSerializer.Deserialize<DateTime>(date, jsonserializeroptions);
        ShipCallsFilter _filter = JsonSerializer.Deserialize<ShipCallsFilter>(filter, jsonserializeroptions);
        Controller controller = (Controller)HttpContext.RequestServices.GetRequiredService<IDemoController>();
        controller.ControllerContext = ControllerContext;
        await ((IDemoController)controller).GetShipCalls(_date, count, _filter);
    }
}

