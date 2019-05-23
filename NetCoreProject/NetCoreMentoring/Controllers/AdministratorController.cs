using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Areas.Identity.Data;

namespace NetCoreMentoring.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("admin")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdministratorController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }
    }
}