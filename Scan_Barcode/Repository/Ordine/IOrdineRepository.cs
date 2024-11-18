﻿using Scan_Barcode.Migrations;

namespace Scan_Barcode.Repository;

public interface IOrdineRepository
{
    List<Dictionary<string, object>> GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId);
}