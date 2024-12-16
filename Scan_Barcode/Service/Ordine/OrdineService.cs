using Scan_Barcode.Entities.DTO;
using Scan_Barcode.Repository;

namespace Scan_Barcode.Service.Ordine;

public class OrdineService : IOrdineService
{
    private readonly IOrdineRepository _ordineRepository;

    public OrdineService(IOrdineRepository ordineRepository)
    {
        _ordineRepository = ordineRepository;
    }
    
  /*
    public async Task<IEnumerable<MaterialeOrdineDto>> GetMaterialeOrdineAsync(int idOrdine, string barcode)
    {
        return await _ordineRepository.GetMaterialeOrdineAsync(idOrdine, barcode);
    }
    
    */
}