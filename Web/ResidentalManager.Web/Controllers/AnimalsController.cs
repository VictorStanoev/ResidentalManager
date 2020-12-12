namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Animals;
    using System.Threading.Tasks;

    public class AnimalsController : Controller
    {
        private readonly IAnimalsService animalService;

        public AnimalsController(IAnimalsService animalService)
        {
            this.animalService = animalService;
        }

        [HttpGet]
        public IActionResult Create(int realEstateId, int propertyId)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;
            var model = this.animalService.AddFeeDropDown();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int realEstateId, int propertyId, AnimalsInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.propertyId = propertyId;

            if (!this.ModelState.IsValid)
            {
                var model = this.animalService.AddFeeDropDown();
                return this.View(model);
            }

            await this.animalService.CreateAsync(inputModel);
            return this.RedirectToAction("All", "Properties_", new { realEstateId });
        }
    }
}
