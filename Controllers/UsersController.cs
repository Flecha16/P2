using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserStore<ApplicationUser> _userStore;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<UsersController> logger, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _userStore = userStore;
        }

        // GET: /Users/Index
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user, string roleName)
        {
            if (ModelState.IsValid)
            {
                
                await _userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
                // Crea el usuario
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    // Asigna el rol al usuario
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ApplicationUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name", "Name", userRoles);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser model, string roleName)
        {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                // Actualizar el rol del usuario
                var currentRoles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        _logger.LogError($"Error removing user roles: {error.Description}");
                    }
                }

                if (!string.IsNullOrEmpty(roleName))
                {
                    result = await _userManager.AddToRoleAsync(user, roleName);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                            _logger.LogError($"Error adding user to role: {error.Description}");
                        }
                    }
                }

                // Validar si los valores del usuario se han actualizado correctamente
                var updatedUser = await _userManager.UpdateAsync(user);
                if (!updatedUser.Succeeded)
                {
                    foreach (var error in updatedUser.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        _logger.LogError($"Error updating user: {error.Description}");
                    }

                    var userRoles = await _userManager.GetRolesAsync(user);
                    ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name", "Name", userRoles);
                    return View(model);
                }

                return RedirectToAction("Index");
            

            //var roles = await _roleManager.Roles.ToListAsync();
            //ViewData["Roles"] = new SelectList(roles, "Name", "Name");
            //return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }

    }

}
