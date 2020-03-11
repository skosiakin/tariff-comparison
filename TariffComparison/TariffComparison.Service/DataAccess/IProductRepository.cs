using System;
using System.Collections.Generic;
using System.Text;
using TariffComparison.Service.Models;

namespace TariffComparison.Service.DataAccess
{
    public interface IProductRepository
    {
        IList<Product> GetProducts();
    }
}
