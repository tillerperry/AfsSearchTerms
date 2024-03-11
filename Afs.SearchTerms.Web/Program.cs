using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Options;
using Afs.SearchTerms.Web.Services.Interfaces;
using Afs.SearchTerms.Web.Services.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services.AddControllersWithViews();
 services.AddScoped<IHttpServices,HttpServices>();
services.AddScoped<ITranslatorDbRepository,TranslatorDbRepository>();
services.AddScoped<ITranslatorService,TranslatorService>();


//AppContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(config.GetConnectionString("DbConnection")));

//configurations
services.Configure<ExternalApiConfigs>(config.GetSection(nameof(ExternalApiConfigs)));

//build application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   // pattern: "{controller=Home}/{action=Index}/{id?}");
pattern: "{controller=TranslationSearch}/{action=Index}/{id?}");

app.Run();

