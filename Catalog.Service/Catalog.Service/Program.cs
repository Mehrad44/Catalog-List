using Carter;
using Catalog.Service;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEshopServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapCarter();

app.UseCors();

app.MapControllers();

app.Run();
