using Net.Leksi.RestContract;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Net.Mime;

namespace DtoKit.Demo;

public interface IConnector
{
    [RoutePath("/shipCalls/{filter}/{amount:double}/{date}?qq")]
    [HttpMethodGet]
    [Authorization(Roles = "1, 2, 3")]
    Task GetShipCalls(DateTime date, double amount, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list);

    [RoutePath("/form")]
    [HttpMethodPost]
    Task Commit([Body] IShipCall shipCall);

}
