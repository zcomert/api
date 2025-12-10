
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistrationDto);
    Task<AppUser?> ValidateUserAsync(UserForAuthenticationDto userForAuthenticationDto);
    Task<String> CreateTokenAsync(AppUser user);
    Task<AppUser> GetOneUserAsync(string userName);
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser?> GetUserByIdAsync(string userId);
    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent);
    Task SignOutAsync();
    Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword);
    Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<IList<string>> GetRolesAsync(string userId);
    Task<IList<string>> GetAllRoleNamesAsync();
}
