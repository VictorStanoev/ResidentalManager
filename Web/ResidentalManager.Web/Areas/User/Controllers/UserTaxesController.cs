namespace ResidentalManager.Web.Areas.User.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels;

    public class UserTaxesController : UserController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITaxesService taxesService;

        public UserTaxesController(
            UserManager<ApplicationUser> userManager,
            ITaxesService taxesService)
        {
            this.userManager = userManager;
            this.taxesService = taxesService;
        }

        public async Task<IActionResult> Test()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userID = user.Id;
            return Json(user.Id);
        }

        public async Task<IActionResult> All(int pageNum = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.RealEstateId == null || user.PropertyId == null)
            {
                return this.View("Error");
            }

            var realEstateId = (int)user.RealEstateId;
            var propertyId = (int)user.PropertyId;

            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;

            var model = this.taxesService.GetAllPropertyTaxes(realEstateId, propertyId, pageNum);
            return this.View(model);
        }
    }
}
