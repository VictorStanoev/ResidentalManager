namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Expences;

    public class ExpencesController : BaseController
    {
        private readonly IExpencesService expencesService;

        public ExpencesController(IExpencesService expencesService)
        {
            this.expencesService = expencesService;
        }

        [HttpGet]
        public IActionResult All(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.expencesService.GetAll(realEstateId);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int realEstateId, CreateExpencesInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;

            if (!this.ModelState.IsValid)
            {
                return this.View(realEstateId);
            }

            await this.expencesService.CreateAsync(realEstateId, inputModel);

            return this.Redirect($"/Expences/All?realEstateId={realEstateId}");
        }
    }
}
