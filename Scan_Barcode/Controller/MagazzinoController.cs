using Microsoft.AspNetCore.Mvc;
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
            return Ok(_magazzinoService.GetAllMagazzini());
        }
}