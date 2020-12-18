namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Pets;
    using System.Threading.Tasks;

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
            var model = this.petService.AddFeeDropDown();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int realEstateId, int propertyId, PetsInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;

            if (!this.ModelState.IsValid)
            {
                var model = this.petService.AddFeeDropDown();
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
    }
}
