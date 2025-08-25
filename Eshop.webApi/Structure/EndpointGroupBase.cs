namespace Eshop.webApi.Structure;

public abstract class EndpointGroupBase
{
    public abstract string Prefix {  get; }
    public abstract void Map(WebApplication app);
}