namespace ResidentalManager.Services.Data
{
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Animals;

    public interface IAnimalsService
    {
        Task CreateAsync(AnimalsInputModel inputModel);

        AnimalsInputModel AddFeeDropDown();
    }
}
