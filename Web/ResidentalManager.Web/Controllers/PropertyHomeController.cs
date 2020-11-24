namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PropertyHomeController : BaseController
    {
        public PropertyHomeController()
        {
        }

        public IActionResult Index(int realEstateId)
        {
            this.ViewBag.realEstateId = realEstateId;
            return this.View();
        }
    }
}
