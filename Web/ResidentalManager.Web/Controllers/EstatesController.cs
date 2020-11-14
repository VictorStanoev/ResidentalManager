namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Estates;

    public class EstatesController : Controller
    {
        private readonly IEstatesService estatesService;

        public EstatesController(IEstatesService estatesService)
        {
            this.estatesService = estatesService;
        }

        public IActionResult All()
        {
            var realEstates = this.estatesService.GetAll();
            return this.View(realEstates);
        }

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
            return this.Redirect("/Estates/All");

            // return this.Json(inputModel);
        }
    }
}
