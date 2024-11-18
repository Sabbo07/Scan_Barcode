using Scan_Barcode.Data;
using Scan_Barcode.Repository;
using Microsoft.Extensions.DependencyInjection;
using Scan_Barcode.Repository.Login;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al contenitore.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura DatabaseService e OrdineRepository per l'iniezione delle dipendenze
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddScoped<IOrdineRepository, OrdineRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();


// Aggiungi i controller
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

// Configura Swagger per essere sempre attivo
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();