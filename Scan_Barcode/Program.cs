using Scan_Barcode.Data;
using Scan_Barcode.Repository;
using Microsoft.Extensions.DependencyInjection;
using Scan_Barcode.Repository.Barcode;
using Scan_Barcode.Repository.Login;
using Scan_Barcode.Repository.Magazzino;
using Scan_Barcode.Service;
using Scan_Barcode.Service.Magazzino;
using Scan_Barcode.Service.Ordine;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al contenitore.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura DatabaseService e OrdineRepository per l'iniezione delle dipendenze
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddScoped<IOrdineRepository, OrdineRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IBarcodeService, BarcodeService>();
builder.Services.AddScoped<IBarcodeRepository, BarcodeRepository>();
builder.Services.AddScoped<IMagazzinoRepository, MagazzinoRepository>();
builder.Services.AddScoped<IMagazzinoService, MagazzinoService>();
builder.Services.AddScoped<IOrdineService, OrdineService>();
// Aggiungi i controller
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Configura i servizi CORS per consentire tutte le origini
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Consenti tutte le origini
            .AllowAnyMethod() // Consenti tutti i metodi (GET, POST, PUT, DELETE, ecc.)
            .AllowAnyHeader(); // Consenti tutti gli header
    });
});

var app = builder.Build();

// Configura Swagger per essere sempre attivo
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Abilita CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();