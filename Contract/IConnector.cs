using Net.Leksi.RestContract;
using System.Collections.ObjectModel;

namespace DtoKit.Demo;

public interface IConnector
{
    [RoutePath("/shipCalls/{filter}/{count:int}/{date}")]
    [HttpMethodGet]
    Task GetShipCalls(DateTime date, int count, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list);
}
