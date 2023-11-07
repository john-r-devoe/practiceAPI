using Microsoft.EntityFrameworkCore;
using Scratch.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
    options.AddPolicy("DevCors", (corsbuilder) =>
        corsbuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    )
);
builder.Services.AddCors((options) =>
    options.AddPolicy("ProdCors", (corsbuilder) =>
        corsbuilder.WithOrigins("https://mywebsitedomain.com").AllowAnyMethod().AllowAnyHeader().AllowCredentials() //replace with actual domain
    )
);
builder.Services.AddDbContext<DataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCors");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("ProdCors");

}


app.UseAuthorization();

app.MapControllers();

app.Run();
