namespace ResidentalManager.Web.Areas.User.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Models;

    public class UserTaxesController : UserController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserTaxesController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task <IActionResult> Test()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userID = user.Id;
            return Json(user.Id);
        }
    }
}
