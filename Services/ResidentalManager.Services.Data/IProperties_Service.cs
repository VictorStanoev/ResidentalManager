namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Properties_;

    public interface IProperties_Service
    {
        Task CreateAsync(CreatePropertiesInputModel inputModel);

        IEnumerable<AllPropertiesViewModel> GetAll(int realEstateId);

        CreatePropertiesInputModel AddFee(int realEstateId);

        Task DeleteAsync(int id);

        Task Update(int id, CreatePropertiesInputModel inputModel);

        AllPropertiesViewModel Get(int id);
    }
}
