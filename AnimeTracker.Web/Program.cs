using AnimeTracker.Application.Interfaces;
using AnimeTracker.Application.Interfaces.Repositories;
using AnimeTracker.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using AnimeTracker.Application.Mappings;
using AnimeTracker.Application.Services;
using AnimeTracker.Domain.Entities;
using AnimeTracker.Infrastructure.Services; // JikanService burada
using AnimeTracker.Persistence.Contexts;
using AnimeTracker.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --- Serilog Konfigürasyonu ---
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// -----------------------------

// 1. Controller ve View Servisleri
builder.Services.AddControllersWithViews();

// FluentValidation Kaydı
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(AnimeTracker.Application.Validations.AnimeCreateDtoValidator).Assembly);



// 2. DbContext Ayarı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 3. Identity Ayarları
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 4. Repository ve UnitOfWork Kayıtları
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 5. Uygulama Servisleri (Business Logic)
builder.Services.AddScoped<IAnimeService, AnimeService>();

// 6. Jikan API Servisi Kaydı
builder.Services.AddHttpClient<IJikanService, JikanService>();

// 7. AutoMapper Kaydı
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<GeneralMapping>();
});

// --- TÜM KAYITLAR BURADAN ÖNCE BİTMELİ ---
var app = builder.Build();

// Serilog İstek Loglama Middleware
app.UseSerilogRequestLogging();

// Global Exception Handling Middleware
app.UseMiddleware<AnimeTracker.Web.Middlewares.ExceptionMiddleware>();

// 8. Pipeline Ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();