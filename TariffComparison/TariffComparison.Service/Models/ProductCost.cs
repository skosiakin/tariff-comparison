
namespace TariffComparison.Service.Models
{
    public class ProductCost
    {
        public ProductCost(string name, decimal cost)
        {
            TariffName = name;
            Cost = cost;
        }
        public string TariffName { get; set; }
        public decimal Cost { get; set; }
    }
}
