using Microsoft.AspNetCore.Mvc;
using Scan_Barcode.Entities.DTO;
using Scan_Barcode.Repository;

namespace Scan_Barcode.Controller;
[ApiController]
[Route("api/[controller]")]
public class OrdiniController : ControllerBase
{
    
        private readonly IOrdineRepository _ordineRepository;

        public OrdiniController(IOrdineRepository ordineRepository)
        {
            _ordineRepository = ordineRepository;
        }

        [HttpGet("GetOrdiniPerMagazzino")]
        public IActionResult GetOrdiniPerMagazzino(int idMagazzino)
        {
            List<Dictionary<string, object>> ordini = _ordineRepository.GetOrdiniPerMagazzino(idMagazzino);
            return Ok(ordini);
        }

        [HttpPost("InserisciOrdine")]
        public IActionResult InserisciOrdine(int idOrdine, int idMateriale, int qtaRichiesta)
        {
            int rowsAffected = _ordineRepository.InserisciOrdine(idOrdine, idMateriale, qtaRichiesta);
            return Ok(new { RowsAffected = rowsAffected });
        }
        [HttpGet("GetOrdiniPerMagazzino2")]
        public IActionResult GetOrdiniPerMagazzino2(int idMagazzino)
        {
            var ordiniFiltrati = _ordineRepository.GetOrdiniPerMagazzino2(idMagazzino);
            return Ok(ordiniFiltrati);
        }
    
}