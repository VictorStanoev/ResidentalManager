namespace ResidentalManager.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Common;
    using ResidentalManager.Web.Controllers;

    [Authorize(Roles = GlobalConstants.UserRoleName)]
    [Area("User")]
    public class UserController : BaseController
    {
    }
}