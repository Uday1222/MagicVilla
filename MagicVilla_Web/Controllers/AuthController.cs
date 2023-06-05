using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            LoginRequestDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO != null)
            {
                var response = await _authService.LoginAsync<APIResponse>(loginRequestDTO);

                if (response != null && response.IsSuccess)
                {
                    LoginResponseDTO login = JsonConvert.DeserializeObject<LoginResponseDTO>(response.Result.ToString());

                    var handler = new JwtSecurityTokenHandler();

                    var jwt = handler.ReadJwtToken(login.Token);

                    var role = jwt.Claims.FirstOrDefault(x => x.Type == "role").Value;

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, login.User.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal);

                    HttpContext.Session.SetString(SD.SessionToken, login.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            RegisterationRequestDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            if(registerationRequestDTO != null)
            {
               var response = await _authService.RegisterAsync<APIResponse>(registerationRequestDTO);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Registered Successfully";
                    RedirectToAction("Login");
                }
                else if(response.ErrorMessages.Count() > 0)
                {
                    TempData["error"] = response.ErrorMessages.FirstOrDefault();
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
