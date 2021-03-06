﻿namespace ResidentalManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Services.Data;
    using ResidentalManager.Services.Messaging;
    using ResidentalManager.Web.ViewModels.Taxes;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

        public IActionResult PropertyTaxes(int realEstateId, int propertyId, int pageNum = 1)
        {
            this.ViewBag.realEstateId = realEstateId;
            var model = this.taxesService.GetAllPropertyTaxes(realEstateId, propertyId, pageNum);
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

        public async Task<IActionResult> Pay(int id, int realEstateId, int pageNum)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.Pay(id);
            return this.RedirectToAction("All", "Taxes", new { realEstateId, pageNum });
        }

        public async Task<IActionResult> ReversePayment(int id, int realEstateId, int pageNum)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.ReversePayment(id);
            return this.RedirectToAction("All", "Taxes", new { realEstateId, pageNum });
        }

        public async Task<IActionResult> UpdateTax(int id, int realEstateId, int pageNum)
        {
            this.ViewBag.realEstateId = realEstateId;
            await this.taxesService.UpdateTax(id);
            return this.RedirectToAction("All", "Taxes", new { realEstateId, pageNum });
        }

        public IActionResult Receipt(int id, int realEstateId, int pageNum)
        {
            this.ViewBag.realEstateId = realEstateId;
            this.ViewBag.pageNum = pageNum;
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
