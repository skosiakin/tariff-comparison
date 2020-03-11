using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Service;
using TariffComparison.Service.Models;

namespace TariffComparison.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductComparisonService _comparisonService;

        public ProductsController(IProductComparisonService comparisonService)
        {
            _comparisonService = comparisonService;
        }

        // GET: api/Products/5
        // In real life this method will be async
        [HttpGet("{consumptionKwh}", Name = "Get")]
        public IActionResult Get(int consumptionKwh)
        {
            if(consumptionKwh < 0)
            {
                return BadRequest();
            }

            var productPrices = _comparisonService.CompareProducts(consumptionKwh);
            return Ok(productPrices);
        }
    }
}
