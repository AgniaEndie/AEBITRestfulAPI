using AEBITRestfulAPI.Data;
using AEBITRestfulAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using static AEBITRestfulAPI.Models.AuthModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AEBITRestfulAPI.Services;

public interface IAuthService
{
    public Task<object>? Registry(AuthModels.RegistrationRequest request);
    public Task<object>? Authentication(AuthModels.AuthenticationRequest request);
    public Task<string>? GenerateJwt(string email);
}

public class AuthService : IAuthService
{
    private readonly WebApiContext _webApiContext;
    private readonly IConfiguration _configuration;

    public AuthService(WebApiContext webApiContext, IConfiguration configuration)
    {
        _webApiContext = webApiContext;
        _configuration = configuration;
    }

    public async Task<object>? Registry(AuthModels.RegistrationRequest request)
    {
        User user = new User();
        user.code = Guid.NewGuid().ToString();
        user.password = request.password;
        user.email = request.email;
        try
        {
            if (request.password.Length > 6)
            {
                await _webApiContext.User.AddAsync(user);
            }
            else
            {
                throw new Exception("Не валидная длина пароля");
            }
            await _webApiContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return user;
    }
    public async Task<object> Authentication(AuthenticationRequest request)
    {
        try
        {
            if (request.email != null && request.password != null)
            {
                var user = await _webApiContext.User.Where(l => l.email == request.email).FirstOrDefaultAsync();
                if (user != null)
                {
                        if (user.password == request.password)
                        {
                            var response = new AuthenticationResponse();
                            response.email = request.email;
                            response.token = await GenerateJwt(request.email);
                            return response;
                        }
                        else
                        {
                            throw new Exception("Почта или пароль неверны");
                        }
                }
                else
                {
                    throw new Exception("Пользователя с такой почтой не найдено");
                }
                
            }
        }catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return null;
    }
    public async Task<string>? GenerateJwt(string email)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, email));
        claims.Add(new Claim(ClaimTypes.Name, "user"));
        claims.Add(new Claim(ClaimTypes.Role, "auth"));
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtTokenSettings:Issuer"],
            audience: _configuration["JwtTokenSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtTokenSettings:Key"]!)), SecurityAlgorithms.HmacSha256)
            );


        return tokenHandler.WriteToken(jwt);
    }
}