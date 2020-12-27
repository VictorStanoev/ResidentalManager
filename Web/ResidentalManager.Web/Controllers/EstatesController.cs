namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Estates;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class EstatesController : Controller
    {
        private readonly IEstatesService estatesService;

        public EstatesController(IEstatesService estatesService)
        {
            this.estatesService = estatesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var realEstates = this.estatesService.GetAll();
            return this.View(realEstates);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateEstatesInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEstatesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.estatesService.CreateAsync(inputModel);
            return this.RedirectToAction("All", "Estates");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.estatesService.DeleteAsync(id);
            return this.RedirectToAction("All", "Estates");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = this.estatesService.Get(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, CreateEstatesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.estatesService.Get(id);
                return this.View(model);
            }

            this.estatesService.Update(id, inputModel);
            return this.RedirectToAction("All", "Estates");
        }
    }
}
