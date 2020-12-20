namespace ResidentalManager.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Administration.Dashboard;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IUsersService usersService;

        public DashboardController(
            ISettingsService settingsService,
            IUsersService usersService)
        {
            this.settingsService = settingsService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        public IActionResult Users()
        {
            var model = this.usersService.GetUsers();

            return this.View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.usersService.Delete(id);

            return this.RedirectToAction("Users", "Dashboard");
        }

        public IActionResult Edit(string id)
        {
            var model = this.usersService.GetUser(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UsersInputModel model)
        {
            await this.usersService.Edit(id, model);
            return this.RedirectToAction("Users", "Dashboard");
        }
    }
}
