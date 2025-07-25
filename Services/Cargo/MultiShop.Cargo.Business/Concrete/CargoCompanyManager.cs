using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.Business.Concrete;
public class CargoCompanyManager(ICargoCompanyDal cargoCompanyDal) : ICargoCompanyService
{
    private readonly ICargoCompanyDal _cargoCompanyDal = cargoCompanyDal;

    public async Task AddTAsync(CargoCompany entity)
    {
        await _cargoCompanyDal.AddAsync(entity);
    }

    public async Task DeleteTAsync(int id)
    {
        await _cargoCompanyDal.DeleteAsync(id);
    }

    public async Task<bool> ExistsTAsync(int id)
    {
        return await _cargoCompanyDal.ExistsAsync(id);
    }

    public async Task<IEnumerable<CargoCompany>> GetAllTAsync()
    {
        var cargoCompanies = await _cargoCompanyDal.GetAllAsync();
        return cargoCompanies;
    }

    public async Task<CargoCompany> GetTByIdAsync(int id)
    {
        var entity = await _cargoCompanyDal.GetByIdAsync(id);
        return entity;
    }

    public async Task UpdateTAsync(CargoCompany entity)
    {
        await _cargoCompanyDal.UpdateAsync(entity);
    }
}
