namespace ResidentalManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FeesController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/");
        }
    }
}
