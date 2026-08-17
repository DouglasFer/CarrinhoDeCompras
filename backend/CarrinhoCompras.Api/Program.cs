using CarrinhoCompras.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Application.Interfaces.Servicos;
using CarrinhoCompras.Application.Services;
using CarrinhoCompras.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Carrinho de Compras API",
        Version = "v1"
    });
});

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<ICupomRepository, CupomRepository>();
builder.Services.AddScoped<ICupomServico, CupomServico>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<ICarrinhoServico, CarrinhoServico>();

// CORS (front-end Angular em dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Carrinho de Compras API v1"));
}

app.UseHttpsRedirection();

app.UseCors("AngularApp");

app.MapControllers();

app.Run();