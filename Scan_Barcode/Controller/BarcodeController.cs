
using Microsoft.AspNetCore.Mvc;
using Scan_Barcode.Accessi;
using Scan_Barcode.Data;
using Scan_Barcode.Service;

namespace Scan_Barcode.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly IBarcodeService _barcodeService;

        public BarcodeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        [HttpGet("{barcode}")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            try
            {
                var product = await _barcodeService.GetProductByBarcodeAsync(barcode);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Errore interno del server", Details = ex.Message });
            }
        }
        [HttpPut("{barcode}/{newQuantity}/{IDOrdine}/{UserID}/{username}")]
        public async Task<IActionResult> UpdateProductQuantity(string barcode,  int newQuantity, int IDOrdine,int UserID,string username)
        {
            if (IDOrdine == 0)
            {
                return BadRequest(new { Message = "L'ID dell'ordine è obbligatorio e deve essere maggiore di 0." });
            }
            if (newQuantity < 0)
            {
                return BadRequest(new { Message = "Il Valore inserito è negativo, Non è possibile" });
            }
            try
            {
                var isUpdated = await _barcodeService.UpdateProductQuantityAsync(barcode, newQuantity,IDOrdine,username);

                if (isUpdated)
                {
                    
                    return Ok(new { Message = "Quantità aggiornata con successo." });
                }

                return NotFound(new { Message = "Prodotto non trovato." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Errore interno del server", Details = ex.Message });
            }
        }

    }
}