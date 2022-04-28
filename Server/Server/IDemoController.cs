//------------------------------
// MVC Controller interface DtoKit.Demo.IDemoController (Generated automatically)
//------------------------------

namespace DtoKit.Demo;

public interface IDemoController
{
    Task GetShipCalls(DateTime date, Double amount, ShipCallsFilter filter);
}

