using Ramos.eSocial.Api.IoC;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Adiciona suporte a controllers
builder.Services.AddControllers();

// 🔹 (Opcional) Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Registra seus módulos
builder.Services.AddModules();


//swagger port
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirSwagger", builder =>
    {
        builder.WithOrigins("http://localhost:5000") // ou a porta onde o Swagger roda
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔹 (Opcional) Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirSwagger");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API eSocial S1000 v1");
        c.RoutePrefix = string.Empty; 
    });
}

// 🔹 Roteamento e controllers
app.MapControllers();

app.Run();