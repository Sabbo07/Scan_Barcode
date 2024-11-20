using Scan_Barcode.Repository.Magazzino;

namespace Scan_Barcode.Service.Magazzino;

public class MagazzinoService : IMagazzinoService
{
    private readonly IMagazzinoRepository _magazzinoRepository;

    public MagazzinoService(IMagazzinoRepository magazzinoRepository)
    {
        _magazzinoRepository = magazzinoRepository;
    }

    public List<Dictionary<string, object>> GetAllMagazzini()
    {
        return _magazzinoRepository.GetMagazzini();
    }
}