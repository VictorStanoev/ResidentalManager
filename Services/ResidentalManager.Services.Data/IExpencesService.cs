namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Expences;

    public interface IExpencesService
    {
        Task CreateAsync(int realEstateId, CreateExpencesInputModel inputModel);

        IEnumerable<AllExpencesViewModel> GetAll(int realEstateId);

        Task DeleteAsync(int id);

        Task Update(int id, CreateExpencesInputModel inputModel);

        AllExpencesViewModel Get(int id);
    }
}
