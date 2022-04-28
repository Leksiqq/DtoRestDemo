using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DtoKit.Demo;

public class DemoController : Controller, IDemoController
{
    public async Task GetShipCalls(DateTime date, double amount, ShipCallsFilter filter)
    {
        Console.WriteLine($"{date}, {amount}, {JsonSerializer.Serialize(filter, new JsonSerializerOptions { WriteIndented = true })}");
        await Results.Ok().ExecuteAsync(this.HttpContext);
    }
}
