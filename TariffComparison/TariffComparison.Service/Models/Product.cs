using System.Collections.Generic;

namespace TariffComparison.Service.Models
{
    public class Product
    {
        public Product()
        {
            Prices = new List<ElectricityPrice>();
        }

        public string Name { get; set; }
        public decimal BaseYearlyCost { get; set; }
        public List<ElectricityPrice> Prices { get; set; }
     }
}
