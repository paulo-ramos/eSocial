using Ramos.eSocial.S1000.Api.IoC;
using Ramos.eSocial.S1000.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// Registre os endpoints diretamente no nível superior
app.MapControllers();
app.MapEndpoints(); // Chame o método para mapear os endpoints personalizado

app.UseMiddleware<RequestIdMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

