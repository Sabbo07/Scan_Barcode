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
        
        [HttpGet("GetOrdiniPerMagazzinoScarico")]
        public IActionResult GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId)
        {
            try
            {
                var ordiniFiltrati = _ordineRepository.GetOrdiniPerMagazzinoScarico(idMagazzino, userId);
                if (ordiniFiltrati.Count == 0)
                {
                    return NotFound(new { message = "La lista è vuota. Nessun ordine trovato." });
                }
                return Ok(ordiniFiltrati);
            }
            catch (NullReferenceException)
            {
                return BadRequest(new {message = "Errore! il magazziniere non è stato specificato"});
            }
            
        }
        
        [HttpGet("GetOrdiniPerMagazzinoCarico")]
        public IActionResult GetOrdiniPerMagazzinoCarico(int idMagazzino, int userId)
        {
            try
            {
                var ordiniFiltrati = _ordineRepository.GetOrdiniPerMagazzinoCarico(idMagazzino, userId);
                if (ordiniFiltrati.Count == 0)
                {
                    return NotFound(new { message = "La lista è vuota. Nessun ordine trovato." });
                }
                return Ok(ordiniFiltrati);
            }
            catch (NullReferenceException)
            {
                return BadRequest(new {message = "Errore! il magazziniere non è stato specificato"});
            }
            
        }
    
}