using Api_RickAndMorty_DotNet.Context;
using Api_RickAndMorty_DotNet.Service;
using Api_RickAndMorty_DotNet.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "API Ricky And Morty",
        Description = "Ricky And Morty Serie",
        TermsOfService = new Uri("https://foqsz.github.io/"),
        Contact = new OpenApiContact
        {
            Name = "Victor Vinicius",
            Email = "contatovictorvinicius05@gmail.com",
            Url = new Uri("https://foqsz.github.io/"),
        },
        License = new OpenApiLicense
        {
            Name = "Usar sobre LICX",
            Url = new Uri("https://foqsz.github.io/"),
        }
    });
     
    c.EnableAnnotations();
});

builder.Services.AddTransient<IRickyMortyService, RickyMortyService>();
builder.Services.AddTransient<ILocationRickyMortyService, LocationRickyMortyService>();
builder.Services.AddTransient<IEpisodesService, EpisodesService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
