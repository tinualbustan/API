using InventoryManagementCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProjectQueries();
builder.Services.AddProjectServices();
builder.Services.AddProjectRepository();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
