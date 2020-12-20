namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;

    using ResidentalManager.Web.ViewModels.Administration.Dashboard;

    public interface IUsersService
    {
        IEnumerable<UsersViewModel> GetUsers();
    }
}
