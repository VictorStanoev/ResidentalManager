namespace ResidentalManager.Web.Areas.User.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Models;

    public class UserReportsController : UserController
    {
        public UserReportsController(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; }

        public async Task<IActionResult> Index()
        {
            var user = await this.UserManager.GetUserAsync(this.User);

            if (user.RealEstateId == null || user.PropertyId == null)
            {
                return this.View("Error");
            }

            this.ViewBag.realEstateId = user.RealEstateId;
            return this.View();
        }
    }
}
