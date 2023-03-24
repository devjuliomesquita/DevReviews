using DevReviews_API.Persistence;
using DevReviews_API.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Chamando a COnection string 
builder.Services.AddDbContext<DevReviewsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

//Injeção de Dependência do DbContext
builder.Services.AddDbContext<DevReviewsDbContext>();
//Chamando o AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile));

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
