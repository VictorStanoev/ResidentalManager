namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Services.Messaging;
    using ResidentalManager.Web.ViewModels.Taxes;

    using ResidentalManager.Common;

    public class TaxesController : BaseController
    {
        private readonly ITaxesService taxesService;
        private readonly IEmailSender emailSender;

        public TaxesController(ITaxesService taxesService, IEmailSender emailSender)
        {
            this.taxesService = taxesService;
            this.emailSender = emailSender;
        }

        public IActionResult All(int realEstateId, int pageNum = 1)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.taxesService.GetAllEstateTaxes(realEstateId, pageNum);
            return this.View(model);
        }

        public IActionResult Generate(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Generate(int realEstateId, TaxesGenerateInputModel inputModel)
        {
            this.ViewBag.realEstateId = realEstateId;

            await this.taxesService.GenerateTaxes(realEstateId, inputModel);

            return this.RedirectToAction("All", new { realEstateId });
        }

        public async Task<IActionResult> Pay(int id, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.Pay(id);
            return this.Redirect($"/Taxes/All?realEstateId={realEstateId}");
        }

        public async Task<IActionResult> ReversePayment(int id, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.ReversePayment(id);
            return this.Redirect($"/Taxes/All?realEstateId={realEstateId}");
        }

        public async Task<IActionResult> UpdateTax(int id, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.UpdateTax(id);
            return this.Redirect($"/Taxes/All?realEstateId={realEstateId}");
        }

        public IActionResult Receipt(int id, int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.taxesService.GetReceiptInfo(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Receipt(int id, int realEstateId, TaxReceiptViewModel taxReceiptModel)
        {
            this.ViewBag.realEstateId = realEstateId;

            if (!this.ModelState.IsValid)
            {
                var model = this.taxesService.GetReceiptInfo(id);
                return this.View(model);
            }

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                GlobalConstants.SystemName,
                taxReceiptModel.Email,
                taxReceiptModel.FullName,
                taxReceiptModel.Title,
                taxReceiptModel.Content);

            return this.RedirectToAction("All", "Taxes", new { realEstateId });
        }
    }
}
