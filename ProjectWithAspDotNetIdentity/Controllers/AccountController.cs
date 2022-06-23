using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectWithAspDotNetIdentity.ViewFolder;

namespace ProjectWithAspDotNetIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> uManager,
                                 SignInManager<IdentityUser> sManager)
        {
            userManager = uManager;
            signInManager = sManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,
          string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await
                signInManager.PasswordSignInAsync(model.Username,
                                              model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
     
       }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                };
                var result = await userManager.CreateAsync(user,
                                                         model.Password);

                if (result.Succeeded)
                {
                    //samajh ni ay
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("index", "home");
                    }
                    return RedirectToAction("login", "account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
