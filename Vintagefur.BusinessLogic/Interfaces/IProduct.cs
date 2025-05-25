using System.Collections.Generic;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
        List<ProductDTO> GetProductsByCategory(int categoryId);
        List<ProductDTO> SearchProducts(string searchTerm);
        bool CreateProduct(ProductDTO product);
        bool UpdateProduct(ProductDTO product);
        bool DeleteProduct(int id);
    }
} 