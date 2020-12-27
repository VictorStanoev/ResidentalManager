namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Administration.Dashboard;

    public interface IUsersService
    {
        Task Delete(string userId);

        UsersViewModel GetUser(string userId);

        UsersViewModel GetUserProperty(string userId, int realEstateId);

        IEnumerable<UsersViewModel> GetUsers();

        Task EditRealEstate(string userId, int realEstateId);

        Task EditProperty(string userId, int propertyId);
    }
}
