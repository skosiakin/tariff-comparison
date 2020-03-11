using System.Collections.Generic;
using TariffComparison.Service.Models;

namespace TariffComparison.Service.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        // Here we supposed to read products from db in real life
        // Also, in real life, this method will be async
        public IList<Product> GetProducts()
        {
            return new List<Product> { 
                new Product { 
                    Name = "Basic electricity tariff",
                    BaseYearlyCost = 5 * 12,
                    Prices = new List<ElectricityPrice> { 
                        new ElectricityPrice { PriceForKwh = 0.22m  } 
                    }
                },
                new Product {
                    Name = "Packaged tariff",
                    BaseYearlyCost = 800,
                    Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { ConsumptionToKwh = 4000  },
                        new ElectricityPrice { ConsumptionFromKwh = 4000, PriceForKwh = 0.3m  }
                    }
                }
            };
        }
    }
}
