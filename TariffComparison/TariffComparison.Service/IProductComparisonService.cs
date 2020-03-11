using System.Collections.Generic;
using TariffComparison.Service.Models;

namespace TariffComparison.Service
{
    public interface IProductComparisonService
    {
        IEnumerable<ProductCost> CompareProducts(int consumptionKwh);
    }
}
