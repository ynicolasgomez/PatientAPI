using PatientAPI.Infrastructure;
using PatientAPI.Infrastructure.DocumentType;
using PatientAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<GenderRepository>();
builder.Services.AddScoped<DocumentTypeRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ExamRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
