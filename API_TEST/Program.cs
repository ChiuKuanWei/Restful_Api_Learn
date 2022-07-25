using API_TEST.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=CreativeTEMP_DB;Trusted_Connection=True;User ID=sa;Password=54013657" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
builder.Services.AddControllers();
//builder.Services.AddDbContext<ACTRON_WIPContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDatabase")));
builder.Services.AddDbContext<CreativeTEMP_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDatabase")));
builder.Services.AddAutoMapper(typeof(Program));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
