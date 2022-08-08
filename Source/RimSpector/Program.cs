
using Microsoft.Extensions.Caching.Memory;
using RimSpectorApi;
using RimSpectorApi.Attributes;
using System.Net;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RimSpector", Version = "v1" });
});

//builder.Services.AddMemoryCache();
builder.Services.AddTransient<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<Cache>();
builder.Services.AddTransient<Service>();
builder.Services.AddTransient<ClientKeyFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{   
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//TODO make home page
//TODO empty pawns/ideo page (id available, but no data yet. User in menu or something)


app.UseMiddleware<ErrorHandlerMiddleware>();
app.Use(async (context, next) =>
{
    //TODO make some nice error pages
    await next();
    if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        context.Request.Path = "/Errors/NotFound";
        await next();
    }
    if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
    {
        context.Request.Path = "/Errors/Error";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{    
//    endpoints.MapControllerRoute(
//       name: "default",
//       pattern: "{clientId}/{Page=Pawns}",
//       defaults: new { controller = "Pawns" });
//    endpoints.Map("{clientId}",)
//});

app.MapRazorPages();
app.MapControllers();

app.Run();
