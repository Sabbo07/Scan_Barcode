
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        
        /*
         * Qua effettuiamo l'update del prodotto preso in esame
         */
        [HttpPut("{barcode}/{newQuantity}/{IDOrdine}/{UserID}")]
        public async Task<IActionResult> UpdateProductQuantity(string barcode, int newQuantity, int IDOrdine, int UserID)
        {
            try
            {
                var username = await _barcodeService.GetUsernameByUserIdAsync(UserID);

                if (string.IsNullOrEmpty(username))
                {
                    return NotFound(new { message = "Errore! L'username associato all'UserID non è stato trovato." });
                }

                var isUpdated = await _barcodeService.UpdateProductQuantityAsync(barcode, newQuantity, IDOrdine, username);

                if (isUpdated)
                {
                    return Ok(new { Message = "Quantità aggiornata con successo." });
                }

                return NotFound(new { Message = "Prodotto non trovato." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message }); // Quantità non valida
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (SqlException ex) when (ex.Number == -2) 
            {
                return StatusCode(500, new { Message = "Attenzione il network attualmente non è disponiblile. Riprovare!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Errore interno del server", Details = ex.Message });
            }
        }


    }
}