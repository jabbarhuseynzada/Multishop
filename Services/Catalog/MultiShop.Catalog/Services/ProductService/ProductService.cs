using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Enitites;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductService;
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;

    public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="createProductDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task CreateAsync(CreateProductDto createProductDto)
    {
        if (createProductDto == null)
        {
            throw new ArgumentNullException(nameof(createProductDto), "CreateProductDto cannot be null");
        }
        if (string.IsNullOrEmpty(createProductDto.ProductName))
        {
            throw new ArgumentException("Product name cannot be null or empty", nameof(createProductDto.ProductName));
        }
        if (string.IsNullOrEmpty(createProductDto.CategoryId))
        {
            throw new ArgumentException("Category ID cannot be null or empty", nameof(createProductDto.CategoryId));
        }
        var product = _mapper.Map<Product>(createProductDto);
        await _productCollection.InsertOneAsync(product);
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="updateProductDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task UpdateAsync(UpdateProductDto updateProductDto)
    {
        if (updateProductDto == null)
        {
            throw new ArgumentNullException(nameof(updateProductDto), "UpdateProductDto cannot be null");
        }
        if (string.IsNullOrEmpty(updateProductDto.ProductId))
        {
            throw new ArgumentException("Product ID cannot be null or empty", nameof(updateProductDto.ProductId));
        }
        var filter = Builders<Product>.Filter.Eq(p => p.ProductId, updateProductDto.ProductId);
       
        var product = _mapper.Map<Product>(updateProductDto);
        await _productCollection.ReplaceOneAsync(filter, product);
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Product ID cannot be null or empty", nameof(id));
        }
        var filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);
        var result = await _productCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found");
        }
    }

    /// <summary>
    /// Retrieves all products from the data source asynchronously.
    /// </summary>
    /// <remarks>This method fetches all product records from the underlying data source and maps them to 
    /// <see cref="ResultProductDto"/> objects. The returned list will be empty if no products are found.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of  <see
    /// cref="ResultProductDto"/> objects representing the products.</returns>
    public async Task<List<ResultProductDto>> GetAllAsync()
    {
        var products = await _productCollection.Find(_ => true).ToListAsync();
        return _mapper.Map<List<ResultProductDto>>(products);
    }

    /// <summary>
    /// Retrieves all products by category ID.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<List<ResultProductWithCategoryDto>> GetAllByCategoryIdAsync(string categoryId)
    {
        if (string.IsNullOrEmpty(categoryId))
        {
            throw new ArgumentException("Category ID cannot be null or empty", nameof(categoryId));
        }
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        var products = await _productCollection.Find(filter).ToListAsync();
        return _mapper.Map<List<ResultProductWithCategoryDto>>(products);
    }

    /// <summary>
    /// Retrieves all products with their associated categories.
    /// </summary>
    /// <returns></returns>
    public async Task<List<ResultProductWithCategoryDto>> GetAllProductsWithCategoryAsync()
    {
        var products = await _productCollection.Find(_ => true).ToListAsync();
        var productDtos = _mapper.Map<List<ResultProductWithCategoryDto>>(products);
        foreach (var productDto in productDtos)
        {
            if (!string.IsNullOrEmpty(productDto.CategoryId))
            {
                var category = await _categoryCollection.Find(c => c.CategoryId == productDto.CategoryId).FirstOrDefaultAsync();
                productDto.Category = _mapper.Map<ResultCategoryDto>(category);
            }
        }
        return productDtos;
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<ResultProductDto> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Product ID cannot be null or empty", nameof(id));
        }
        var filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
        return _mapper.Map<ResultProductDto>(product);
    }

    /// <summary>
    /// Retrieves a product by its ID along with its associated category.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<ResultProductWithCategoryDto> GetByIdWithCategoryAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Product ID cannot be null or empty", nameof(id));
        }
        var filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found");
        }
        var productDto = _mapper.Map<ResultProductWithCategoryDto>(product);
        if (!string.IsNullOrEmpty(productDto.CategoryId))
        {
            var category = await _categoryCollection.Find(c => c.CategoryId == productDto.CategoryId).FirstOrDefaultAsync();
            productDto.Category = _mapper.Map<ResultCategoryDto>(category);
        }
        return productDto;
    }

}
