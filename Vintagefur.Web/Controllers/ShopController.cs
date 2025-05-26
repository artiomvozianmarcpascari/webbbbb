using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.BusinessLogic;
using Vintagefur.Domain.Models;
using Vintagefur.Domain.DTO;

namespace Vintagefur.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProduct _productService;

        public ShopController()
        {
            _productService = BusinessLogicFactory.Instance.GetProductBL();
        }

        // GET: Shop
        public ActionResult Index()
        {
            var productsDTO = _productService.GetAllProducts();
            var products = ConvertToProductModels(productsDTO);
            return View(products);
        }

        // GET: Shop/Category/5
        public ActionResult Category(int id)
        {
            var productsDTO = _productService.GetProductsByCategory(id);
            var products = ConvertToProductModels(productsDTO);
            return View("Index", products);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            var productDTO = _productService.GetProductById(id);
            if (productDTO == null)
            {
                return HttpNotFound();
            }
            var product = ConvertToProductModel(productDTO);
            return View(product);
        }

        // Вспомогательные методы для конвертации DTO в модели
        private List<Product> ConvertToProductModels(List<ProductDTO> productsDTO)
        {
            return productsDTO.Select(ConvertToProductModel).ToList();
        }

        private Product ConvertToProductModel(ProductDTO dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                IsAvailable = dto.IsAvailable,
                MaterialId = dto.MaterialId,
                StyleId = dto.StyleId
            };
        }
    }
} 