using DtoKit.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Leksi.RestContract;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(serviceCollection =>
    {
        Setup.Configure(serviceCollection);
        serviceCollection.AddTransient<HelpersBuilder>();
    }).Build();
host.RunAsync();

HttpConnector httpConnector = new(host.Services);
httpConnector.BaseAddress = new Uri("https://localhost:7145");
Connector connector = new(httpConnector);

ShipCallsFilter filter = new() { PortName = "SPB-BRONKA", VesselName = "FINNSEA", Voyage = "FIS22001"};


await connector.GetShipCalls(DateTime.Now, 2.3, filter, null);
await connector.Commit(host.Services.GetRequiredService<IShipCall>());
