using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICurrentUserDiscountsProvider, CurrentUserDiscountsProvider>();
builder.Services.AddDbContext<DiscountDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.MapGet("tickets/{userid}/{zone}", (string userid, string zone, [FromServices] ICurrentUserDiscountsProvider currentUserDiscountsProvider) =>
    {
        var zoneEnum = Enum.Parse<Zone>(zone);
        var price = TicketPriceCalculator.Calculate(zoneEnum, userid, currentUserDiscountsProvider);
        return Results.Ok(new TicketPriceResponse(price));
    })
    .WithName("Get zone ticket price")
    .WithOpenApi();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

public partial class Program;