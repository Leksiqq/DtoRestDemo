//------------------------------
// Connector base
// DtoKit.Demo.DemoConnectorBase
// (Generated automatically)
//------------------------------
using Microsoft.Extensions.DependencyInjection;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace DtoKit.Demo;

public class DemoConnectorBase
{
    private readonly HttpConnector _httpConnector;
    public DemoConnectorBase(HttpConnector httpConnector)
    {
        _httpConnector = httpConnector;
    }

    public Task<HttpResponseMessage> GetShipCalls(DateTime date, Double amount, ShipCallsFilter filter)
    {
        DtoJsonConverterFactory getConverter = _httpConnector.Services.GetRequiredService<DtoJsonConverterFactory>();
        getConverter.KeysProcessing = KeysProcessing.OnlyKeys;
        JsonSerializerOptions getOptions = new();
        getOptions.Converters.Add(getConverter);
        string _date = HttpUtility.UrlEncode(JsonSerializer.Serialize(date, getOptions));
        string _amount = HttpUtility.UrlEncode(JsonSerializer.Serialize(amount, getOptions));
        string _filter = HttpUtility.UrlEncode(JsonSerializer.Serialize(filter, getOptions));
        string route = $"/shipCalls/{_filter}/{_amount}/{_date}";
        HttpRequestMessage httpRequest = new(HttpMethod.Get, route);

        return _httpConnector.SendAsync(httpRequest);

    }

    public Task<HttpResponseMessage> Commit(IShipCall shipCall)
    {
        string route = $"/form";
        HttpRequestMessage httpRequest = new(HttpMethod.Post, route);

        DtoJsonConverterFactory postConverter = _httpConnector.Services.GetRequiredService<DtoJsonConverterFactory>();
        JsonSerializerOptions postOptions = new();
        postOptions.Converters.Add(postConverter);
        httpRequest.Content = JsonContent.Create(shipCall, typeof(IShipCall), default, postOptions);
        return _httpConnector.SendAsync(httpRequest);

    }

}
