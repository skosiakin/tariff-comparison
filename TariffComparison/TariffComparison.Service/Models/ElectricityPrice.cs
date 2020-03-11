
namespace TariffComparison.Service.Models
{
    public class ElectricityPrice
    {
        public int ConsumptionFromKwh { get; set; }
        public int? ConsumptionToKwh { get; set; }
        public decimal PriceForKwh { get; set; }
    }
}
