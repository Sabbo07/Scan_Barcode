using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scan_Barcode.Repository;
using Scan_Barcode.Repository.Barcode;

namespace Scan_Barcode.Service
{
    public class BarcodeService : IBarcodeService
    {
        private readonly IBarcodeRepository _barcodeRepository;

        public BarcodeService(IBarcodeRepository barcodeRepository)
        {
            _barcodeRepository = barcodeRepository;
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
        public async Task<bool> UpdateProductQuantityAsync(string barcode, int newQuantity)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new ArgumentException("Il barcode non può essere nullo o vuoto.");
            }

            if (newQuantity < 0)
            {
                throw new ArgumentException("La quantità non può essere negativa.");
            }

            var isUpdated = await _barcodeRepository.UpdateQuantityByBarcodeAsync(barcode, newQuantity);

            if (!isUpdated)
            {
                throw new KeyNotFoundException("Prodotto non trovato per il barcode fornito.");
            }

            return isUpdated;
        }

    }
}