namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Residents;

    public class ResidentsController : Controller
    {
        private readonly IResidentsService residentsService;

        public ResidentsController(IResidentsService residentsService)
        {
            this.residentsService = residentsService;
        }

        [HttpGet]
        public IActionResult All(int propertyId)
        {
            this.ViewData["propertyId"] = propertyId;
            var model = this.residentsService.GetAll(propertyId);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create(int propertyId)
        {
            this.ViewData["propertyId"] = propertyId;
            var model = this.residentsService.AddFee();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int propertyId, CreateResidentsInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["propertyId"] = propertyId;
                var model = this.residentsService.AddFee();
                return this.View(model);
            }

            await this.residentsService.CreateAsync(inputModel);
            return this.Redirect($"/Residents/All?propertyId={propertyId}");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, int propertyId)
        {
            await this.residentsService.DeleteAsync(id);
            return this.Redirect($"/Residents/All?propertyId={propertyId}");
        }

        [HttpGet]
        public IActionResult Update(string id, int propertyId)
        {
            this.ViewData["propertyId"] = propertyId;
            var model = this.residentsService.Get(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, CreateResidentsInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.residentsService.Get(id);
                return this.View(model);
            }

            await this.residentsService.Update(id, inputModel);
            return this.Redirect($"/Residents/All?propertyId={inputModel.PropertyId}");
        }
    }
}
