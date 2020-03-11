using System;
using TariffComparison.Service.Models;

namespace TariffComparison.Service
{
    public interface IProductCostCalculator
    {
        ProductCost CalculateYearlyCost(Product product, int consumptionKwh);
    }
}
