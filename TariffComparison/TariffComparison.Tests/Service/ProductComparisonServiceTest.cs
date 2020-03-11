using TariffComparison.Service;
using TariffComparison.Service.DataAccess;
using Xunit;
using Moq;
using AutoFixture;
using TariffComparison.Service.Models;
using System.Linq;
using FluentAssertions;

namespace TariffComparison.Tests.Service
{
    public class ProductComparisonServiceTest
    {
        private Fixture _fixture;

        private readonly Mock<IProductRepository> _repository;
        private readonly Mock<IProductCostCalculator> _costCalculator;

        private readonly ProductComparisonService _sut;

        public ProductComparisonServiceTest()
        {
            _fixture = new Fixture();

            _repository = new Mock<IProductRepository>();
            _costCalculator = new Mock<IProductCostCalculator>();

            _fixture.Inject(_repository.Object);
            _fixture.Inject(_costCalculator.Object);

            _sut = _fixture.Create<ProductComparisonService>();
        }

        [Fact]
        public void CompareProducts_should_calculate_yearly_cost_for_each_product()
        {
            // Arrange
            int consumption = _fixture.Create<int>();
            var products = _fixture.CreateMany<Product>().ToList();
            _repository.Setup(x => x.GetProducts()).Returns(products);
            _costCalculator.Setup(x => x.CalculateYearlyCost(It.IsAny<Product>(), consumption)).Returns(_fixture.Create<ProductCost>);

            // Act
            var result = _sut.CompareProducts(consumption);

            // Assert
            _costCalculator.Verify(x => x.CalculateYearlyCost(It.IsAny<Product>(), consumption), Times.Exactly(products.Count));
        }

        [Fact]
        public void CompareProducts_should_sort_results_by_price()
        {
            // Arrange
            int consumption = _fixture.Create<int>();
            var products = _fixture.CreateMany<Product>(2).ToList();
            _repository.Setup(x => x.GetProducts()).Returns(products);
            var cost1 = new ProductCost(_fixture.Create<string>(), 500);
            var cost2 = new ProductCost(_fixture.Create<string>(), 200);
            _costCalculator.SetupSequence(x => x.CalculateYearlyCost(It.IsAny<Product>(), consumption))
                .Returns(cost1)
                .Returns(cost2);

            // Act
            var result = _sut.CompareProducts(consumption).ToList();

            // Assert
            result[0].Should().Be(cost2);
            result[1].Should().Be(cost1);
        }
    }
}
