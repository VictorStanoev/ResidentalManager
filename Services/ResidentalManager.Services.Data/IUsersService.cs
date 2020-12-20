namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Administration.Dashboard;

    public interface IUsersService
    {
        Task Delete(string userId);

        UsersViewModel GetUser(string userId);

        IEnumerable<UsersViewModel> GetUsers();

        Task Edit(string userId, UsersInputModel model);
    }
}
