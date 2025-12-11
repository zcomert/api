

using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthManager(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> AddToRolesAsync(string userId, IEnumerable<string> roles)
    {
        // 1. Kullanıcıyı ID'ye Göre Bul
        var user = await GetUserByIdAsync(userId);

        // 2. Tüm roller aldık
        var existingroles = await GetAllRoleNamesAsync();

        // 3. Kullanıcıya Rollerini Ata
        var validRoles = roles
            .Where(r => existingroles.Contains(r))
            .ToList() ?? new List<String>();
        
        if (!validRoles.Any())
            return IdentityResult.Success;
        
        // 4. Atama işlemini gerçekleştir
        return await _userManager
            .AddToRolesAsync(user!, validRoles!);
    }

    public async Task<IdentityResult> ChangePasswordAsync(string userId, 
        string currentPassword, 
        string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            throw new UserNotFoundException(userId);
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<string> CreateTokenAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var item in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item));
        }

        // appsettings.json dosyasından Token ayarlarını okuyup
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? string.Empty;
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<IList<string>> GetAllRoleNamesAsync()
    {
        var roles  = await _roleManager
                .Roles
                .Select(r => r.Name ?? String.Empty)
                .ToListAsync();
        return roles;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<AppUser> GetOneUserAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            throw new UserNotFoundException(userName);
        }
        return user;
    }

    public async Task<IList<string>> GetRolesAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        return await _userManager.GetRolesAsync(user!);
    }

    public async Task<AppUser?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user is null)
            throw new UserNotFoundException(userId);
        return user;    
    }

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent)
    {
        // lockoutOnFailure: yanlış parola denemelerinde hesabı kilitlemeyi dikkate almak
        return await _signInManager
            .PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure: false);
    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistrationDto)
    {

        // 1. adım: Kullanıcıyı Oluştur (Nesne oluşturma)
        AppUser user = new AppUser()
        {
            UserName = userForRegistrationDto.UserName,
            Email = userForRegistrationDto.Email,
            TCKN = userForRegistrationDto.TCKN,
            PhoneNumber = userForRegistrationDto.PhoneNumber,
            FirstName = userForRegistrationDto.FirstName,
            LastName = userForRegistrationDto.LastName
        };

        // 2. adım: Kullanıcıyı Veritabanına Kaydet
        var result = await _userManager
            .CreateAsync(user, userForRegistrationDto.Password);

        // 3. adım: Kullanıcıya Rollerini Ata (sonuç başarılı ise)
        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
        }

        // 4. adım: Sonucu Dön
        return result;
    }

    public async Task<IdentityResult> RemoveFromRolesAsync(string userId, 
        IEnumerable<string> roles)
    {
        // 1. Kullanıcıyı ID'ye Göre Bul
        var user = await GetUserByIdAsync(userId);

        // 2. Tüm roller aldık
        var existingroles = await GetAllRoleNamesAsync();

        // 3. Kullanıcıdan Rollerini Kaldır
        var validRoles = roles
            .Where(r => existingroles.Contains(r))
            .ToList() ?? new List<String>();
        if (!validRoles.Any())
            return IdentityResult.Success;
        
        // 4. Kaldırma işlemini gerçekleştir
        return await _userManager
            .RemoveFromRolesAsync(user!, validRoles!);
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        return await _userManager.DeleteAsync(user!);
    }

    public async Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword)
    {
        // 1. Kullanıcıyı ID'ye Göre Bul
        var user = await _userManager.FindByIdAsync(userId);

        // 2. İstisna Yönetimi: Kullanıcı Bulunamazsa
        if (user is null)
        {
            IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = $"Kullanıcı ID'si '{userId}' olan kullanıcı bulunamadı."
            });
        }

        // 3. Parolayı Sıfırlamak için Geçici Bir Jeton Oluştur
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user!);

        // 4. Yeni Parolayı Ayarla
        var result = await _userManager
            .ResetPasswordAsync(user!, resetToken, newPassword);
        return result;
    }

    public async Task SeedUsersAndRolesAsync()
    {
        // Admin Rolü sistem var mı?
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            // Admin rolü yok, oluştur
            IdentityRole adminRole = new IdentityRole("Admin");
            //await _roleManager.CreateAsync(adminRole);
            _roleManager.CreateAsync(adminRole).Wait();
        }

        // User Rolü sistem var mı?
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            // User rolü yok, oluştur
            IdentityRole userRole = new IdentityRole("User");
            await _roleManager.CreateAsync(userRole);
        }

        // Admin Kullanıcı var mı?
        if (await _userManager.FindByNameAsync("admin") is null)
        {
            // Admin kullanıcı yok, oluştur
            AppUser adminUser = new AppUser()
            {
                UserName = "Admin",
                Email = "admin@samsun.edu.tr",
                FirstName = "John",
                LastName = "Doe",
                TCKN = "12345678901",
                PhoneNumber = "5551234567",
            };

            var result = await _userManager
                .CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // zcomert Kullanıcı var mı?
        if (await _userManager.FindByNameAsync("zcomert") is null)
        {
            // zcomert kullanıcı yok, oluştur
            AppUser normalUser = new AppUser()
            {
                UserName = "zcomert",
                Email = "zcomert@samsun.edu.tr",
                TCKN = "10987654321",
                FirstName = "Zafer",
                LastName = "Comert",
                PhoneNumber = "5559876543",
                EmailConfirmed = true
            };
        
            var result = await _userManager
                .CreateAsync(normalUser, "Zcomert123!");
            
            if (result.Succeeded)   
            {
                await _userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<AppUser?> ValidateUserAsync(UserForAuthenticationDto userForAuthenticationDto)
    {
        // 1. Kullanıcıyı Kullanıcı Adına Göre Bul
        var user = await _userManager
            .FindByNameAsync(userForAuthenticationDto.UserName);

        // 2. Parolayı Doğrula
        if(user is not null && 
            await _userManager.CheckPasswordAsync(user, userForAuthenticationDto.Password))
        {
            // 3. Kullanıcıyı Dön 
            return user;
        }

        // Kullanıcı yok
        return null;
    }
}
