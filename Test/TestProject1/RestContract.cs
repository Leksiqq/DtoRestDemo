using DtoKit.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text.Json;
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
            }).Build();
        Trace.Listeners.Add(new ConsoleTraceListener());
        Trace.AutoFlush = true;
    }

    [Test]
    public void GenerateRestSources()
    {
        SourceGenerator sg = new(_host.Services.GetRequiredService<DtoServiceProvider>());

        string result = sg.GenerateHelpers<IConnector>("DtoKit.Demo.IDemoController", "DtoKit.Demo.DemoControllerProxy", "DtoKit.Demo.DemoConnectorBase");
        Console.WriteLine(result);
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
