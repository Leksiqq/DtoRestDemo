using DtoKit.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Leksi.RestContract;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

public class RestContract
{
    private static IHost _host;

    static RestContract()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(serviceCollection =>
            {
                Setup.Configure(serviceCollection);
                serviceCollection.AddTransient<HelpersBuilder>();
            }).Build();
        Trace.Listeners.Add(new ConsoleTraceListener());
        Trace.AutoFlush = true;
    }

    [Test]
    public async Task BuildRestHelpers()
    {
        HelpersBuilder codeGenerator = _host.Services.GetRequiredService<HelpersBuilder>();

        await codeGenerator.BuildHelpers<IConnector>("DtoKit.Demo.IDemoController", "DtoKit.Demo.DemoControllerProxy", "DtoKit.Demo.DemoConnectorBase");
    }

    [Test]
    public void ShipCallsFilterToJson()
    {
        ShipCallsFilter filter = new() { Voyage = "DER22001", PortName = "SPB-BRONKA", VesselName = "FINNSEA", From = DateTime.Parse("2022-01-01"), To = DateTime.Now };
        string json = JsonSerializer.Serialize(filter);
        string path = "/shipCalls/" + HttpUtility.UrlEncode(json) + "/23";
        Console.WriteLine(path);
    }
}
