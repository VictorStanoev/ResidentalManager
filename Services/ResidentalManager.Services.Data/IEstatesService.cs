namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Estates;

    public interface IEstatesService
    {
        Task CreateAsync(CreateEstatesInputModel inputModel);

        IEnumerable<AllEstatesViewModel> GetAll();
    }
}
