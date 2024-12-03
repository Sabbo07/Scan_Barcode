using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scan_Barcode.Data;
using Scan_Barcode.Repository;
using Scan_Barcode.Repository.Barcode;

namespace Scan_Barcode.Service
{
    public class BarcodeService : IBarcodeService
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly DatabaseService _databaseService;
        public BarcodeService(IBarcodeRepository barcodeRepository, DatabaseService databaseService)
        {
            _barcodeRepository = barcodeRepository;
            _databaseService = databaseService;
        }

        public async Task<Dictionary<string, object>> GetProductByBarcodeAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new ArgumentException("Il barcode non può essere nullo o vuoto.");
            }

            var product = await _barcodeRepository.FindByBarcodeAsync(barcode);

            if (product == null)
            {
                throw new KeyNotFoundException("Prodotto non trovato per il barcode fornito.");
            }

            return product;
        }
        public async Task<bool> UpdateProductQuantityAsyncScarico(string barcode, int quantityChange, int idOrdine, string username)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new ArgumentException("Il barcode non può essere nullo o vuoto.");
            }

            // Recupera la quantità attuale dal database
            var currentQuantity = await _barcodeRepository.GetCurrentQuantityByBarcodeAsync(barcode, idOrdine);

            // Verifica se la quantità risultante sarebbe negativa
            if (currentQuantity + quantityChange < 0)
            {
                throw new InvalidOperationException("La quantità non può diventare negativa.");
            }

            // Procedi con l'aggiornamento della quantità
            var isUpdated = await _barcodeRepository.UpdateQuantityByBarcodeAsyncCarico(barcode, quantityChange, idOrdine, username);

            if (!isUpdated)
            {
                throw new KeyNotFoundException("Prodotto non trovato per il barcode fornito.");
            }

            return isUpdated;
        }

        public async Task<string> GetUsernameByUserIdAsync(int userId)
        {
            string query = @"SELECT UserId 
                     FROM Utenti 
                     WHERE IdUtente = @UserID";

            var parameters = new Dictionary<string, object>
            {
                { "@UserID", userId }
            };

            var result = await _databaseService.ExecuteScalarAsync(query, parameters);
            return result?.ToString();
        }

    }
}