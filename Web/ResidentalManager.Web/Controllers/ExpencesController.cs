namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Expences;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ExpencesController : BaseController
    {
        private readonly IExpencesService expencesService;

        public ExpencesController(IExpencesService expencesService)
        {
            this.expencesService = expencesService;
        }

        [HttpGet]
        public IActionResult All(int realEstateId, int pageNum = 1)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.expencesService.GetAll(realEstateId, pageNum);
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

            return this.RedirectToAction("All", "Expences", new { realEstateId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int realEstateId, int pageNum)
        {
            await this.expencesService.DeleteAsync(id);
            return this.RedirectToAction("All", "Expences", new { realEstateId, pageNum });
        }
    }
}
