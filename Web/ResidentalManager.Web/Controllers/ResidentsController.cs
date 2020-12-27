namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Residents;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ResidentsController : Controller
    {
        private readonly IResidentsService residentsService;

        public ResidentsController(IResidentsService residentsService)
        {
            this.residentsService = residentsService;
        }

        [HttpGet]
        public IActionResult All(int propertyId, int realEstateId)
        {
            this.ViewBag.propertyId = propertyId;
            this.ViewBag.realEstateId = realEstateId;
            var model = this.residentsService.GetAll(propertyId);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult AllEstateResidents(int id)
        {
            var model = this.residentsService.GetAllEstateResidents(id);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create(int propertyId, int realEstateId)
        {
            this.ViewBag.propertyId = propertyId;
            this.ViewBag.realEstateId = realEstateId;
            var model = this.residentsService.AddFee();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int propertyId, int realEstateId, CreateResidentsInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.propertyId = propertyId;
                this.ViewBag.realEstateId = realEstateId;
                var model = this.residentsService.AddFee();
                return this.View(model);
            }

            await this.residentsService.CreateAsync(inputModel);
            return this.RedirectToAction("All", "Residents", new { propertyId, realEstateId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, int propertyId, int realEstateId)
        {
            await this.residentsService.DeleteAsync(id);
            return this.RedirectToAction("All", "Residents", new { propertyId, realEstateId });
        }

        [HttpGet]
        public IActionResult Update(string id, int propertyId, int realEstateId)
        {
            this.ViewBag.propertyId = propertyId;
            this.ViewBag.realEstateId = realEstateId;
            var model = this.residentsService.Get(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, int realEstateId, CreateResidentsInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;
            if (!this.ModelState.IsValid)
            {
                var model = this.residentsService.Get(id);
                return this.View(model);
            }

            await this.residentsService.Update(id, inputModel);
            return this.RedirectToAction("All", "Residents", new { inputModel.PropertyId, realEstateId });
        }
    }
}
