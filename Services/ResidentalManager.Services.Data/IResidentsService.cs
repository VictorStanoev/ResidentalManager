namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Residents;

    public interface IResidentsService
    {
        Task CreateAsync(CreateResidentsInputModel inputModel);

        IEnumerable<AllResidentsViewModel> GetAll(int propertyId);

        IEnumerable<AllResidentsViewModel> GetAllEstateResidents(int id);

        CreateResidentsInputModel AddFee();

        Task DeleteAsync(string id);

        Task Update(string id, CreateResidentsInputModel inputModel);

        AllResidentsViewModel Get(string id);
    }
}
