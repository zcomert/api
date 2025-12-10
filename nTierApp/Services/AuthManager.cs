using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthManager(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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

    public Task<string> CreateTokenAsync(AppUser user)
    {
        // bunu sonra yapacağız
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IdentityUser> GetOneUserAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            throw new UserNotFoundException(userName);
        }
        return user;
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
