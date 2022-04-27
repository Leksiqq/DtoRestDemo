using System.Collections.ObjectModel;

namespace DtoKit.Demo;

public class Connector : DemoConnectorBase,  IConnector
{
    public Connector(IServiceProvider services) : base(services) { }
    public async Task GetShipCalls(DateTime date, int count, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list)
    {
        HttpResponseMessage response = await base.GetShipCalls(date, count, filter);
        Console.WriteLine(response.StatusCode);
    }
}
