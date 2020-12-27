namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator, User")]
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
