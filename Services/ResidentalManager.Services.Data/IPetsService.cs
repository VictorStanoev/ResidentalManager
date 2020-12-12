namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Pets;

    public interface IPetsService
    {
        Task CreateAsync(PetsInputModel inputModel);

        PetsInputModel AddFeeDropDown();

        IEnumerable<PetsViewModel> GetAll(int propertyId);
    }
}
