using System.Collections.Generic;
using System.Linq;
using TariffComparison.Service.DataAccess;
using TariffComparison.Service.Models;

namespace TariffComparison.Service
{
    public class ProductComparisonService : IProductComparisonService
    {
        private readonly IProductRepository _repository;
        private readonly IProductCostCalculator _costCalculator;

        public ProductComparisonService(IProductRepository repository, IProductCostCalculator costCalculator)
        {
            _repository = repository;
            _costCalculator = costCalculator;
        }

        public IEnumerable<ProductCost> CompareProducts(int consumptionKwh)
        {
            var products = _repository.GetProducts();
            var result = new List<ProductCost>();
            foreach(var product in products)
            {
                result.Add(_costCalculator.CalculateYearlyCost(product, consumptionKwh));
            }

            return result.OrderBy(x => x.Cost);
        }
    }
}
