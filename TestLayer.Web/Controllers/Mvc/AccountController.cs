using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestLayer.Core.Entities;
using TestLayer.Application.DTOs;
using TestLayer.Application.Services;
using TestLayer.Application.Mappers;
using TestLayer.Application.Helpers.HttpResponseExceptions;
using TestLayer.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System;

namespace TestLayer.Web.Controllers.Mvc
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;

        public AccountController(IUserService userService, IWebHostEnvironment environment, IFileService fileService)
        {
            _userService = userService;
            _environment = environment;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("login", "account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            try
            {
                await _userService.AuthUserAsync(user);

                if (ModelState.IsValid)
                {
                    await UpdateUserClaims(user.Username);
                    return RedirectToAction("Index", "Home");
                }

                return View();

            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;                
            }

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserMapper.MapUserSignUpToEntity(dto);
                    await _userService.AddUserAsync(user);

                    var createduser = await _userService.GetUserByUsernameAsync(dto.Username);

                    string defaultPath = Path.Combine(_environment.WebRootPath, "img", "default.png");
                    using (var stream = new FileStream(defaultPath, FileMode.Open, FileAccess.Read))
                    {
                        await _fileService.UploadFileAsync(stream, $"{createduser.Id}.png", Path.Combine(_environment.WebRootPath, "Uploads", "ProfilePic"));
                    }


                    return RedirectToAction("Login", "Account");
                }

                ViewBag.error = "Please fill all areas correctly.";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;               
            }

            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login", "account"); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeUserField(string field, string value)
        {
            try
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                    return RedirectToAction("Account", "Login");

                var user = await _userService.GetUserByUsernameAsync(username);

                if (user == null)
                    return RedirectToAction("Account", "Login");

                switch (field.ToLower())
                {
                    case "username":
                        user.Username = value;
                        break;
                    case "name":
                        user.Name = value;
                        break;
                    case "surname":
                        user.Surname = value;
                        break;
                    case "img":
                        break;
                    default:
                        return BadRequest("Invalid field.");
                }

                await _userService.UpdateUserAsync(user);
                await UpdateUserClaims(user.Username);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadPp(IFormFile file)
        {
            try
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                    return RedirectToAction("Account", "Login");

                var user = await _userService.GetUserByUsernameAsync(username);

                if (user == null)
                    return RedirectToAction("Account", "Login");

                if (file == null || file.Length == 0)
                    return RedirectToAction("Index", "Profile");

                using (var stream = file.OpenReadStream())
                {
                    await _fileService.UploadFileAsync(stream, $"{user.Id}.png", Path.Combine(_environment.WebRootPath, "Uploads", "ProfilePic"));
                }

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return RedirectToAction("Index", "Profile");
            }
        }





        private async Task UpdateUserClaims(string username)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "User")
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }


    }
}
