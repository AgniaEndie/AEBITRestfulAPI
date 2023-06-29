using AEBITRestfulAPI.Data;
using AEBITRestfulAPI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<WebApiContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("MainBase"))
    );
builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //x.SerializerSettings.ContractResolver = new LowecaseContractResolver();
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
