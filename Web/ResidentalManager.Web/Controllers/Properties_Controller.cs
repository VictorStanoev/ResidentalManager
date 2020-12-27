namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Properties_;

    public class Properties_Controller : Controller
    {
        private readonly IProperties_Service propertiesService;

        public Properties_Controller(IProperties_Service properties_Service)
        {
            this.propertiesService = properties_Service;
        }

        [HttpGet]
        public IActionResult All(int realEstateId)
        {
            var model = this.propertiesService.GetAll(realEstateId);
            this.ViewBag.realEstateId = realEstateId;

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.propertiesService.AddFee(realEstateId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int realEstateId, CreatePropertiesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.realEstateId = realEstateId;
                var model = this.propertiesService.AddFee(realEstateId);
                return this.View(model);
            }

            await this.propertiesService.CreateAsync(inputModel);
            return this.Redirect($"/Properties_/All?realEstateId={realEstateId}");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int realEstateId)
        {
            await this.propertiesService.DeleteAsync(id);
            return this.RedirectToAction("All", "Properties_", new { realEstateId });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = this.propertiesService.Get(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CreatePropertiesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.propertiesService.Get(id);
                return this.View(model);
            }

            await this.propertiesService.Update(id, inputModel);
            return this.Redirect($"/Properties_/All?realEstateId={inputModel.RealEstateId}");
        }
    }
}
