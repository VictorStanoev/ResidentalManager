namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Web.ViewModels.Fees;

    public class FeesController : Controller
    {
        private readonly IFeesService feesService;

        public FeesController(IFeesService feesService)
        {
            this.feesService = feesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = this.feesService.GetAll();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
           return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.feesService.CreateAsync(inputModel);
            return this.Redirect("/Fees/All");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.feesService.DeleteAsync(id);
            return this.Redirect("/Fees/All");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = this.feesService.Get(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, CreateFeesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.feesService.Get(id);
                return this.View(model);
            }

            this.feesService.Update(id, inputModel);
            return this.Redirect("/Fees/All");
        }
    }
}
