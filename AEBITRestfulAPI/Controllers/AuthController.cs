using AEBITRestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController
{
    private IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("registry")]
    public async Task<object> RegistryUser(RegistrationRequest request)
    {
        try
        {
            var result = await _authService.Registry(request);
            return result;
        }
        catch (Exception ex)
        {
            ExceptionMessage response = new ExceptionMessage();
            response.message = ex.Message;
            return response;
        }
    }

    [HttpPost("auth")]
    public async Task<object> AuthenticationUser(AuthenticationRequest request)
    {
        try
        {
            var result = await _authService.Authentication(request);
            return result;
        }catch (Exception ex)
        {
            ExceptionMessage response = new ExceptionMessage();
            response.message = ex.Message;
            return response;
        }
    }

}