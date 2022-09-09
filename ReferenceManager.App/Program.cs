using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Core;
using ReferenceManager.App.Core.Filters;
using ReferenceManager.App.Core.Hubs;
using ReferenceManager.App.Core.MiddlewareExtensions;
using ReferenceManager.App.Core.SubscribeTableDependencies;
using ReferenceManager.App.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider(); 

builder.Services.AddSignalR();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddDbContext<DBReferenciasContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseReferencias"));
});

//DI
builder.Services.AddSingleton<GestionReferenciaHub>();
builder.Services.AddSingleton<SqlDependencyServiceReferencia>();
builder.Services.AddSingleton<IGestionReferenciaRepository, GestionReferenciaRepository>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddAuthorization();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();


var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DataBaseReferencias");

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

app.MapHub<GestionReferenciaHub>("/GestionReferenciaHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");


/*
 * we must call SubscribeTableDependency() here
 * we create one middleware and call SubscribeTableDependency() method in the middleware
 */
app.UseSqlTableDependency<SqlDependencyServiceReferencia>();


app.Run();
