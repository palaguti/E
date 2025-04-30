using Microsoft.EntityFrameworkCore;
using E.DAL;
using E.BL;
System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture; var builder = WebApplication.CreateBuilder(args);


// Configuración del DbContext para la entidad Persona
builder.Services.AddDbContext<EDBContext>(options =>
{
    var conexionString = builder.Configuration.GetConnectionString("Conn");
    options.UseMySql(conexionString, ServerVersion.AutoDetect(conexionString));
});

// Inyección de dependencias para la capa DAL y BL
builder.Services.AddScoped<PersonaEDAL>();
builder.Services.AddScoped<PersonaEBL>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
