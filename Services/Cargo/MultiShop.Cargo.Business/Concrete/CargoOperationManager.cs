using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.Business.Concrete;
public class CargoOperationManager(ICargoOperationDal cargoOperationDal) : ICargoOperationService
{
    private readonly ICargoOperationDal _cargoOperationDal = cargoOperationDal;

    public async Task AddTAsync(CargoOperation entity)
    {
        await _cargoOperationDal.AddAsync(entity);
    }

    public async Task DeleteTAsync(int id)
    {
        await _cargoOperationDal.DeleteAsync(id);
    }

    public async Task<bool> ExistsTAsync(int id)
    {
        return await _cargoOperationDal.ExistsAsync(id);
    }

    public async Task<IEnumerable<CargoOperation>> GetAllTAsync()
    {
        var operations = await _cargoOperationDal.GetAllAsync();
        return operations;
    }

    public async Task<CargoOperation> GetTByIdAsync(int id)
    {
        var operation = await _cargoOperationDal.GetByIdAsync(id);
        return operation;
    }

    public async Task UpdateTAsync(CargoOperation entity)
    {
        await _cargoOperationDal.UpdateAsync(entity);
    }
}
