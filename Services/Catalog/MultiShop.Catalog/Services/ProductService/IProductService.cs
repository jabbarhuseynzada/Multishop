using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductService;
public interface IProductService
{
    Task CreateAsync(CreateProductDto createProductDto);
    Task UpdateAsync(UpdateProductDto updateProductDto);
    Task DeleteAsync(string id);

    // Retrieves
    // by id
    Task<ResultProductDto> GetByIdAsync(string id);
    Task<ResultProductWithCategoryDto> GetByIdWithCategoryAsync(string id);

    // All
    Task<List<ResultProductDto>> GetAllAsync();
    Task<List<ResultProductWithCategoryDto>> GetAllProductsWithCategoryAsync();
    Task<List<ResultProductWithCategoryDto>> GetAllByCategoryIdAsync(string categoryId);
}
