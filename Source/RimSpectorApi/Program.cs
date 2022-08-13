
using Microsoft.Extensions.Caching.Memory;
using NeoSmart.Caching.Sqlite;
using RimSpectorApi;
using RimSpectorApi.Attributes;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Note: this *must* come before services.AddMvc()!
builder.Services.AddSqliteCache(options =>
{
    options.CachePath = @"cache.db";
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RimSpector", Version = "v1" });
});

builder.Services.AddTransient<IMemoryCache, MemoryCache>();
builder.Services.AddTransient<MemoryCache>();
builder.Services.AddSingleton<Cache>();
builder.Services.AddTransient<Service>();
builder.Services.AddTransient<ClientKeyFilter>();

builder.Services.AddApplicationInsightsTelemetry(o =>
{
    o.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//TODO make home page
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseStatusCodePages();

app.MapHealthChecks("/monitoring/health");

app.Run();
