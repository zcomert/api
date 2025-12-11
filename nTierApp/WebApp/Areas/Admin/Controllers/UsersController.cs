using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly IServiceManager _manager;

    public UsersController(IServiceManager manager)
    {
        _manager = manager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _manager.AuthService.GetAllUsersAsync();
        return View(users);
    }

    public IActionResult Create()
    {
        return View(new UserForRegistrationDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserForRegistrationDto userForRegistrationDto)
    {
        if (!ModelState.IsValid)
            return View(userForRegistrationDto);

        userForRegistrationDto.Roles.Add("User"); 

        var result = await _manager.AuthService.RegisterUserAsync(userForRegistrationDto);

        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(userForRegistrationDto);
        }
    }

    public async Task<IActionResult> Update(string id)
    {
        var user = await _manager.AuthService.GetUserByIdAsync(id);
        if (user is null) return NotFound();

        var userRoles = await _manager.AuthService.GetRolesAsync(id);
        var allRoles = await _manager.AuthService.GetAllRoleNamesAsync();

        ViewBag.User = user;
        ViewBag.UserRoles = userRoles;
        ViewBag.AllRoles = allRoles;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(string userId, List<string> roles)
    {
        var user = await _manager.AuthService.GetUserByIdAsync(userId);
        if(user is null) return NotFound();

        var userRoles = await _manager.AuthService.GetRolesAsync(userId);

        await _manager.AuthService.RemoveFromRolesAsync(userId, userRoles);
        if (roles is not null && roles.Any())
        {
            await _manager.AuthService.AddToRolesAsync(userId, roles);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        var user = await _manager.AuthService.GetUserByIdAsync(id);
        if (user is null)
            return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var result = await _manager.AuthService.DeleteUserAsync(id);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach(var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        
        var user = await _manager.AuthService.GetUserByIdAsync(id);
        return View(user);
    }
}
