using Microsoft.EntityFrameworkCore;
using APIZapato.Models;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<bdSuperZapatoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));


//Ignorar Referencias Ciclicas
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}
   );



var misreglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misreglasCors, builder => {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    
    }
    );
}
);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();

app.UseCors(misreglasCors);
app.UseAuthorization();

app.MapControllers();

app.Run();
