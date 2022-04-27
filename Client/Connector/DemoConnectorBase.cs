//------------------------------
// Connector base class DtoKit.Demo.DemoConnectorBase (Generated automatically)
//------------------------------
using Microsoft.Extensions.DependencyInjection;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using System.Text.Json;
using System.Web;

namespace DtoKit.Demo;

public class DemoConnectorBase : BaseConnector
{
    public DemoConnectorBase(IServiceProvider services) : base(services) { }
    public Task<HttpResponseMessage> GetShipCalls(DateTime date, Int32 count, ShipCallsFilter filter)
    {
        JsonSerializerOptions? jsonserializeroptions = null;
        DtoJsonConverterFactory dtojsonconverterfactory = Services.GetRequiredService<DtoJsonConverterFactory>();
        jsonserializeroptions = new();
        jsonserializeroptions.Converters.Add(dtojsonconverterfactory);
        String _date = HttpUtility.UrlEncode(JsonSerializer.Serialize(date, jsonserializeroptions));
        String _count = HttpUtility.UrlEncode(JsonSerializer.Serialize(count, jsonserializeroptions));
        String _filter = HttpUtility.UrlEncode(JsonSerializer.Serialize(filter, jsonserializeroptions));
        String urlpath = $"/shipCalls/{_filter}/{_count}/{_date}";
        HttpRequestMessage httprequestmessage = new(HttpMethod.Get, urlpath);
        return SendAsync(httprequestmessage);
    }
}

