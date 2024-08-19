using System.Reflection;
using CNewsProject.StaticTempData;

namespace CNewsProject.Service;

public interface IFillStaticHelper
{
    void MapRatesToStaticClass(Rates rates);
}

public class FillStaticHelper : IFillStaticHelper
{
    public void MapRatesToStaticClass(Rates rates)
    {
        var sourceProperties = rates.GetType().GetProperties();

        var destinationProperties = typeof(ExchangeRateData)
            .GetProperties(BindingFlags.Public | BindingFlags.Static);

        foreach (var prop in sourceProperties)
        {
            
            var destinationProp = destinationProperties
                .Single(p => p.Name == prop.Name);
            
            destinationProp.SetValue(null, prop.GetValue(rates));
        }
    }
}
