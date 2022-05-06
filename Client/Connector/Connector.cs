using Net.Leksi.RestContract;
using System.Collections.ObjectModel;

namespace DtoKit.Demo;

public class Connector : DemoConnectorBase,  IConnector
{
    public Connector(HttpConnector httpConnector) : base(httpConnector) { }
    public async Task GetShipCalls(DateTime date, double amount, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list)
    {
        HttpResponseMessage response = await base.GetShipCalls(date, amount, filter);
        Console.WriteLine(response.StatusCode);
    }

    public async Task Commit(IShipCall shipCall)
    {
        HttpResponseMessage response = await base.Commit(shipCall);
        Console.WriteLine(response.StatusCode);
    }
}
