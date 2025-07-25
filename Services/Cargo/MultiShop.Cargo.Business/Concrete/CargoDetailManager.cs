using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.Business.Concrete;
public class CargoDetailManager(ICargoDetailDal cargoDetailDal) : ICargoDetailService
{
    private readonly ICargoDetailDal _cargoDetailDal = cargoDetailDal;

    public async Task AddTAsync(CargoDetail entity)
    {
        await _cargoDetailDal.AddAsync(entity);
    }

    public async Task DeleteTAsync(int id)
    {
        await _cargoDetailDal.DeleteAsync(id);
    }

    public async Task<bool> ExistsTAsync(int id)
    {
        return await _cargoDetailDal.ExistsAsync(id);
    }

    public async Task<IEnumerable<CargoDetail>> GetAllTAsync()
    {
        var details = await _cargoDetailDal.GetAllAsync();
        return details;
    }

    public async Task<CargoDetail> GetTByIdAsync(int id)
    {
        var detail = await _cargoDetailDal.GetByIdAsync(id);
        return detail;
    }

    public async Task UpdateTAsync(CargoDetail entity)
    {
        await _cargoDetailDal.UpdateAsync(entity);
    }
}
