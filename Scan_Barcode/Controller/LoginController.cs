using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        try
        {
            loginRequest.Password = Accesso.Encrypt(loginRequest.Password);
            var (userId, status) = await _loginRepository.GetUserIdAsync(loginRequest.Username, loginRequest.Password);

            switch (status)
            {
                case "Success":
                    return Ok(new { UserId = userId });

                case "InvalidUser":
                    return Unauthorized(new { message = "Username o Password non validi." });

                case "UserNotFound":
                    return Unauthorized(new { message = "Utente non trovato." });

                case "UnauthorizedRole":
                    return Unauthorized(new { message = "Utente non autorizzato per questo sistema." });

                default:
                    return Unauthorized(new { message = "Accesso non autorizzato." });
            }
        }
        catch (SqlException ex) when (ex.Number == -2)
        {
            return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponibile. Riprovare!" });
        }
    }

}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}