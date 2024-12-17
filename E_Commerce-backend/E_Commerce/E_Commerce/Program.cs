using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow localhost:4200
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<E_CommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));


// Register repository services
builder.Services.AddScoped<Iitem, ItemsRepository>();
builder.Services.AddScoped<IUsers, UsersRepository>();
builder.Services.AddScoped<ICart, CartRepository>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
