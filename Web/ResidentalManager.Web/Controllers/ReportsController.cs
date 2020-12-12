namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReportsController : BaseController
    {
        public ReportsController()
        {
        }

        public IActionResult Index(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            return this.View();
        }
    }
}
