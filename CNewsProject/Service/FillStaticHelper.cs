using System.Reflection;
using CNewsProject.StaticTempData;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CNewsProject.Service;

public interface IFillObjectHelper
{
    void MapRatesToStaticClass(Rates rates);
}

public class FillObjectHelper : IFillObjectHelper
{
    public void MapRatesToStaticClass(Rates rates) // No longer needed. 
    {
        // var sourceProperties = rates.GetType().GetProperties();
        //
        // var destinationProperties = typeof(ExchangeRateData)
        //     .GetProperties(BindingFlags.Public | BindingFlags.Static);
        //
        // foreach (var prop in sourceProperties)
        // {
        //     var destinationProp = destinationProperties
        //         .Single(p => p.Name == prop.Name);
        //     
        //     destinationProp.SetValue(null, prop.GetValue(rates));
        // }
    }
}
