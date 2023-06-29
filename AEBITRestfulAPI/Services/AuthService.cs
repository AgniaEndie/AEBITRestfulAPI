using AEBITRestfulAPI.Data;
using AEBITRestfulAPI.Models;
using Microsoft.EntityFrameworkCore;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Services;

public interface IAuthService
{
    public Task<object>? Registry(AuthModels.RegistrationRequest request);
    public Task<object>? Authentication(AuthModels.AuthenticationRequest request);
}

public class AuthService : IAuthService
{
    private readonly WebApiContext _webApiContext;

    public AuthService(WebApiContext webApiContext)
    {
        _webApiContext = webApiContext;
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
                            response.token = Guid.NewGuid().ToString();
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
}