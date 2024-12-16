using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        /*
         * Qua mostriamo tutti gli ordini di scarico di uno specifico magazzino 
         */
        [HttpGet("GetOrdiniPerMagazzinoScarico")]
        public IActionResult GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId)
        {
            if  ((userId <= 0) || (userId == null))
            {
                return BadRequest(new { message = "Id invalido." });
            }
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
            catch (SqlException ex) when (ex.Number == -2) 
            {
                return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponiblile. Riprovare!" });
            }
            
        }
        /*
         * Qua mostriamo tutti gli ordini di carico di uno specifico magazzino
         */
        [HttpGet("GetOrdiniPerMagazzinoCarico")]
        public IActionResult GetOrdiniPerMagazzinoCarico(int idMagazzino, int userId)
        {
            if  ((userId <= 0) || (userId == null))
            {
                return BadRequest(new { message = "Id invalido." });
            }
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
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = "L'ID utente fornito non è valido o non è stato fornito. Perfavore riprovare" });
            }
            catch (SqlException ex) when (ex.Number == -2) 
            {
                return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponiblile. Riprovare!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Si è verificato un errore inatteso. Contatta il supporto." });
            }
            
        }
        // codice per testare il prodotto abbia un ordine
       /* [HttpGet("{idOrdine}/{barcode}/SUPERFLUO")]
        public async Task<IActionResult> GetMaterialeOrdine(int idOrdine, string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    return BadRequest(new { message = "Errore! il Barcode risulta essere nullo!" });
                }

                if (barcode.Length < 13)
                {
                    return BadRequest(new
                        { message = "Errore! il Barcode risulta di grandezza minore a quella stabilita!" });
                }

                if (barcode.Length > 13)
                {
                    return BadRequest(new
                        { message = "Errore! il Barcode risulta di grandezza maggiore a quella stabilita!" });
                }
                if  ((idOrdine <= 0) || (idOrdine == null))
                {
                    return BadRequest(new { message = "Id invalido." });
                }
                var result = await _ordineService.GetMaterialeOrdineAsync(idOrdine, barcode);
                if (result == null || !result.Any())
                {
                    return NotFound(new
                        { message = "Attenzione! Stai forse cercando un Prodotto non incluso nell'ordine?" });
                }

                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return BadRequest(new {message = "Errore! il magazziniere non è stato specificato"});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = "L'ID utente fornito non è valido o non è stato fornito. Perfavore riprovare" });
            }
            catch (SqlException ex) when (ex.Number == -2)
            {
                return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponiblile. Riprovare!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Si è verificato un errore inatteso. Contatta il supporto." });
            }
            
        }
        */
        

}