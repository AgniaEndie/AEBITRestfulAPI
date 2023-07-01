using AEBITRestfulAPI.Filters;
using AEBITRestfulAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }
    /// <summary>Registry User</summary>
    /// <param name="request">Request</param>
    /// <remarks>
    ///description
    ///</remarks>
    ///<returns>null</returns>
    [HttpPost("registry"), AllowAnonymous, HideAuthenticationFilter]
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

    [HttpPost("auth"), AllowAnonymous, HideAuthenticationFilter]
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