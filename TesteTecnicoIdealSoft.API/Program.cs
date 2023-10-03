using TesteTecnicoIdealSoft.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjectionHandler(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MigrateDatabase();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }