using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReferenceManager.App.Core.Filters;
using ReferenceManager.App.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBReferenciasContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseReferencias"));
});

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddAuthorization();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("es-CO"),
        new CultureInfo("en-US"),
        new CultureInfo("fa-IR"),
        new CultureInfo("de-DE")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "es-CO", uiCulture: "es-CO");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    var defaultCookieRequestProvider =
        options.RequestCultureProviders.FirstOrDefault(rcp =>
            rcp.GetType() == typeof(CookieRequestCultureProvider));
    if (defaultCookieRequestProvider != null)
        options.RequestCultureProviders.Remove(defaultCookieRequestProvider);

    options.RequestCultureProviders.Insert(0,
        new CookieRequestCultureProvider()
        {
            CookieName = ".AspNetCore.Culture",
            Options = options
        });
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix) 
            .AddDataAnnotationsLocalization();

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

app.UseCookiePolicy();
app.UseSession();



app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer" + JWToken);
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");



app.Run();
