using Microsoft.AspNetCore.Mvc;
using Scan_Barcode.Entities.DTO;
using Scan_Barcode.Repository;
using Scan_Barcode.Service.Ordine;

namespace Scan_Barcode.Controller;
[ApiController]
[Route("api/[controller]")]
public class OrdiniController : ControllerBase
{
    
        private readonly IOrdineRepository _ordineRepository;
        private readonly IOrdineService _ordineService;

        public OrdiniController(IOrdineRepository ordineRepository, IOrdineService ordineService)
        {
            _ordineRepository = ordineRepository;
            _ordineService = ordineService;
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
        [HttpGet("{idOrdine}/{barcode}")]
        public async Task<IActionResult> GetMaterialeOrdine(int idOrdine, string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                return BadRequest(new {message = "Errore! il Barcode risulta essere nullo!"});
            }

            if (barcode.Length < 13)
            {
                return BadRequest(new {message = "Errore! il Barcode risulta di grandezza minore a quella stabilita!"});
            }
            if (barcode.Length > 13)
            {
                return BadRequest(new {message = "Errore! il Barcode risulta di grandezza maggiore a quella stabilita!"});
            }
            
            var result = await _ordineService.GetMaterialeOrdineAsync(idOrdine, barcode);
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "Attenzione! Stai forse cercando un Prodotto non incluso nell'ordine?" });
            }
            return Ok(result);
        }
    
}