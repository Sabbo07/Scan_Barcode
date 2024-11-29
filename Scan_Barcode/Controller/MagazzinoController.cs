using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Scan_Barcode.Service.Magazzino;

namespace Scan_Barcode.Controller;
[ApiController]
[Route("api/[controller]")]
public class MagazzinoController : ControllerBase
{
    
    
        private readonly IMagazzinoService _magazzinoService;

        public MagazzinoController(IMagazzinoService magazzinoService)
        {
            _magazzinoService = magazzinoService;
        }

        [HttpGet("GetMagazzino")]
        public ActionResult<List<Dictionary<string, object>>> GetAll()
        {
            try
            {
                return Ok(_magazzinoService.GetAllMagazzini());
            }
            catch (SqlException ex) when (ex.Number == -2) 
            {
                return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponiblile. Riprovare!" });
            }

        }
}