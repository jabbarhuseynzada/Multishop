using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.Business.Concrete;
public class CargoCustomerManager(ICargoCustomerDal cargoCustomerDal) : ICargoCustomerService
{
    private readonly ICargoCustomerDal _cargoCustomerDal = cargoCustomerDal;

    public async Task AddTAsync(CargoCustomer entity)
    {
        await _cargoCustomerDal.AddAsync(entity);
    }

    public async Task DeleteTAsync(int id)
    {
        await _cargoCustomerDal.DeleteAsync(id);
    }

    public async Task<bool> ExistsTAsync(int id)
    {
        return await _cargoCustomerDal.ExistsAsync(id);
    }

    public async Task<IEnumerable<CargoCustomer>> GetAllTAsync()
    {
        var customers = await _cargoCustomerDal.GetAllAsync();
        return customers;
    }

    public async Task<CargoCustomer> GetTByIdAsync(int id)
    {
        var customer = await _cargoCustomerDal.GetByIdAsync(id);
        return customer;
    }

    public async Task UpdateTAsync(CargoCustomer entity)
    { 
        await _cargoCustomerDal.UpdateAsync(entity);
    }
}
