namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Taxes;
    using System.Threading.Tasks;

    public class TaxesController : BaseController
    {
        private readonly ITaxesService taxesService;

        public TaxesController(ITaxesService taxesService)
        {
            this.taxesService = taxesService;
        }

        public IActionResult All(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.taxesService.GetAllEstateTaxes(realEstateId);
            return this.View(model);
        }

        public IActionResult Generate(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Generate(int realEstateId, GenerateTaxesInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;

            await this.taxesService.GenerateTaxes(realEstateId, inputModel);

            return this.Redirect($"/Taxes/All?realEstateId={realEstateId}");
        }
    }
}
