namespace ResidentalManager.Services.Data
{
    using ResidentalManager.Web.ViewModels.Taxes;
    using System.Threading.Tasks;

    public interface ITaxesService
    {
        TaxesListViewModel GetAllEstateTaxes(int realEstateId, int pageNum);

        TaxesListViewModel GetAllPropertyTaxes(int realEstateId, int propertyId, int pageNum);

        Task GenerateTaxes(int realEstateId, TaxesGenerateInputModel inputModel);

        Task Pay(int id);

        Task ReversePayment(int id);

        Task UpdateTax(int id);

        int GetCount(int realEstateId);

        int GetCountPropertyTaxes(int propertyId);

        TaxReceiptViewModel GetReceiptInfo(int id);
    }
}
