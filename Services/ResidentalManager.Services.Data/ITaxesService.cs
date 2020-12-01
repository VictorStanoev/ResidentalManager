﻿namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ResidentalManager.Web.ViewModels.Taxes;

    public interface ITaxesService
    {
        IEnumerable<AllTaxesViewModel> GetAllEstateTaxes(int realEstateId);

        Task GenerateTaxes(int realEstateId, GenerateTaxesInputModel inputModel);
    }
}