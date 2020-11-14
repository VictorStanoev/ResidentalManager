namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Fees;

    public interface IFeesService
    {
        Task CreateAsync(CreateFeesInputModel inputModel);

        Task DeleteAsync(int id);

        void Update(int id, CreateFeesInputModel inputModel);

        AllFeesViewModel Get(int id);

        IEnumerable<AllFeesViewModel> GetAll();
    }
}
