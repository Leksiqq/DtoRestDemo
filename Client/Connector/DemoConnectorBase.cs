//------------------------------
// Connector base class DtoKit.Demo.DemoConnectorBase (Generated automatically)
//------------------------------
using Microsoft.Extensions.DependencyInjection;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using System.Text.Json;
using System.Web;

namespace DtoKit.Demo;

public class DemoConnectorBase
{
    private readonly HttpConnector _httpconnector;
    public DemoConnectorBase(HttpConnector httpconnector)
    {
        _httpconnector = httpconnector;
    }
    public Task<HttpResponseMessage> GetShipCalls(DateTime date, Double amount, ShipCallsFilter filter)
    {
        JsonSerializerOptions? jsonserializeroptions = null;
        DtoJsonConverterFactory dtojsonconverterfactory = _httpconnector.Services.GetRequiredService<DtoJsonConverterFactory>();
        jsonserializeroptions = new();
        jsonserializeroptions.Converters.Add(dtojsonconverterfactory);
        String _date = HttpUtility.UrlEncode(JsonSerializer.Serialize(date, jsonserializeroptions));
        String _amount = HttpUtility.UrlEncode(JsonSerializer.Serialize(amount, jsonserializeroptions));
        String _filter = HttpUtility.UrlEncode(JsonSerializer.Serialize(filter, jsonserializeroptions));
        String urlpath = $"/shipCalls/{_filter}/{_amount}/{_date}";
        HttpRequestMessage httprequestmessage = new(HttpMethod.Get, urlpath);
        return _httpconnector.SendAsync(httprequestmessage);
    }
}
