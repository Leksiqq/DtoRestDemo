using Net.Leksi.RestContract;
using System.Collections.ObjectModel;

namespace DtoKit.Demo;

public interface IConnector
{
    [RoutePath("/shipCalls/{filter}/{amount:double}/{date}")]
    [HttpMethodGet]
    [Authorization(Roles = "1, 2, 3")]
    Task GetShipCalls(DateTime date, double amount, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list);

    [RoutePath("/form")]
    [HttpMethodPost]
    Task Commit([Body] IShipCall shipCall);

}
