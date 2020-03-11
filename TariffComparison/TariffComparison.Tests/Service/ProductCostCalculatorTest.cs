using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using TariffComparison.Service;
using TariffComparison.Service.Models;
using Xunit;

namespace TariffComparison.Tests.Service
{
    public class ProductCostCalculatorTest
    {
        private Fixture _fixture;
        private readonly ProductCostCalculator _sut;

        public ProductCostCalculatorTest()
        {
            _fixture = new Fixture();
            _sut = _fixture.Create<ProductCostCalculator>();
        }

        [Fact]
        public void CalculateYearlyCost_should_add_product_name_to_result()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { PriceForKwh = 0.22m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 100);

            // Assert
            result.TariffName.Should().Be(product.Name);
        }

        [Fact]
        public void CalculateYearlyCost_should_return_base_cost_for_zero_consumption()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { PriceForKwh = 0.22m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 0);

            // Assert
            result.Cost.Should().Be(product.BaseYearlyCost);
        }

        [Fact]
        public void CalculateYearlyCost_should_calculate_cost_for_product_with_one_tariff()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { PriceForKwh = 0.22m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 3500); // 60 + 0.22*3500

            // Assert
            result.Cost.Should().Be(60 + 0.22m * 3500);
        }

        [Fact]
        public void CalculateYearlyCost_should_calculate_cost_for_product_with_two_tariffs_based_on_first_tariff()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { ConsumptionToKwh = 4000, PriceForKwh = 0.22m  },
                        new ElectricityPrice { ConsumptionFromKwh = 4000, PriceForKwh = 0.5m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 3500); // 60 + 0.22*3500

            // Assert
            result.Cost.Should().Be(60 + 0.22m * 3500);
        }

        [Fact]
        public void CalculateYearlyCost_should_calculate_cost_for_product_with_two_tariffs_based_on_first_tariff_edge_cae()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { ConsumptionToKwh = 4000, PriceForKwh = 0.22m  },
                        new ElectricityPrice { ConsumptionFromKwh = 4000, PriceForKwh = 0.5m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 4000); // 60 + 0.22*4000

            // Assert
            result.Cost.Should().Be(60 + 0.22m * 4000);
        }

        [Fact]
        public void CalculateYearlyCost_should_calculate_cost_for_product_with_two_tariffs()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { ConsumptionToKwh = 4000, PriceForKwh = 0.22m  },
                        new ElectricityPrice { ConsumptionFromKwh = 4000, PriceForKwh = 0.5m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 5000); // 60 + 0.22*4000 + 0.5*1000

            // Assert
            result.Cost.Should().Be(60 + 0.22m * 4000 + 0.5m * 1000);
        }

        [Fact]
        public void CalculateYearlyCost_should_calculate_cost_for_product_with_three_tariffs()
        {
            // Arrange
            var product = new Product
            {
                Name = "Basic electricity tariff",
                BaseYearlyCost = 5 * 12,
                Prices = new List<ElectricityPrice> {
                        new ElectricityPrice { ConsumptionToKwh = 4000, PriceForKwh = 0.22m  },
                        new ElectricityPrice { ConsumptionFromKwh = 4000, ConsumptionToKwh = 6000, PriceForKwh = 0.5m  },
                        new ElectricityPrice { ConsumptionFromKwh = 6000, PriceForKwh = 1.2m  }
                    }
            };

            // Act
            var result = _sut.CalculateYearlyCost(product, 6500); // 60 + 0.22*4000 + 0.5*2000 + 500*1.2

            // Assert
            result.Cost.Should().Be(60 + 0.22m * 4000 + 0.5m * 2000 + 1.2m * 500);
        }
    }
}
