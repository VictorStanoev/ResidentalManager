namespace ResidentalManager.Services.Data
{
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Expences;

    public interface IExpencesService
    {
        Task CreateAsync(int realEstateId, CreateExpencesInputModel inputModel);

        ExpencesListViewModel GetAll(int realEstateId, int pageNum);

        AllExpencesViewModel Get(int id);

        Task DeleteAsync(int id);
    }
}
