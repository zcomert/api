using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceManager _manager;

        public AccountController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserForAuthenticationDto userDto)
        {
            if(!ModelState.IsValid)
            {
                return View(userDto);
            }

            var result = await _manager.AuthService.PasswordSignInAsync(
                userDto.UserName,
                userDto.Password,
                true);

            if(result.Succeeded)
                return RedirectToAction("Index", "Book");
            
            ModelState.AddModelError("", "Başarısız giriş! Lütfen kullanıcı adı ve şifrenizi kontrol ediniz!");
            return View(userDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _manager.AuthService.SignOutAsync();
            return RedirectToAction("Index", "Book");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
