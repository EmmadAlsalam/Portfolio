using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Portfolio.Models;
using System;
using System.Security.Cryptography;

namespace Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Generera OTP
                var otp = GenerateOTP();
                
                // Spara OTP i TempData för verifiering
                TempData["OTP"] = otp;
                TempData["Email"] = model.Email;
                TempData["Name"] = model.Name;

                // I en riktig applikation skulle du skicka OTP via email här
                // För demo, visar vi det i TempData
                TempData["DisplayOTP"] = otp;

                // Skapa användaren utan lösenord
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("VerifyOTP");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOTP(string otp)
        {
            var storedOTP = TempData["OTP"]?.ToString();
            var email = TempData["Email"]?.ToString();
            var name = TempData["Name"]?.ToString();

            if (otp == storedOTP && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Användaren hittades inte");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ogiltig OTP-kod");
            }

            return View();
        }

        private string GenerateOTP()
        {
            // Generera en 6-siffrig OTP-kod
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[4];
                rng.GetBytes(bytes);
                var random = BitConverter.ToUInt32(bytes, 0);
                return (random % 1000000).ToString("D6");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Ogiltigt inloggningsförsök.");
            }
            return View(model);
        }
    }
}
