using Net.Leksi.RestContract;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Net.Mime;

namespace DtoKit.Demo;

public interface IConnector
{
    [RoutePath("/shipCalls/{filter}/{amount:double}/{date}?qq")]
    [HttpMethodGet]
    Task GetShipCalls(DateTime date, double amount, ShipCallsFilter filter, ObservableCollection<IShipCallForList> list);

    [RoutePath("/form")]
    [HttpMethodPost]
    [ContentType(typeof(MultipartFormDataContent))]
    Task Commit(
        [Content(0)] 
        string value0,
        [Content(1)][SetFilename]
        string fileName1,
        [Content(1)][SetContentType]
        Type contentType1,
        [Content(1)]
        Stream stream,
        [Content(2)]
        double amount
        );

}
