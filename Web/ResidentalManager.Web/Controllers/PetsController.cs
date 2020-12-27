namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Pets;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class PetsController : Controller
    {
        private readonly IPetsService petService;

        public PetsController(IPetsService petService)
        {
            this.petService = petService;
        }

        [HttpGet]
        public IActionResult Create(int realEstateId, int propertyId)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;
            var model = this.petService.AddFeeDropDown(realEstateId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int realEstateId, int propertyId, PetsInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;

            if (!this.ModelState.IsValid)
            {
                var model = this.petService.AddFeeDropDown(realEstateId);
                return this.View(model);
            }

            await this.petService.CreateAsync(inputModel);
            return this.RedirectToAction("All", "Pets", new { propertyId, realEstateId });
        }

        [HttpGet]
        public IActionResult All(int propertyId, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;

            var model = this.petService.GetAll(propertyId);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Update(int id, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.petService.Get(id, realEstateId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, int realEstateId, PetsInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;
            if (!this.ModelState.IsValid)
            {
                var model = this.petService.Get(id, realEstateId);
                return this.View(model);
            }

            await this.petService.Update(id, inputModel);
            return this.RedirectToAction("All", "Pets", new { inputModel.PropertyId, realEstateId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int realEstateId, int propertyId)
        {
            await this.petService.DeleteAsync(id);

            return this.RedirectToAction("All", "Pets", new { realEstateId, propertyId });
        }
    }
}
