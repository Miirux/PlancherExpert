using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PlancherExpert.Models;
using Microsoft.EntityFrameworkCore;

namespace PlancherExpert.Controllers
{
    public class AccessController : Controller
    {
        private readonly PlancherExpertContext _context;

        public AccessController(PlancherExpertContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "CouvrePlanchers");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Superviseur modelLogin)
        {
            List<Superviseur> lstSuperviseur = new List<Superviseur>(await _context.Superviseurs.ToListAsync());
            foreach(var superviseur in lstSuperviseur)
            {
                if (modelLogin.Email == superviseur.Email && modelLogin.MotDePasse == superviseur.MotDePasse)
                {
                    List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                        new Claim("OtherProperties","Example Role")
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "CouvrePlanchers");
                }
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        public IActionResult SignUp()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "CouvrePlanchers");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,Nom,Prenom,Ville,Zip,Tel,Email,MotDePasse")] Superviseur superviseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superviseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(superviseur);
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
