namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Pets;

    public interface IPetsService
    {
        Task CreateAsync(PetsInputModel inputModel);

        PetsInputModel AddFeeDropDown(int realEstateId);

        IEnumerable<PetsViewModel> GetAll(int propertyId);

        PetsViewModel Get(int id, int realEstateId);

        Task Update(int id, PetsInputModel model);

        Task DeleteAsync(int id);
    }
}
