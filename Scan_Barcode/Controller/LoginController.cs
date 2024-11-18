using Microsoft.AspNetCore.Mvc;
using Scan_Barcode.Repository.Login;
using Scan_Barcode.Accessi;
namespace Scan_Barcode.Controller;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        loginRequest.Password = Accesso.Encrypt(loginRequest.Password);
        int? userId = await _loginRepository.GetUserIdAsync(loginRequest.Username, loginRequest.Password);
        
        if (userId.HasValue)
        {
            
            return Ok(new { UserId = userId.Value });
        }
        else
        {
            return Unauthorized(new { message = "Username o password invalidi."});
        }
    }
}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}