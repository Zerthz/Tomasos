using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tömasös.Models;
using Tömasös.ModelsIdentity;
using Tömasös.Repository;
using Tömasös.ViewModels;

namespace Tömasös.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOrderRepo _orderRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, IOrderRepo orderRepo, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _orderRepo = orderRepo;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> ViewProfile()
        {
            var model = new ViewProfileViewModel();
            model.User = await _userManager.GetUserAsync(User);
            model.Orders = _orderRepo.GetOrdersFromUser(model.User.Id);
            return View(model);


            
        }

        [Authorize]
        public async Task<IActionResult> UpdateProfile()
        {
            var model = await _userManager.GetUserAsync(User);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var curruser = await _userManager.GetUserAsync(HttpContext.User);
                curruser.Name = user.Name;
                curruser.Email = user.Email;
                curruser.PhoneNumber = user.PhoneNumber;
                curruser.StreetAddress = user.StreetAddress;
                curruser.City = user.City;
                curruser.ZipCode = user.ZipCode;


                var result = await _userManager.UpdateAsync(curruser);
                if (result.Succeeded)
                {
                    return RedirectToAction("ViewProfile", "Account");
                }
                else
                {
                    ViewBag.Message = "Misslyckade att uppdatera profil";
                    return View(user);
                }
            }
            else
            {
                ViewBag.Message = "Misslyckade att uppdatera profil";
                return View(user);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AspNetUser user)
        {

            ModelState["Id"].Errors.Clear();
            ModelState["Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            

            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    StreetAddress = user.StreetAddress,
                    ZipCode = user.ZipCode,
                    City = user.City,
                    Name = user.Name
                };




                var result = await _userManager.CreateAsync(newUser, user.Password);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "RegularUser");
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.RegMessage = "Registreringen Misslyckades";
                    return View();
                }
            }
            else
            {
                ViewBag.RegMessage = "Registreringen Misslyckades";
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AspNetUser loginUser, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Message = "Felaktig inloggning försök igen";
                return View();
            }
        }

      

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




    }
}
