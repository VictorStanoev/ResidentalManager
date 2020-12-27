namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Fees;

    public interface IFeesService
    {
        Task CreateAsync(CreateFeesInputModel inputModel);

        IEnumerable<FeesViewModel> GetAll(int realEstateId);

        Task DeleteAsync(int id);

        void Update(int id, CreateFeesInputModel inputModel);

        FeesViewModel Get(int id);
    }
}
