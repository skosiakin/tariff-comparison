using System;
using System.Linq;
using TariffComparison.Service.Models;

namespace TariffComparison.Service
{
    public class ProductCostCalculator : IProductCostCalculator
    {
        public ProductCost CalculateYearlyCost(Product product, int consumptionKwh)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var cost = product.BaseYearlyCost;
            foreach(var price in product.Prices.OrderBy(x => x.ConsumptionFromKwh))
            {
                // we sorted prices by start/end range
                // so if consumption already smaller then low level of current range, we could stop calculation
                if(consumptionKwh < price.ConsumptionFromKwh)
                {
                    break;
                }

                if(price.ConsumptionToKwh.HasValue)
                {
                    // for example, we have tariff from 1000 to 3000
                    // if consumption is 2500, we need to calculate cost for 2500 - 1000 = 1500 kwh
                    // if consumption is 3500, we need to calculate cost for 3000 - 1000 = 2000 kwh
                    // so here we calculate upperLimit as min of consumption and price.ConsumptionToKwh
                    var upperLimit = Math.Min(consumptionKwh, price.ConsumptionToKwh.Value);
                    cost += (upperLimit - price.ConsumptionFromKwh) * price.PriceForKwh;
                }
                else // last tariff without upper limit
                {
                    cost += (consumptionKwh - price.ConsumptionFromKwh) * price.PriceForKwh;
                }
            }

            return new ProductCost(product.Name, cost);
        }
    }
}
