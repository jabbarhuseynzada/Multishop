using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryService;
public interface ICategoryService
{
    Task CreateAsync(CreateCategoryDto createCategoryDto);
    Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteAsync(string id);
    // Retrieves
    // by id
    Task<ResultCategoryDto> GetByIdAsync(string id);
    
    // All
    Task<List<ResultCategoryDto>> GetAllAsync();
}
