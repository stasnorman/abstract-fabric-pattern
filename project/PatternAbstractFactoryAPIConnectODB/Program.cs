using Microsoft.EntityFrameworkCore;
using PatternAbstractFactoryAPIConnectODB.DatabaseContexts;
using PatternAbstractFactoryAPIConnectODB.Factories;

var builder = WebApplication.CreateBuilder(args);

// Регистрация конкретных фабрик
builder.Services.AddScoped<SqlServerFactory>();
builder.Services.AddScoped<PostgreSqlFactory>();

// Регистрация функции для выбора фабрики по имени
builder.Services.AddScoped<Func<string, IDatabaseFactory>>(serviceProvider => key =>
{
    return key switch
    {
        "SqlServer" => serviceProvider.GetRequiredService<SqlServerFactory>(),
        "Postgres" => serviceProvider.GetRequiredService<PostgreSqlFactory>(),
        _ => throw new ArgumentException($"Unknown key: {key}")
    };
});

builder.Services.AddControllers();
builder.Services.AddPooledDbContextFactory<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddPooledDbContextFactory<PostgreSqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
