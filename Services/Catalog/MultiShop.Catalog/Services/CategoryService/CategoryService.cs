using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Enitites;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryService;
public class CategoryService : ICategoryService
{ 
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Category> _categoryCollection;
    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto == null)
        {
            throw new ArgumentNullException(nameof(createCategoryDto), "CreateCategoryDto cannot be null");
        }
        if (string.IsNullOrEmpty(createCategoryDto.CategoryName))
        {
            throw new ArgumentException("Category name cannot be null or empty", nameof(createCategoryDto.CategoryName));
        }
        var category = _mapper.Map<Category>(createCategoryDto);
        await _categoryCollection.InsertOneAsync(category);
    }

    public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        if (updateCategoryDto == null)
        {
            throw new ArgumentNullException(nameof(updateCategoryDto), "UpdateCategoryDto cannot be null");
        }
        if (string.IsNullOrEmpty(updateCategoryDto.CategoryId))
        {
            throw new ArgumentException("Category ID cannot be null or empty", nameof(updateCategoryDto.CategoryId));
        }
        var category = _mapper.Map<Category>(updateCategoryDto);
        var filter = Builders<Category>.Filter.Eq(c => c.CategoryId, updateCategoryDto.CategoryId);
        await _categoryCollection.ReplaceOneAsync(filter, category);
    }
    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Category ID cannot be null or empty", nameof(id));
        }
        var filter = Builders<Category>.Filter.Eq(c => c.CategoryId, id);
        await _categoryCollection.DeleteOneAsync(filter);
    }
    public async Task<ResultCategoryDto> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Category ID cannot be null or empty", nameof(id));
        }
        var filter = Builders<Category>.Filter.Eq(c => c.CategoryId, id);
        var category = await _categoryCollection.Find(filter).FirstOrDefaultAsync();
        return _mapper.Map<ResultCategoryDto>(category);
    }
    public async Task<List<ResultCategoryDto>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(_ => true).ToListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(categories);
    }
}
